using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    

    public static GameManager Instance; // Global instance to access from other scripts
    public string[] escenasPermitidas = { "Nivel1", "Nivel2", "Nivel3" };
    


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && EstaEnNivel())
        {
            if (SceneManager.GetSceneByName("MenuPausa").isLoaded)
            {
                OcultarMenu();
            }
            else
            {
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

    bool EstaEnNivel()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            string nombre = SceneManager.GetSceneAt(i).name;

            if (nombre.StartsWith("Nivel")) // Ej: Nivel1, Nivel2, etc.
            {
                return true;
            }
        }
        return false;
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
