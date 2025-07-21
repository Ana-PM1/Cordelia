using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCuerda : MonoBehaviour
{
    public GameObject cuerda; // El cubo delgado
    private bool jugadorEnRango = false;

    void Update()
    {
        // Solo mostrar la cuerda si el jugador esta dentro del area y mantiene presionada la tecla
        if (jugadorEnRango && Input.GetKey(KeyCode.E))
        {
            cuerda.SetActive(true);
        }
        else
        {
            cuerda.SetActive(false);
        }
    }

    // Detecta cuando el jugador entra al trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador en rango de cuerda");
            jugadorEnRango = true;
        }
    }

    // Detecta cuando el jugador sale del trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnRango = false;
        }
    }
}
