using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveData(GetData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/data.TotallyNotData";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData gameData = new GameData(data);

        formatter.Serialize(stream, gameData);
        stream.Close();
    }

    public static GameData LoadData()
    {
        string path = Application.persistentDataPath + "/data.TotallyNotData";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData gameData = formatter.Deserialize(stream) as GameData;
            stream.Close();

            return gameData;
        }
        else
        {
            Debug.Log("Data not found in " + path);
            return null;
        }
    }

    public static void WipeData()
    {
        string path = Application.persistentDataPath + "/data.TotallyNotData";

        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Save data wiped.");
        }
        else
        {
            Debug.Log("No save data found to wipe.");
        }
    }
}
