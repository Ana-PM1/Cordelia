using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilidadJefe : MonoBehaviour
{
    
    
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

    public void Golpe()
    {
        Collider[] colisionados = Physics.OverlapSphere(puntoAtaque.position, 0.5f);
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

    public void OnDrawGizmosSelected()
    {
        if (puntoAtaque != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(puntoAtaque.position, 0.5f);
        }
    }


}
