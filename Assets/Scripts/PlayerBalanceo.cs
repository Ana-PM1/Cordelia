using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBalanceo : MonoBehaviour
{
    public Rigidbody rb;
    private SpringJoint cuerda;
    public float fuerzaBalanceo = 5f;
    public Transform puntoDeAnclaje;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && puntoDeAnclaje != null)
        {
            Colgarse();
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            Soltarse();
        }

        if (cuerda != null)
        {
            float input = Input.GetAxis("Horizontal");
            rb.AddForce(new Vector3(input * fuerzaBalanceo, 0f, 0f));
        }
    }

    void Colgarse()
    {
        if (cuerda != null) Destroy(cuerda);

        cuerda = gameObject.AddComponent<SpringJoint>();
        cuerda.connectedAnchor = puntoDeAnclaje.position;
        cuerda.autoConfigureConnectedAnchor = false;
        cuerda.maxDistance = Vector3.Distance(transform.position, puntoDeAnclaje.position);
        cuerda.spring = 50f;
        cuerda.damper = 5f;
        cuerda.enableCollision = false;
    }

    void Soltarse()
    {
        if (cuerda != null)
        {
            Destroy(cuerda);
        }
    }
}
