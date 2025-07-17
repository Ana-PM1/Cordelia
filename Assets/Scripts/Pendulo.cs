using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulo : MonoBehaviour
{
    
    public Transform puntoA; // Va con el jugador
    public Transform puntoB; // El target

    void Update()
    {
        if (puntoA != null && puntoB != null)
        {
            // Posicionar la cuerda entre los dos puntos
            transform.position = (puntoA.position + puntoB.position) / 2f;

            // Calcular la distancia para escalar la cuerda
            float distancia = Vector3.Distance(puntoA.position, puntoB.position);
            transform.localScale = new Vector3(0.1f, 0.1f, distancia);

            // Rotar la cuerda para que apunte al target
            transform.LookAt(puntoB);
        }
    }
}
