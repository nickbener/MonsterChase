using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float jumpForce, jumpForceOnLiana, groundChekRadius;
    public Transform groundCheker;
    public LayerMask groundMask;
    public Joystick joystick;
    public GameObject tnt;
    public TextMeshProUGUI distText;
    public Transform distance;
    public GameObject level;
    public MonsterFollow monsterFollow;
    public GameObject whiteObj;
    public ScreenFade screenFade;

    [Header("Camera")]
    public Transform cameraMain;
    public Transform cameraPos1;
    public Transform cameraPos2;

    private Vector2 vineVelocity;
    private TransformSpeed[] allObjWithScript;

    [Header("Sounds")]
    public List<AudioSource> jumpSound = new List<AudioSource>();
    public AudioSource monsterSound;
    public AudioSource fallSound;

    [Header("Coins")]
    public Text cointText;
    public int coins;
    public AudioSource[] coinsSound;

    [Header("Stamina")]
    public Image uiFill;
    public float stmStap;
    public float stmSecontStap;
    public float staminaCourCd;
    public float cdForJoysticRight;
    public float stopCdForTotem;
    private float stmScore;

    //Vine
    private bool swinging = false;
    private Transform currentSwinging;

    //MuroveiTrap
    [Header("MuroveiTrap")]
    public float timeBetweenPresses = 0.001f;
    public int requiredPressCount = 3;
    private float timeSinceLastPress = 0f;
    private int pressCount = 0;
    private float jm;
    private bool inMurovei;
    private bool isConditionExec = false;

    //Buy samples
    public bool portalIsBuying;
    public bool antiBiteIsBuying;
    public bool bootsIsBuying;
    public bool eqipIsBuying;

    private bool grounded;
    private bool isFalling;
    private Animator anim;
    private Animator breakAnim;
    private Animator monsterAnim;
    private Rigidbody2D rb;
    private float verticalMove;
    private float horizontalMove;
    private bool stopGame;
    private bool isTotemCoinOn;

    private bool isJoystickPressed;

    //Log перемнные
    [HideInInspector] public bool normalSpeedMod = true;
    [HideInInspector] public bool isScoreOnNull;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        Time.timeScale = 1;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetFloat("Speed", 1.5f);
        stmScore = 100;
        normalSpeedMod = true;
        isTotemCoinOn = false;
        inMurovei = false;
        StartCoroutine(StaminaCD());
        if(portalIsBuying)
        {
            Vector2 newVecPosition = new Vector3(1000, 0, 0);
            distance.position = newVecPosition;
        }
        UpdateTransformSpeedArray();
        isJoystickPressed = false;
    }

    void Update()
    {
        Application.targetFrameRate = 500;
        uiFill.fillAmount = stmScore / 100.0f;

        verticalMove = joystick.Vertical;
        horizontalMove = joystick.Horizontal;
        anim.SetBool("isGrounded", grounded);
        anim.SetBool("fall", isFalling);
        grounded = Physics2D.OverlapCircle(groundCheker.position, groundChekRadius, groundMask);

        //Jump
        if (verticalMove >= .4f && grounded && inMurovei == false || Input.GetKeyDown(KeyCode.W) && grounded && inMurovei == false)
        {
            rb.velocity = Vector2.up * jumpForce;
            jumpSound[Random.Range(0, jumpSound.Count)].Play();
        }

        //Liana
        if (verticalMove >= .4f && swinging || Input.GetKeyDown(KeyCode.W) && swinging)
        {
            swinging = false;
            rb.velocity = currentSwinging.GetComponent<Rigidbody2D>().velocity;
            rb.AddForce(Vector2.up * jumpForceOnLiana, ForceMode2D.Impulse);
        }

        foreach (TransformSpeed transformSpeed in allObjWithScript)
        {
            // Определить значение, на которое нужно изменить скорость
            float speedChange = 0f;
            
            if (horizontalMove >= .5f|| Input.GetKey(KeyCode.D) && !isJoystickPressed)
            {
                //Здесь джойстик зажат в право(ускорение)
                speedChange += transformSpeed.baseSpeed * 0.5f; // Добавить 50%
                if (!isJoystickPressed)
                {
                    staminaCourCd -= cdForJoysticRight;
                    isJoystickPressed = true;
                    anim.SetFloat("Speed", 3);
                }
            }
            else if(horizontalMove < .5f && isJoystickPressed)
            {
                //Здесь джойстик не зажат в право
                staminaCourCd += cdForJoysticRight;
                isJoystickPressed = false;
                anim.SetFloat("Speed", 1.5f);
            }
            if (normalSpeedMod == false)
            {
                //Врезался в бревно
                if(bootsIsBuying == false)
                {
                    speedChange -= transformSpeed.baseSpeed * 0.3f;
                }
                if (bootsIsBuying)
                {
                    speedChange -= (transformSpeed.baseSpeed * 0.3f) * 0.5f;
                }
            }
            if (normalSpeedMod)
            {
                //Бревно перестало действовать
                speedChange += transformSpeed.baseSpeed * 0.1f;
            }

            if (stopGame == false)
            {
                //Ничего не происходит
                transformSpeed.speed = transformSpeed.baseSpeed + speedChange;
            }
            else if (stopGame)
            {
                //Игра закончилась
                transformSpeed.speed = 0f;
            }
        }

        //BigJump
        if (horizontalMove >= .5f && verticalMove <= .3f && inMurovei)
        {
            stmStap = stmSecontStap;
        }
        else
        {
            stmStap = 1f;
        }

        if (verticalMove <= -.4f || Input.GetKey(KeyCode.S))
        {
            rb.gravityScale = 5;
        }
        else
        {
            rb.gravityScale = 2.8f;
        }

        //In Murovei Trap
        if (inMurovei)
        {
            if (verticalMove > 0 && !isConditionExec || Input.GetKeyDown(KeyCode.W) && !isConditionExec)
            {
                isConditionExec = true;
                if (Time.time - timeSinceLastPress > timeBetweenPresses)
                {
                    pressCount = 1;
                }
                else
                {
                    pressCount++;
                }
                
                timeSinceLastPress = Time.time;
                
                if (pressCount == requiredPressCount)
                {
                    inMurovei = false;
                    pressCount = 0;
                }
            }
        }

        if (verticalMove == 0)
        {
            isConditionExec = false;
        }

        if (stmScore <= 0)
        {
            for (int i = 0; i < allObjWithScript.Length; i++)
            {
                if (bootsIsBuying)
                {
                    allObjWithScript[i].speed = 2 + (2 * 0.5f);
                    stmScore = 0;
                    anim.SetFloat("Speed", 0.2f + (0.2f * 0.5f));
                }
                if(bootsIsBuying == false)
                {
                    allObjWithScript[i].speed = 2;
                    stmScore = 0;
                    anim.SetFloat("Speed", 0.2f);

                }
            }
            antiBiteIsBuying = false;
            isScoreOnNull = true;
        }
        else
        {
            isScoreOnNull = false;
        }

        if(stmScore > 100)
        {
            stmScore = 100;
        }

        //Vine Trap
        if(swinging)
        {
            transform.position = currentSwinging.position;
        }


        float dist = Mathf.RoundToInt(distance.position.x);
        distText.text = dist.ToString();

        cointText.text = coins.ToString();
    } //Update is over

    public void UpdateTransformSpeedArray()
    {
        allObjWithScript = FindObjectsOfType<TransformSpeed>();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //--------------------------------//
        //Fruits Collision
        if (collision.CompareTag("Banana"))
        {
            stmScore += 10;
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Kokos"))
        {
            stmScore += 10;
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Lepeshki"))
        {
            stmScore += 15;
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Papaya"))
        {
            stmScore += 50;
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Packed"))
        {
            stmScore += 30;
        }
        if (collision.CompareTag("TotemCoin"))
        {
            StartCoroutine(ActivateTotemCoin());
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("TotemStamina"))
        {
            StartCoroutine(ActivateTotemStamina());
            Destroy(collision.gameObject);
        }
        //--------------------------------------//
        if (collision.CompareTag("Coin"))
        {
            coins++;
            if (isTotemCoinOn)
            {
                coins++;
            }
            coinsSound[Random.Range(0, coinsSound.Length)].Play();
            Destroy(collision.gameObject);
        }
        if(collision.CompareTag("Scene"))
        {
            SceneManager.LoadScene(1);
        }
        
        if(collision.CompareTag("Monster") )
        {
            if (antiBiteIsBuying == false)
            {
                stopGame = true;
                GameObject monster_obj = GameObject.FindGameObjectWithTag("Monster");
                monster_obj.GetComponent<MonsterFollow>().enabled = false;
                rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                monsterSound.Play();
                StartCoroutine(DelayBeforeSceneLoad(2));
                staminaCourCd = 10;
            }
            if (antiBiteIsBuying)
            {
                StartCoroutine(CourForBiteDist());
            }
        }
        if (collision.CompareTag("Fall"))
        {
            if(eqipIsBuying == false)
            {
                Time.timeScale = 0;
                fallSound.Play();
                StartCoroutine(DelayBeforeSceneLoad(2f));
            }
            else
            {
                rb.velocity = Vector2.up * jumpForce;
                eqipIsBuying = false;
            }
        }

        if (collision.CompareTag("Cave"))
        {
            Destroy(collision.gameObject);
            GameObject Shaht_LVL = GameObject.Find("ShahtLevel");
            transform.position = Shaht_LVL.transform.position;
            cameraMain.position = cameraPos2.position;
            anim.SetBool("isRunCave", true);
        }

        if (collision.CompareTag("ExitCave"))
        {
            GameObject Shaht_LVL = GameObject.Find("ShahtLevel");
            transform.position = Vector2.zero;
            cameraMain.position = cameraPos1.position;
            anim.SetBool("isRunCave", false);
        }

        //----------------- Traps -----------------//
        //WallTrap Collision
        if (collision.CompareTag("WallTrap"))
        {
            GameObject objWall = GameObject.FindGameObjectWithTag("Wall");
            objWall.GetComponentInChildren<BoxCollider2D>().isTrigger = false;
            objWall.GetComponentInChildren<SpriteRenderer>().enabled = true;
        }

        //VineTrap Collision
        if (collision.CompareTag("Vine"))
        {
            collision.GetComponent<Rigidbody2D>().velocity = vineVelocity;
            swinging = true;
            currentSwinging = collision.transform;
        }

        //Desert Collision
        if (collision.CompareTag("Desert"))
        {
            //-30% speed
            normalSpeedMod = false;
            //-50% jump
            jm = jumpForce * (50.0f / 100.0f);
            jumpForce = jm;
        }

        //Murovei Collision
        if(collision.CompareTag("Murovei"))
        {
            inMurovei = true;
            collision.isTrigger = false;
        }

        if (collision.CompareTag("Portal"))
        {
            transform.position = new Vector2(0, 30);
            cameraMain.position = new Vector3(0, 30, -100);
            whiteObj.SetActive(true);
            screenFade.WhiteScreen();
        }

    }

    private IEnumerator ActivateTotemCoin()
    {
        if (!isTotemCoinOn)
        {
            isTotemCoinOn = true;

            // Запустить таймер на 10 секунд
            yield return new WaitForSeconds(10f);

            // По истечении времени сбросить переменную
            isTotemCoinOn = false;
        }
    }

    private IEnumerator ActivateTotemStamina()
    {
        staminaCourCd += stopCdForTotem;

        // Запустить таймер на 10 секунд
        yield return new WaitForSeconds(10f);

        // По истечении времени сбросить переменную
        staminaCourCd -= stopCdForTotem;
    }

    IEnumerator CourForBiteDist()
    {
        monsterFollow.currentSpeed -= 6;

        yield return new WaitForSeconds(1);

        monsterFollow.currentSpeed += 6;
        antiBiteIsBuying = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Desert Collision
        if (collision.CompareTag("Desert"))
        {
            //+50% jump
            jumpForce = jm * 2;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Brevno Trap Collision
        if (collision.gameObject.tag == "Brevno")
        {
            //Percent healf
            stmScore -= 100.0f * (10.0f / 100.0f);

            //Percent speed
            StartCoroutine(normalCour());
            Destroy(collision.gameObject);
        }

        //BreakPlatformTrap Collision
        if (collision.gameObject.tag == "BreakPlatform")
        {
            breakAnim = collision.gameObject.GetComponent<Animator>();
            breakAnim.SetTrigger("isColl");
            Destroy(collision.gameObject, 0.9f);
        }

        //StoneTrap Collision
        if (collision.gameObject.tag == "Stone")
        {
            Restart();
            Destroy(collision.gameObject);
        }
    }

    IEnumerator normalCour()
    {
        normalSpeedMod = false;
        yield return new WaitForSeconds(3);
        normalSpeedMod = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7 && verticalMove <= -.1f || collision.gameObject.layer == 7 && Input.GetKey(KeyCode.S))
        {
            isFalling = true;
            Physics2D.IgnoreLayerCollision(6, 7, true);
            Invoke("IgnoreLayerOff", 0.5f);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Brevno")
        {
            normalSpeedMod = true;
        }
    }
    void IgnoreLayerOff()
    {
        isFalling = false;
        grounded = false;
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }

    void Repeat()
    {
        StartCoroutine(StaminaCD());
    }

    IEnumerator StaminaCD()
    {
        stmScore -= stmStap;
        yield return new WaitForSeconds(staminaCourCd);
        Repeat();
    }

    private IEnumerator DelayBeforeSceneLoad(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1;
        Restart();
    }

    void DelaySoundEating()
    {
        monsterSound.Play();
    }

    public void TntSpawn()
    {
        Instantiate(tnt, transform.position, Quaternion.identity);
    }

}