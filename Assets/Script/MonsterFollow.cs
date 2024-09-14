using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFollow : MonoBehaviour
{
    public Transform player; // ������ �� ������ ������
    public Joystick joystick; // ������ �� ��������� ���������
    public PlayerController playerController;
    public float smoothSpeed = 2.0f; // �������� ��� ��������� ��������
    public float minYLimit; // ����������� ������ �� ��� Y
    public float maxYLimit = 3.0f;  // ������������ ������ �� ��� Y

    public float baseSpeed = 0.3f; // ��������� ��������
    public float joysticSpeedModifier; 
    public float normalSpeedModifier; 
    public float scoreSpeedModifier; 
    public float onBiteSpeedModifier; 

    private bool isJoystickHeld = false;
    private bool isNormalModHeld = false;
    private bool isScoreModHeld = false;

    private bool isDKeyHeld = false;

    [HideInInspector] public float currentSpeed; // ������� ��������

    private void Start()
    {
        currentSpeed = baseSpeed;
        isJoystickHeld = false;
        isNormalModHeld = false;
        isDKeyHeld = false;
    }

    private void Update()
    {
        if(player.position.y <= 15)
        {
            minYLimit = -2.0f;
            maxYLimit = 3.0f;
        }
        else if(player.position.y >= 15)
        {
            minYLimit = 28.4f;
            maxYLimit = 32.5f;
        }

        if (player != null && joystick != null)
        {
            // �������� ������� ��������� ������� � ������
            Vector3 currentPosition = transform.position;
            Vector3 playerPosition = player.position;

            // ���������� ������� ������ �� ��� Y
            playerPosition.y = Mathf.Clamp(playerPosition.y, minYLimit, maxYLimit);

            // ���������� X ������� ������ �������� ��������� �� X
            playerPosition.x = currentPosition.x;

            Vector3 newPositionMons = Vector3.Lerp(currentPosition, playerPosition, smoothSpeed * Time.deltaTime);

            // ���������� ����� ������� �������
            transform.position = newPositionMons;
        }

        if (joystick.Horizontal >= 0.5f && !isJoystickHeld)
        {
            currentSpeed += joysticSpeedModifier; // ���� �������� ����� ������
            isJoystickHeld = true; // ���������� ����, ��� �������� �����
        }
        else if (joystick.Horizontal < 0.5f && isJoystickHeld)
        {
            currentSpeed -= joysticSpeedModifier; // ���� �������� ��� �������
            isJoystickHeld = false; // �������� ����, ��� �������� �����

        }

        if (Input.GetKey(KeyCode.D) && !isDKeyHeld)
        {
            currentSpeed += joysticSpeedModifier;
            isDKeyHeld = true;
        }
        else if (!Input.GetKey(KeyCode.D) && isDKeyHeld)
        {
            currentSpeed -= joysticSpeedModifier;
            isDKeyHeld = false;
        }

        if (playerController.normalSpeedMod == false && !isNormalModHeld)
        {
            currentSpeed += normalSpeedModifier; // ���� �������� � ������
            isNormalModHeld = true; // ���������� ����, ��� �������� � ������
        }
        else if (playerController.normalSpeedMod == true && isNormalModHeld)
        {
            currentSpeed -= normalSpeedModifier; // ���� ��������� ����������� ������
            isNormalModHeld = false; // �������� ����, ��� ��������� ����������� ������

        }

        if (playerController.isScoreOnNull == true && !isScoreModHeld)
        {
            currentSpeed += scoreSpeedModifier; // ���� ����������� �������
            isScoreModHeld = true; // ���������� ����, ��� ����������� �������
        }
        else if (playerController.isScoreOnNull == false && isScoreModHeld)
        {
            currentSpeed -= scoreSpeedModifier; // ���� ��������� �������
            isScoreModHeld = false; // �������� ����, ��������� �������

        }

        // ����������� currentSpeed ��� �������� �������
        Vector3 movement = new Vector3(currentSpeed, 0f, 0f);
        transform.Translate(movement * Time.deltaTime);
    }

    
}