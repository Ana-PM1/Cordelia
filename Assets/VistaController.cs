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
        if (other.CompareTag("Player"))
        {
            enemigo.DetectarJugador(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemigo.PerderJugador();
        }
    }
}

