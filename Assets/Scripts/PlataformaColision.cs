using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaColision : MonoBehaviour
{
    
    
    [SerializeField] private Collider platformCollider;

    
    // Este script permite que el jugador se coloque en una plataforma cuando entra en contacto con ella
    // El jugador se convierte en hijo de la plataforma, lo que le permite moverse con ella
    // Cuando el jugador sale de la plataforma, se le quita la relación de padre y se desactiva el hijo
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
                
            platformCollider.isTrigger = true;
            
        }
    }
    // Cuando el jugador sale de la plataforma, se le quita la relación de padre y se desactiva el hijo
    // Esto asegura que el jugador no quede pegado a la plataforma al salir de ella
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            platformCollider.isTrigger = false; // Desactiva el trigger para evitar problemas de colisión
        }
    }
}
