using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    
    [SerializeField]
    private float vidas = 2f;

    [SerializeField] 
    private Transform vista;
   
    [SerializeField] 
    private NavMeshAgent agent;
    
    [SerializeField] 
    private Transform jugador;

    private bool persiguiendo = false;
    
    private void Start()
    {
        // Asegurarse de que no se mueva en Y ni Z
        agent.updateUpAxis = false;
        agent.updateRotation = false;
    }

    private void Update()
    {
        if (persiguiendo && jugador != null)
        {
            Vector3 destino = new Vector3(jugador.position.x, transform.position.y, transform.position.z);
            agent.SetDestination(destino);
        }
    }

    public void TakeDamage(float damage)
    {
        vidas -= damage;
        Debug.Log($"{gameObject.name} recibió daño. Vidas restantes: {vidas}");
        if (vidas <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy died");
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugador = other.transform;
            persiguiendo = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            persiguiendo = false;
            agent.ResetPath();
        }
    }
}
