using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnCuerda : MonoBehaviour
{
    // Este script permite al jugador interactuar con una cuerda
    // Cuando el jugador entra en el trigger, puede agarrar la cuerda


    [SerializeField] private bool isOnRope = false;
    [SerializeField] private Transform techo;
    [SerializeField] private Transform player;
    
    
    void Start()
    {
        
    }

    // Este método se llama cada frame para verificar si el jugador está en la cuerda
    // Si el jugador está en la cuerda y supera la altura del techo, se ajusta su posición
    // y se desactiva la gravedad para que pueda deslizarse por la cuerda
    void Update()
    {
        if(isOnRope && player.position.y > techo.position.y)
        {
            player.position = techo.position;
            isOnRope = false;
            player.GetComponent<PlayerController>().isOnRope = false;
            player.GetComponent<Rigidbody>().useGravity = true;
        }
       
    }
    // Este método se llama cuando el jugador entra en el trigger de la cuerda
    // Si el jugador presiona el botón de acción y no está en la cuerda, se activa la cuerda
    // Si el jugador ya está en la cuerda, se desactiva la cuerda
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") )
        {
            if (Input.GetButtonDown("Action") && !isOnRope)
            {
                player = other.transform;
                isOnRope = true;
                other.GetComponent<PlayerController>().isOnRope = true; 
                other.transform.SetParent(transform);
                Rigidbody rb = other.GetComponent<Rigidbody>();
                if (rb != null)
                {

                    rb.useGravity = false;

                }
                if(player.position.y > techo.position.y)
                {
                    player.position = new Vector3(transform.position.x, techo.position.y - .1f, transform.position.z);
                }
                else
                {
                    player.position = new Vector3(transform.position.x, player.position.y + .1f, transform.position.z);
                }
            }
            else if (Input.GetButtonDown("Action") && isOnRope)
            {
                player = null;
                isOnRope = false;
                other.GetComponent<PlayerController>().isOnRope = false; 
                other.transform.SetParent(null);
                Rigidbody rb = other.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.useGravity = true;

                }
            }
        }
    }
}
