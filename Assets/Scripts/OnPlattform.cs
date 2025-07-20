using UnityEngine;

public class OnPlattform : MonoBehaviour
{
    
    
    [SerializeField] private Transform platform;
    [SerializeField] private GameObject hijo;
    // Este script permite que el jugador se coloque en una plataforma cuando entra en contacto con ella
    // El jugador se convierte en hijo de la plataforma, lo que le permite moverse con ella
    // Cuando el jugador sale de la plataforma, se le quita la relación de padre y se desactiva el hijo
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.transform.position.y > platform.position.y +  0.2f)
            {
                other.transform.SetParent(platform);
                hijo.SetActive(true);
            }
        }
    }
    // Cuando el jugador sale de la plataforma, se le quita la relación de padre y se desactiva el hijo
    // Esto asegura que el jugador no quede pegado a la plataforma al salir de ella
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
            hijo.SetActive(false);
        }
    }
}
