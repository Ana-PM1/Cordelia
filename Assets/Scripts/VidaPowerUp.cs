using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaPowerUp : MonoBehaviour
{
    [SerializeField] 
    private float cantidadVida = 1f; // Cuánta vida le da al jugador

    private void OnTriggerEnter(Collider other)
    {
        PlayerController jugador = other.GetComponent<PlayerController>();
        if (jugador != null)
        {
            jugador.GanarVida(cantidadVida);
            Debug.Log("Power-up de vida recogido");
            Destroy(gameObject); // Destruir el power-up después de recogerlo
        }
    }
}
