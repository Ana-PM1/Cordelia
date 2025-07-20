using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour
{
    
    [SerializeField] private Button botonContinuar;

    private void Start()
    {
        // Si hay datos guardados, activar botón continuar
        if (GameFlowManager.Instance.HayPartidaGuardada())
        {
            botonContinuar.gameObject.SetActive(true);
        }
        else
        {
            botonContinuar.gameObject.SetActive(false);
        }
    }
    // Método para cargar la escena del juego
    // Si hay datos guardados, carga la escena guardada
    public void Continuar()
    {
        PlayerData data = SaveManager.LoadPlayer();

        if (data != null)
        {
            TransitionManager.Instance.CargarEscenaConTransicion(data.escenaNombre); // nombre de la escena guardada
        }
    }
    // Método para iniciar un nuevo juego
    // Limpia los datos guardados y carga la primera escena del juego
    public void IniciarNuevoJuego()
    {
        SaveManager.BorrarDatos(); // Limpia datos viejos
        TransitionManager.Instance.CargarEscenaConTransicion("Nivel1"); // nombre exacto de tu primer escena
    }
    // Método para salir del juego
    public void SalirJuego()
    {
        Application.Quit();
        Debug.Log("Juego cerrado.");
    }
    
}
