using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArquero : EnemyController
{
    // Este script controla el comportamiento específico de un enemigo arquero
    // El arquero puede atacar al jugador desde una distancia, además de patrullar y perseguir

    [Header("Parámetros específicos del arquero")]
    [SerializeField] private float rangoDisparo = 10f; // Rango máximo para disparar flechas
    [SerializeField] private GameObject flechaPrefab; // Prefab de la flecha que el arquero dispara
    [SerializeField] private Transform puntoDisparo; // Punto desde donde se dispara la flecha

    private void Update()
    {
        base.Update(); // Llama al método Update de la clase base para manejar estados comunes

        if (estadoActual == Estado.Persiguiendo && Vector3.Distance(transform.position, jugador.position) <= rangoDisparo)
        {
            estadoActual = Estado.Atacando;
        }
    }

    protected override void Atacar()
    {
        if (Time.time >= tiempoUltimoAtaque + cooldownAtaque)
        {
            DispararFlecha();
            tiempoUltimoAtaque = Time.time;
        }
    }

    private void DispararFlecha()
    {
        GameObject flecha = Instantiate(flechaPrefab, puntoDisparo.position, Quaternion.identity);
        Vector3 direccion = (jugador.position - puntoDisparo.position).normalized;
        flecha.GetComponent<Rigidbody>().AddForce(direccion * 10f, ForceMode.Impulse);
    }

    
}

