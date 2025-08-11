using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLight : MonoBehaviour
{
    [SerializeField] private Transform objetivo; // el jugador
    [SerializeField] private Vector3 offset = new Vector3(0, 5, -5); // ajusta según tu escena

    void LateUpdate()
    {
        if (objetivo == null) return;

        // Mover la luz con un offset respecto al jugador
        transform.position = objetivo.position + offset;

        // Hacer que mire siempre al jugador
        //transform.LookAt(objetivo.position);
    }
}
