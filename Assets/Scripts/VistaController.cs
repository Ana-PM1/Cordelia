using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VistaController : MonoBehaviour
{
    private EnemyController enemigo;

    private void Start()
    {
        // Busca el controlador del enemigo 
        enemigo = GetComponentInParent<EnemyController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si el objeto que entra en el trigger es el jugador, llama al método de detección del enemigo
        if (other.CompareTag("Player"))
        {
            enemigo.DetectarJugador(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Si el objeto que sale del trigger es el jugador, llama al método de pérdida de jugador del enemigo
        if (other.CompareTag("Player"))
        {
            enemigo.PerderJugador();
        }
    }
}

