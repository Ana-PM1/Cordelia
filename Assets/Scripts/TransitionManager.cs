using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance;

    // Referencias a los elementos de UI para la transición
    [Header("Elementos de UI de carga")]
    public GameObject canvasCarga;       
    public Text loadingText;
    
    //public GameObject canvasViejo;
    

    private void Awake()
    {
        // Singleton
        // Asegura que solo haya una instancia de TransitionManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        // Inicializa el canvas de carga
        if (canvasCarga != null)
            canvasCarga.SetActive(false); // Se inicia oculto
    }

    

    public void CargarEscenaConTransicion(string nombreEscena)
    {
        // Inicia la corrutina para cargar la escena con transición
        StartCoroutine(CargarEscena(nombreEscena));
    }

    public void DesCargarEscenasAnteriores(string nombreEscena)
        {
            // Desactiva las escenas anteriores excepto la actual
            StartCoroutine(DesactivarEscenas(nombreEscena));
        }
    private IEnumerator DesactivarEscenas(string nombreEscena)
    {
        // Espera un momento antes de desactivar las escenas
        yield return new WaitForSeconds(0.1f);

        // Desactiva todas las escenas excepto la que se está cargando
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene escena = SceneManager.GetSceneAt(i);
            if (escena.name != nombreEscena)
            {
                yield return SceneManager.UnloadSceneAsync(escena);
            }
        }
    }

    // Corrutina para cargar la escena con una transición
    private IEnumerator CargarEscena(string nombreEscena)
    {
        if (canvasCarga != null)
            canvasCarga.SetActive(true);
            //canvasViejo.SetActive(false); 

        if (loadingText != null)
            loadingText.text = "Cargando...";

       

        yield return new WaitForSeconds(4f);// Simula un tiempo de carga

        // Desactiva el canvas de carga si está activo
        
        AsyncOperation operacion = SceneManager.LoadSceneAsync(nombreEscena, LoadSceneMode.Additive);
        // Carga la nueva escena de forma asíncrona y aditiva
        operacion.allowSceneActivation = false; // Evita que la escena se active inmediatamente

        // Espera hasta que la operación de carga esté completa
        while (!operacion.isDone)
        {
            float progreso = Mathf.Clamp01(operacion.progress / 0.9f);

            
            // Actualiza el texto de carga
            if (operacion.progress >= 0.9f)
            {
                yield return new WaitForSeconds(0.5f);
                operacion.allowSceneActivation = true;
                
                // Scene nuevaEscena = SceneManager.GetSceneByName(nombreEscena);// Verifica si la escena se ha cargado correctamente
                // if (nuevaEscena.IsValid())
                // {
                //     SceneManager.SetActiveScene(nuevaEscena);
                // }
            }

            yield return null;
    
        }
        

        

        if (SceneManager.sceneCount > 1)
        {
            Debug.Log("Desactivando escenas anteriores");
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene escena = SceneManager.GetSceneAt(i);
                // Desactiva el canvas de carga si la escena no es "Sistema"
                if (escena.name != "SistemaBase" && escena.name != nombreEscena)
                    yield return SceneManager.UnloadSceneAsync(escena);

            }
        }
        if (canvasCarga != null)
            canvasCarga.SetActive(false); // Oculta el canvas de carga
    }

}
