using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    public enum Estado
    {
        Patrullando,
        Persiguiendo,
        Atacando,
        Muerto
    }

    
    [Header("Parámetros generales")]
    [SerializeField] private float velocidad = 2f;
    [SerializeField] private float vidas = 3f;
    [SerializeField] private float dañoAtaque = 1f;
    [SerializeField] private float rangoAtaque = 1f;
    [SerializeField] private float cooldownAtaque = 1f;

    [Header("Puntos de patrulla")]
    [SerializeField] private Transform puntoA;
    [SerializeField] private Transform puntoB;

    [Header("Ataque")]
    [SerializeField] private Transform puntoAtaque;

    private Estado estadoActual = Estado.Patrullando;
    private Transform jugador;
    private Transform destinoActual;
    private float tiempoUltimoAtaque = 0f;

    private void Start()
    {
        destinoActual = puntoB;
    }

    private void Update()
    {
        switch (estadoActual)
        {
            case Estado.Patrullando:
                Patrullar();
                break;
            case Estado.Persiguiendo:
                Perseguir();
                break;
            case Estado.Atacando:
                Atacar();
                break;
            case Estado.Muerto:
                // No hacer nada
                break;
        }

        RevisarTransicionEstado();
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

        
        float direccion = destinoActual.position.x - transform.position.x;
        transform.localScale = new Vector3(Mathf.Sign(direccion), 1, 1);
        
    }

    private void Perseguir()
    {
        if (jugador == null) return;

        Vector3 destino = new Vector3(jugador.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, destino, velocidad * Time.deltaTime);

        float direccion = jugador.position.x - transform.position.x;
        transform.localScale = new Vector3(Mathf.Sign(direccion), 1, 1);
    }

    private void Atacar()
    {
        if (Time.time - tiempoUltimoAtaque >= cooldownAtaque)
        {
            tiempoUltimoAtaque = Time.time;

            Collider[] colliders = Physics.OverlapSphere(puntoAtaque.position, rangoAtaque);
            foreach (Collider col in colliders)
            {
                if (col.CompareTag("Player"))
                {
                    PlayerController player = col.GetComponent<PlayerController>();
                    if (player != null)
                    {
                        player.TakeDamage(dañoAtaque);
                        Debug.Log("¡Ataque enemigo!");
                    }
                }
            }
        }
    }

    private void RevisarTransicionEstado()
    {
        if (estadoActual == Estado.Muerto) return;

        if (jugador != null)
        {
            float distancia = Vector3.Distance(transform.position, jugador.position);

            if (distancia <= rangoAtaque)
                estadoActual = Estado.Atacando;
            else
                estadoActual = Estado.Persiguiendo;
        }
        else
        {
            estadoActual = Estado.Patrullando;
        }
    }


    public void DetectarJugador(Transform jugadorDetectado)
    {
        jugador = jugadorDetectado;
    }

    public void PerderJugador()
    {
        jugador = null;
    }

    public void TakeDamage(float damage)
    {
        vidas -= damage;
        Debug.Log($"{gameObject.name} recibio daño. Vidas restantes: {vidas}");
        if (vidas <= 0 && estadoActual != Estado.Muerto)
        {
            estadoActual = Estado.Muerto;
            Morir();
        }
    }

    private void Morir()
    {
        Debug.Log("Enemigo muerto");
        Destroy(gameObject);
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
