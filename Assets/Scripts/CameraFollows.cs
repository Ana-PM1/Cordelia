using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollows : MonoBehaviour
{
    public Transform target; // El jugador
    public Vector3 offset; // Desplazamiento de la cámara
    void LateUpdate()
    {
        // Calcula la nueva posición de la cámara
        Vector3 desiredPosition = target.position + offset;
        // Suaviza el movimiento de la cámara
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 5);
    }
}
