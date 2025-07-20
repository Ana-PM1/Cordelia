using UnityEngine;
using UnityEngine.Audio;

public class ConfigManager : MonoBehaviour
{
    // Este script gestiona la configuración del juego
    // Permite cargar configuraciones de audio y pantalla al iniciar el juego
    public static ConfigManager Instance;

    public AudioMixer volumeMixer;
    public AudioMixer efectosMixer;

    // Este método se llama al iniciar el juego
    // Carga las configuraciones guardadas en PlayerPrefs
    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Método para cargar configuraciones
    // Se llama desde GameFlowManager al iniciar el juego
    public void CargarConfiguracion()
    {
        float volumen = PlayerPrefs.GetFloat("volumenMusica", -20f);
        volumeMixer.SetFloat("Musica", volumen);

        float fx = PlayerPrefs.GetFloat("volumenEfectos", -20f);
        efectosMixer.SetFloat("Efectos", fx);

        int resolucion = PlayerPrefs.GetInt("resolucion", 0);
        AplicarResolucion(resolucion);

        bool pantallaCompleta = PlayerPrefs.GetInt("pantallaCompleta", 1) == 1;
        Screen.fullScreen = pantallaCompleta;
    }
    // Método para aplicar la resolución según el índice
    // Se utiliza en el menú de opciones para cambiar la resolución del juego
    private void AplicarResolucion(int index)
    {
        switch (index)
        {
            case 0: Screen.SetResolution(1920, 1080, true); break;
            case 1: Screen.SetResolution(1280, 720, false); break;
            case 2: Screen.SetResolution(800, 600, false); break;
        }
    }
}
