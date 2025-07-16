using UnityEngine;

public class Palanca : MonoBehaviour
{
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
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            EjecutarAccion();
        }
    }

    private void EjecutarAccion()
    {
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
            Debug.Log("Jugador cerca de la palanca");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
            Debug.Log("Jugador se alejó de la palanca");
        }
    }
}
