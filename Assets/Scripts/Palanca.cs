using UnityEngine;

public class Palanca : MonoBehaviour
{
    // Son estados que controla el comportamiento de una palanca que activa o desactiva objetos o plataformas en el juego
    [Header("Configuración de la Palanca")]
    public enum TipoAccion
    {
        ActivarObjeto,
        DesactivarObjeto,
        ActivarPlataforma
    }

    [SerializeField] 
    private TipoAccion accion;
    
    [SerializeField] 
    private GameObject objetivo;

    private bool jugadorCerca = false;

    private void Update()
    {
        // Si el jugador está cerca de la palanca y presiona la tecla E, ejecuta la acción
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            EjecutarAccion();
        }
    }

    // Dependiendo del tipo de acción, activa o desactiva el objeto o plataforma objetivo
    private void EjecutarAccion()
    {
        // Verifica que el objetivo no sea nulo antes de intentar activarlo o desactivarlo
        switch (accion)
        {
            case TipoAccion.ActivarObjeto:
                if (objetivo != null) objetivo.SetActive(true);
                break;

            case TipoAccion.DesactivarObjeto:
                if (objetivo != null) objetivo.SetActive(false);
                break;

            case TipoAccion.ActivarPlataforma:
                if (objetivo != null)
                {
                    var plataforma = objetivo.GetComponent<MovingPlatform>();
                    if (plataforma != null) plataforma.Activate();
                }
                break;
        }

        Debug.Log($"Palanca activó: {accion} en {objetivo.name}");
    }
    // Detecta si el jugador entra o sale del área de activación de la palanca
    // Esto permite que el jugador interactúe con la palanca solo cuando está cerca
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
            Debug.Log("Jugador cerca de la palanca");
        }
    }
    // Cuando el jugador sale del área de activación, se desactiva la posibilidad de interactuar con la palanca
    // Esto evita que el jugador pueda activar la palanca sin estar cerca
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
            Debug.Log("Jugador se alejó de la palanca");
        }
    }
}
