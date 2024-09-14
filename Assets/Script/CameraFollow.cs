using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // Ссылка на объект, за которым нужно следить
    public float smoothSpeed;  // Плавность следования камеры
    public Vector3 offset;  // Смещение камеры относительно цели

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        desiredPosition.y = transform.position.y; // Ограничиваем следование по оси Y
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
