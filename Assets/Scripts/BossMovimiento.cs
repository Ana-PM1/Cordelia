using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossMovimiento : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;

    public Transform player;
    private bool mirandoIzquierda = true;

    [Header("Vida")]
    [SerializeField]
    private float vida;
    /*[SerializeField]
    private BarraVida barraVida;*/
    [Header("Ataque")]
    [SerializeField]
    private Transform puntoAtaque;
    [SerializeField]
    private float rangoAtaque;
    [SerializeField]
    private float dañoAtaque;



    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void TomarDaño(float daño)
    {
        vida -= daño;
        if (vida <= 0)
        {
            Debug.Log("El jefe ha sido derrotado.");
            //animator.SetTrigger("Morir");
        }
        /*if (barraVida != null)
        {
            barraVida.ActualizarBarra(vidaMaxima);
        }*/
    }

    public void MirarJugador()
    {
        if ((player.position.x < transform.position.x && mirandoIzquierda) || 
            (player.position.x > transform.position.x && !mirandoIzquierda))
        {
         
            mirandoIzquierda = !mirandoIzquierda;
            transform.eulerAngles = new Vector3(0, mirandoIzquierda ? 180 : 0, 0);
        }
        
    }

    private void Muerte()
    {
        Destroy(gameObject);
    }

    

    private void Ataque()
    {
        Collider[] colliders = Physics.OverlapSphere(puntoAtaque.position, rangoAtaque);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                PlayerController playerController = collider.GetComponent<PlayerController>();
                if (playerController != null)
                {
                    playerController.TakeDamage(dañoAtaque);
                    Debug.Log("Atacando al jugador con xmov");
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (puntoAtaque != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(puntoAtaque.position, rangoAtaque);
        }
    }



}
