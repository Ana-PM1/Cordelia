using UnityEngine;

public class PruebaScene : MonoBehaviour
{
    public string nomScene;

    public void CargarEscena()
    {
        TransitionManager.Instance.CargarEscenaConTransicion(nomScene); // nombre de la escena a cargar
    }
}
