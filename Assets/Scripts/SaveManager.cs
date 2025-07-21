using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager 
{
    

    // Guarda los datos del jugador en un archivo
    public static void SavePlayer(PlayerController playerController)
    {
        PlayerData playerData = new PlayerData(playerController);// Crea una instancia de PlayerData con los datos del jugador
       
        string pathData = Application.persistentDataPath + "/player.save";// Ruta donde se guardará el archivo
        FileStream stream = new FileStream(pathData, FileMode.Create);// Crea o sobreescribe el archivo
        BinaryFormatter formatter = new BinaryFormatter();// Crea un formateador binario para serializar los datos

        formatter.Serialize(stream, playerData);// Serializa los datos del jugador
        stream.Close();
    }

    // Carga los datos del jugador desde un archivo
    public static PlayerData LoadPlayer()
    {
        string pathData = Application.persistentDataPath + "/player.save";
        if (File.Exists(pathData))
        {
            FileStream stream = new FileStream(pathData, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            PlayerData playerData = formatter.Deserialize(stream) as PlayerData;// Deserializa los datos del jugador
            stream.Close();
            return playerData;
        }
        else
        {
            Debug.LogError("Archivo no enconardo " + pathData);
            return null;
        }
    }

    // Verifica si hay una partida guardada
    public static bool ExistePartidaGuardada()
    {
        string pathData = Application.persistentDataPath + "/player.save";
        return File.Exists(pathData);
    }
    // Borra los datos guardados del jugador
    public static void BorrarDatos()
    {
        string pathData = Application.persistentDataPath + "/player.save";
        if (File.Exists(pathData))
        {
            File.Delete(pathData);
        }
    }
}
