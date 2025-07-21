using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    

    public static GameManager Instance; // Global instance to access from other scripts
    
    

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

    

    


    
}
