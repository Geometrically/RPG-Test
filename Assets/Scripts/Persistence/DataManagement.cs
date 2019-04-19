using UnityEngine;

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataManagement : MonoBehaviour
{
    public static DataManagement dataManagement;
    public static string saveName;

    private string savesDirectory;

    private void Awake()
    {
        if (dataManagement == null)
        {
            DontDestroyOnLoad(gameObject);
            dataManagement = this;
        }
        else if (dataManagement != this)
        {
            Destroy(gameObject);
        }

        savesDirectory = Path.Combine(Application.persistentDataPath, "saves");
        
        if (!Directory.Exists(savesDirectory))
            Directory.CreateDirectory(savesDirectory);
    }

    public void SaveData(int playerLives, string currentLevel, Checkpoint lastCheckpoint)
    {
        var binaryFormatter = new BinaryFormatter();
        var file = new FileStream(Path.Combine(savesDirectory, saveName + ".dat"), FileMode.OpenOrCreate);

        var data = new GameData();

        data.playerLives = playerLives;
        data.currentLevel = currentLevel;

        data.checkpointX = lastCheckpoint.transform.position.x;
        data.checkpointY = lastCheckpoint.transform.position.y;
        data.checkpointZ = lastCheckpoint.transform.position.z;

        binaryFormatter.Serialize(file, data);
        file.Close();
    }

    public GameData LoadData()
    {
        string loadedFile = Path.Combine(Application.persistentDataPath, "saves", saveName + ".dat");

        if (File.Exists(loadedFile))
        {
            var binaryFormatter = new BinaryFormatter();
            var file = File.Open(loadedFile, FileMode.Open);

            var gameData = (GameData)binaryFormatter.Deserialize(file);
            file.Close();

            return gameData;
        }

        throw new FileNotFoundException();
    }
}

[Serializable]
public class GameData
{
    public int playerLives;
    public string currentLevel;

    public float checkpointX;
    public float checkpointY;
    public float checkpointZ;
}
