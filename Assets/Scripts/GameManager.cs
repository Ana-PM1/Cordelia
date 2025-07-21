using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    

    public static GameManager Instance; // Global instance to access from other scripts


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape presionado");
            if (SceneManager.GetSceneByName("MenuPausa").isLoaded)
            {
                Debug.Log("Ocultando menú");
                OcultarMenu();
            }
            else
            {
                Debug.Log("Mostrando menú");
                MostrarMenu();
            }
        }
    }

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void CargarNivel(string nombreNivel)
    {
        SceneManager.LoadScene(nombreNivel, LoadSceneMode.Additive);
    }

    public void DesCargarNivel(string currentLevel)
    {
        SceneManager.UnloadSceneAsync(currentLevel);
    }



    [NaughtyAttributes.Button]

    public void SiguienteNivel()
    {
        int escenaCargada = UnityEngine.SceneManagement.SceneManager.sceneCount;
        UnityEngine.SceneManagement.Scene escena = UnityEngine.SceneManagement.SceneManager.GetSceneAt(escenaCargada-1);

        char x = escena.name[escena.name.Length - 1];
        int index = int.Parse(x.ToString())+1; // Assuming the scene name ends with a number
        SceneManager.LoadScene("Nivel" + index);
        
    }
    
    public void MostrarMenu()
    {
        SceneManager.LoadScene("MenuPausa", LoadSceneMode.Additive);
    }

    public void OcultarMenu()
    {
        SceneManager.UnloadSceneAsync("MenuPausa");
    }
    


    
}
