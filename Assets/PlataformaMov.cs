using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMov : MonoBehaviour
{
    [Header("Movimiento entre puntos")]
    [SerializeField] private KeyCode teclaMover = KeyCode.Space; // Tecla que activa el movimiento
    [SerializeField] private Transform puntoA; // Posición inicial
    [SerializeField] private Transform puntoB; // Posición final
    [SerializeField] private float velocidad = 3f;

    [Header("Desaparecer/mostrar objeto")]
    [SerializeField] private KeyCode teclaDesaparecer = KeyCode.F; // Tecla que oculta
    [SerializeField] private bool puedeDesaparecer = false; // Si este objeto tiene esa función

    private Renderer render;
    private Collider col;

    private void Start()
    {
        // Guardamos referencias si debe desaparecer
        if (puedeDesaparecer)
        {
            render = GetComponent<Renderer>();
            col = GetComponent<Collider>();
        }
    }

    private void Update()
    {
        // --- Movimiento entre puntoA y puntoB ---
        if (puntoA != null && puntoB != null)
        {
            Transform destino = Input.GetKey(teclaMover) ? puntoB : puntoA;
            transform.position = Vector3.MoveTowards(transform.position, destino.position, velocidad * Time.deltaTime);
        }

        // --- Desaparecer/mostrar ---
        if (puedeDesaparecer && render != null && col != null)
        {
            if (Input.GetKey(teclaDesaparecer))
            {
                render.enabled = false;
                col.enabled = false;
            }
            else
            {
                render.enabled = true;
                col.enabled = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Dibuja línea entre puntos para que se vea en el editor
        if (puntoA != null && puntoB != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(puntoA.position, puntoB.position);
        }
    }
}
