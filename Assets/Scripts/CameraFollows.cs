using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollows : MonoBehaviour
{
    public Transform target; // El jugador
    public Vector3 offset; // Desplazamiento de la cámara

    public float minY = -5f; // Límite inferior
    public float maxY = 10f; // Límite superior

    void LateUpdate()
    {
        // Calcula la nueva posición deseada de la cámara
        Vector3 desiredPosition = target.position + offset;

        // Limita la posición en el eje Y
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, minY, maxY);

        // Suaviza el movimiento de la cámara
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 5);
    }
}
