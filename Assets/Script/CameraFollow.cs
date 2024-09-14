using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // ������ �� ������, �� ������� ����� �������
    public float smoothSpeed;  // ��������� ���������� ������
    public Vector3 offset;  // �������� ������ ������������ ����

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        desiredPosition.y = transform.position.y; // ������������ ���������� �� ��� Y
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
