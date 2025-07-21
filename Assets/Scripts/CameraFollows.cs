using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollows : MonoBehaviour
{
    public Transform target; // El jugador
    public Vector3 offset; // Desplazamiento de la c�mara

    public float minY = -5f; // L�mite inferior
    public float maxY = 10f; // L�mite superior

    void LateUpdate()
    {
        // Calcula la nueva posici�n deseada de la c�mara
        Vector3 desiredPosition = target.position + offset;

        // Limita la posici�n en el eje Y
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, minY, maxY);

        // Suaviza el movimiento de la c�mara
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 5);
    }
}
