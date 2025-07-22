using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilidadJefe : MonoBehaviour
{
    

    // Este script controla la habilidad de ataque del jefe
    // Se encarga de infligir daño al jugador cuando entra en contacto con el área de ataque
    // La habilidad se activa al llamar al método Golpe, que verifica si el jugador esta en el área de ataque
    // El daño se aplica al jugador y se destruye el objeto de ataque después de un tiempo definido
    [Header("Habilidad del Jefe")]
    [SerializeField]
    private float daño;

    [SerializeField]
    private Vector3 direccionAtaque;

    [SerializeField]
    private Transform puntoAtaque;

    [SerializeField]
    private float tiempo;


    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, tiempo);
    }
    // Este método se llama para infligir daño al jugador
    // Utiliza Physics.OverlapSphere para detectar colisiones en un área alrededor del punto de ataque
    // Si el jugador está dentro de esta área, se le aplica el daño
    public void Golpe()
    {
        Collider[] colisionados = Physics.OverlapSphere(puntoAtaque.position, 0.5f);// Define el área de ataque como un círculo con un radio de 0.5 unidades
        // Recorre todos los colliders en el área de ataque
        foreach (Collider col in colisionados)
        {
            if (col.CompareTag("Player"))
            {
                PlayerController player = col.GetComponent<PlayerController>();
                if (player != null)
                {
                    player.vidas -= daño;
                    Debug.Log("Jugador golpeado por el jefe, daño: " + daño);
                }
            }
        }
    }
    // Método para dibujar el área de ataque en el editor
    // Esto ayuda a visualizar el área de ataque del jefe durante el desarrollo
    public void OnDrawGizmosSelected()
    {
        if (puntoAtaque != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(puntoAtaque.position, 0.5f);
        }
    }


}
