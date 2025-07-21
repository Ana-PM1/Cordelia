using UnityEngine;

public class PruebaScene : MonoBehaviour
{
    public string nomScene;
    public GameObject canvasCarga;

    public void CargarEscena()
    {
        TransitionManager.Instance.CargarEscenaConTransicion(nomScene); // nombre de la escena a cargar
    }

    public void Start()
    {
        TransitionManager.Instance.canvasCarga = canvasCarga; // Asigna el canvas de carga al TransitionManager
    }
}
