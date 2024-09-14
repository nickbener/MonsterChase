using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFollow : MonoBehaviour
{
    public Transform player; // Ссылка на объект игрока
    public Joystick joystick; // Ссылка на компонент джойстика
    public PlayerController playerController;
    public float smoothSpeed = 2.0f; // Параметр для плавности движения
    public float minYLimit; // Минимальный предел по оси Y
    public float maxYLimit = 3.0f;  // Максимальный предел по оси Y

    public float baseSpeed = 0.3f; // Стартовая скорость
    public float joysticSpeedModifier; 
    public float normalSpeedModifier; 
    public float scoreSpeedModifier; 
    public float onBiteSpeedModifier; 

    private bool isJoystickHeld = false;
    private bool isNormalModHeld = false;
    private bool isScoreModHeld = false;

    private bool isDKeyHeld = false;

    [HideInInspector] public float currentSpeed; // Текущая скорость

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
            // Получите текущее положение монстра и игрока
            Vector3 currentPosition = transform.position;
            Vector3 playerPosition = player.position;

            // Ограничьте позицию игрока по оси Y
            playerPosition.y = Mathf.Clamp(playerPosition.y, minYLimit, maxYLimit);

            // Установите X монстра равным текущему положению по X
            playerPosition.x = currentPosition.x;

            Vector3 newPositionMons = Vector3.Lerp(currentPosition, playerPosition, smoothSpeed * Time.deltaTime);

            // Установите новую позицию монстра
            transform.position = newPositionMons;
        }

        if (joystick.Horizontal >= 0.5f && !isJoystickHeld)
        {
            currentSpeed += joysticSpeedModifier; // Если джойстик зажат вправо
            isJoystickHeld = true; // Установить флаг, что джойстик зажат
        }
        else if (joystick.Horizontal < 0.5f && isJoystickHeld)
        {
            currentSpeed -= joysticSpeedModifier; // Если джойстик был отпущен
            isJoystickHeld = false; // Сбросить флаг, что джойстик зажат

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
            currentSpeed += normalSpeedModifier; // Если ударился в бревно
            isNormalModHeld = true; // Установить флаг, что ударился в бревно
        }
        else if (playerController.normalSpeedMod == true && isNormalModHeld)
        {
            currentSpeed -= normalSpeedModifier; // Если перестало действовать бревно
            isNormalModHeld = false; // Сбросить флаг, что перестало действовать бревно

        }

        if (playerController.isScoreOnNull == true && !isScoreModHeld)
        {
            currentSpeed += scoreSpeedModifier; // Если закончилась стамина
            isScoreModHeld = true; // Установить флаг, что закончилась стамина
        }
        else if (playerController.isScoreOnNull == false && isScoreModHeld)
        {
            currentSpeed -= scoreSpeedModifier; // Если появилась стамина
            isScoreModHeld = false; // Сбросить флаг, появилась стамина

        }

        // Используйте currentSpeed для движения монстра
        Vector3 movement = new Vector3(currentSpeed, 0f, 0f);
        transform.Translate(movement * Time.deltaTime);
    }

    
}