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
    [SerializeField] 
    private float tiempoEntreAtaques = 1.5f;
    private float tiempoUltimoAtaque = 0f;


    [Header("Movimiento")]
    [SerializeField]
    private float velocidad = 2f;
    [SerializeField]
    private float rangoVision;

    [Header("Salto")]
    [SerializeField]
    private float rangoSalto;
    


    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    private void Update()
    {
        float distancia = Mathf.Abs(player.position.x - transform.position.x);
        
        if (distancia <= rangoVision){
            MirarJugador();
            Perseguir();

        }
        if(distancia <= rangoAtaque)
        {
            if (Time.time >= tiempoUltimoAtaque + tiempoEntreAtaques)
            {
                Ataque();
            }
        }else
        {
            animator.SetBool("X", false);
        }

        if (distancia <= rangoSalto && distancia > rangoAtaque)
        {
            // Saltar si está en el rango de salto
            Salto();
        }
        else
        {
            // Caminar si está fuera de rango de salto o muy cerca
            animator.SetBool("Y", false);
            Perseguir();
            
        }

    }

    public void TomarDaño(float daño)
    {
        vida -= daño;
        if (vida <= 0)
        {
            Debug.Log("El jefe ha sido derrotado.");
            Muerte();
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

    private void Perseguir()
    {
        Vector3 destino = new Vector3(player.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, destino, velocidad * Time.deltaTime);
    }

    private void Muerte()
    {
        Destroy(gameObject);
    }

    private void Salto()
    {
        if (rb == null) return;
        Vector3 direccion = (player.position - transform.position).normalized;
        Vector3 fuerzaSalto = new Vector3(direccion.x * 3f, 6f, 0); // Puedes ajustar la fuerza horizontal y vertical
        rb.AddForce(fuerzaSalto, ForceMode.Impulse);
        animator.SetBool("Y", true); // Asegúrate de tener un trigger o animación para salto
    }

    

    private void Ataque()
    {
        Collider[] colliders = Physics.OverlapSphere(puntoAtaque.position, rangoAtaque);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                animator.SetBool("X", true);
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
        if (rangoVision != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(puntoAtaque.position, rangoVision);
        }
        if (rangoAtaque != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(puntoAtaque.position, rangoAtaque);
        }
        if (rangoSalto != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(puntoAtaque.position, rangoSalto);
        }
    }



}
