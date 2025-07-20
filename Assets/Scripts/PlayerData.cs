[System.Serializable]
public class PlayerData
{
    public int vidas;
    public float[] posicion = new float[3];
    public int escenaIndex;
    public string escenaNombre;
    
    // Constructor para inicializar datos del jugador
    public PlayerData(PlayerController playerController)
    {
        // Asignar datos del jugador
        vidas = (int)playerController.vidas;
        posicion[0] = playerController.transform.position.x;
        posicion[1] = playerController.transform.position.y;
        posicion[2] = playerController.transform.position.z;
        escenaIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        escenaNombre = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
    }
}
