using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveSystem
{ 
    public static void SaveGameData(SaveData savedata)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/gameData.data";

        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(savedata);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SaveSettingsData(SettingManager setting)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/settings.ini";

        FileStream stream = new FileStream(path, FileMode.Create);

        SettingData data = new SettingData(setting);

        formatter.Serialize(stream, data);
        stream.Close();
    }


    public static GameData LoadGameData()
    {
        string path = Application.persistentDataPath + "/gameData.data";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = (GameData)formatter.Deserialize(stream);
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Save File not found! "  + path);
            return null;
        }
    }

    public static SettingData LoadSettingData()
    {
        string path = Application.persistentDataPath + "/settings.ini";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SettingData data = (SettingData)formatter.Deserialize(stream);
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Setings not configured! " + path);
            return null;
        }
    }
}
