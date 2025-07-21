using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBalanceo : MonoBehaviour
{
    [Header("Componentes")]
    public Rigidbody rb;
    private SpringJoint cuerda;
    public float fuerzaBalanceo = 5f;
    public Transform puntoDeAnclaje;

    void Update()
    {
        // Si el jugador presiona la tecla E y hay un punto de anclaje, c cuelga :v
        // Si suelta la tecla E, se suélta
        if (Input.GetKeyDown(KeyCode.E) && puntoDeAnclaje != null)
        {
            Colgarse();
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            Soltarse();
        }
        // Si está colgado, balancea
        if (cuerda != null)
        {
            float input = Input.GetAxis("Horizontal");
            rb.AddForce(new Vector3(input * fuerzaBalanceo, 0f, 0f));
        }
    }
    // Cuelga al jugador en el punto de anclaje
    // Añade un SpringJoint para simular el balanceo
    void Colgarse()
    {
        if (cuerda != null) Destroy(cuerda);
        // Añade un SpringJoint al jugador
        // Conecta al punto de anclaje y configura sus propiedades
        cuerda = gameObject.AddComponent<SpringJoint>();
        cuerda.connectedAnchor = puntoDeAnclaje.position;
        cuerda.autoConfigureConnectedAnchor = false;
        cuerda.maxDistance = Vector3.Distance(transform.position, puntoDeAnclaje.position);
        cuerda.spring = 50f;
        cuerda.damper = 5f;
        cuerda.enableCollision = false;
    }
    // Suelta al jugador, destruye el SpringJoint
    void Soltarse()
    {
        if (cuerda != null)
        {
            Destroy(cuerda);
        }
    }
}
