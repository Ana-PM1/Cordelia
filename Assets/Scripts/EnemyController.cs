using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float velocidad = 2f;
    [SerializeField] private float vidas = 2f;
    [SerializeField] private Transform puntoA;
    [SerializeField] private Transform puntoB;

    private Transform jugador;
    private bool persiguiendo = false;

    private Transform destinoActual;

    private void Start()
    {
        destinoActual = puntoB;
    }

    private void Update()
    {
        if (persiguiendo && jugador != null)
        {
            Vector3 destino = new Vector3(jugador.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, destino, velocidad * Time.deltaTime);
        }
        else
        {
            Patrullar();
        }
    }

    private void Patrullar()
    {
        if (destinoActual == null) return;

        Vector3 destino = new Vector3(destinoActual.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, destino, velocidad * Time.deltaTime);

        float distancia = Mathf.Abs(transform.position.x - destinoActual.position.x);
        if (distancia < 0.1f)
        {
            // Cambiar de direccin
            destinoActual = (destinoActual == puntoA) ? puntoB : puntoA;
        }

        if (destinoActual != null)
        {
            float direccion = destinoActual.position.x - transform.position.x;
            if (direccion != 0)
                transform.localScale = new Vector3(Mathf.Sign(direccion), 1, 1);
        }
    }

    public void DetectarJugador(Transform jugadorDetectado)
    {
        jugador = jugadorDetectado;
        persiguiendo = true;
    }

    public void PerderJugador()
    {
        jugador = null;
        persiguiendo = false;
    }

    public void TakeDamage(float damage)
    {
        vidas -= damage;
        Debug.Log($"{gameObject.name} recibio daño. Vidas restantes: {vidas}");
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
}
