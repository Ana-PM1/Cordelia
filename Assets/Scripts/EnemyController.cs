using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Este script controla el comportamiento de un enemigo en el juego
    // El enemigo patrulla entre dos puntos, persigue al jugador y ataca si está cerca
    // También maneja la detección del jugador y la pérdida de su rastro

    // Enumeración para los estados del enemigo
    // Define los diferentes estados que puede tener el enemigo
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
    [SerializeField] protected float cooldownAtaque = 1f;

    [Header("Puntos de patrulla")]
    [SerializeField] private Transform puntoA;
    [SerializeField] private Transform puntoB;

    [Header("Ataque")]
    [SerializeField] protected Transform puntoAtaque;

    protected Estado estadoActual = Estado.Patrullando;
    protected Transform jugador;
    private Transform destinoActual;
    protected float tiempoUltimoAtaque = 0f;

    private void Start()
    {
        destinoActual = puntoB;
    }
    // Este método se llama en cada frame para actualizar el comportamiento del enemigo
    // Dependiendo del estado actual, el enemigo patrulla, persigue al jugador o ataca
    // También revisa las transiciones entre estados para cambiar el comportamiento según la situación
    protected virtual void Update()
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
    // Métodos privados para manejar el comportamiento del enemigo
    // Estos métodos se encargan de patrullar entre dos puntos, perseguir al jugador y atacar cuando está cerca
    // Cada método actualiza la posición del enemigo y verifica si debe cambiar de estado
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
        // transform.localScale = new Vector3(Mathf.Sign(direccion), 1, 1);
        transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, Mathf.Sign(direccion) > 0 ? 0 : 180, 0 );


        
    }
    // Este método se encarga de perseguir al jugador
    // Si el jugador está dentro del rango de ataque, cambia al estado de ataque
    private void Perseguir()
    {
        if (jugador == null) return;

        Vector3 destino = new Vector3(jugador.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, destino, velocidad * Time.deltaTime);

        float direccion = jugador.position.x - transform.position.x;
        transform.localScale = new Vector3(Mathf.Sign(direccion), 1, 1);
    }
    // Este método se encarga de atacar al jugador si está dentro del rango de ataque
    // Utiliza Physics.OverlapSphere para detectar colisiones en un área alrededor del punto de ataque
    // Si el jugador está dentro de esta área, se le aplica el daño
    protected virtual void Atacar()
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
    // Este método revisa las condiciones para cambiar el estado del enemigo
    // Si el jugador está cerca, cambia al estado de ataque
    // Si el jugador se aleja, cambia al estado de persecución o patrullaje
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

    // Métodos públicos para detectar y perder al jugador
    // Estos métodos permiten que el enemigo detecte al jugador cuando entra en su rango de visión
    public void DetectarJugador(Transform jugadorDetectado)
    {
        jugador = jugadorDetectado;
    }
    // Este método se llama cuando el jugador sale del rango de visión del enemigo
    // Permite que el enemigo pierda el rastro del jugador y vuelva a patrullar
    public void PerderJugador()
    {
        jugador = null;
    }
    // Método para aplicar daño al enemigo
    // Reduce las vidas del enemigo y verifica si ha muerto
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
    // Método para manejar la muerte del enemigo
    // Destruye el objeto del enemigo y emite un mensaje 
    private void Morir()
    {
        Debug.Log("Enemigo muerto");
        Destroy(gameObject);
    }
    // Método para dibujar el área de ataque en el editor
    // Esto ayuda a visualizar el área de ataque del enemigo durante el desarrollo
    private void OnDrawGizmosSelected()
    {
        if (puntoAtaque != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(puntoAtaque.position, rangoAtaque);
        }
    }
}
