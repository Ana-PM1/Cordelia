using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Este script controla una plataforma que se mueve entre dos puntos (pointA y pointB)
    // La plataforma se activa mediante un script de palanca y se mueve a una velocidad definida
    [Header("Plataforma en Movimiento")]
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    private bool activated = false;
    private Vector3 target;

    void Start()
    {
        target = pointB.position;
    }

    void Update()
    {
        // Si la plataforma no está activada, no se mueve
        // Esto permite que la plataforma solo se mueva cuando se activa mediante una palanca
        if (!activated) return;

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            target = target == pointA.position ? pointB.position : pointA.position;
        }
    }
    // Método para activar la plataforma, llamado por el script de palanca
    // Esto permite que la plataforma comience a moverse cuando se activa la palanca
    public void Activate()
    {
        activated = true;
    }
}
