using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager 
{
    public static void SavePlayer(PlayerController playerController)
    {
        PlayerData playerData = new PlayerData(playerController);
       
        string pathData = Application.persistentDataPath + "/player.save";
        FileStream stream = new FileStream(pathData, FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();

        formatter.Serialize(stream, playerData);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string pathData = Application.persistentDataPath + "/player.save";
        if (File.Exists(pathData))
        {
            FileStream stream = new FileStream(pathData, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            PlayerData playerData = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return playerData;
        }
        else
        {
            Debug.LogError("Save file not found in " + pathData);
            return null;
        }
    }

    public static void BorrarDatos()
    {
        string pathData = Application.persistentDataPath + "/player.save";
        if (File.Exists(pathData))
        {
            File.Delete(pathData);
        }
    }
}
