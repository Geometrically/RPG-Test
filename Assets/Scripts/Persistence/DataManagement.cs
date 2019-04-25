using UnityEngine;

using System.IO;
using System.Xml;
using System.Xml.Serialization;

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
        var serializer = new XmlSerializer(typeof(GameData));

        var data = new GameData();

        data.playerLives = playerLives;
        data.currentLevel = currentLevel;

        data.checkpointX = lastCheckpoint.transform.position.x;
        data.checkpointY = lastCheckpoint.transform.position.y;
        data.checkpointZ = lastCheckpoint.transform.position.z;

        using (var stream = new FileStream(Path.Combine(savesDirectory, saveName + ".xml"), FileMode.Create))
        {
            serializer.Serialize(stream, data);
        }
    }

    public GameData LoadData()
    {
        string loadedFile = Path.Combine(savesDirectory, saveName + ".xml");

        if (File.Exists(loadedFile))
        {
            var serializer = new XmlSerializer(typeof(GameData));

            using (var stream = new FileStream(Path.Combine(savesDirectory, saveName + ".xml"), FileMode.Open))
            {
                return serializer.Deserialize(stream) as GameData;
            }
        }

        throw new FileNotFoundException();
    }
}

[XmlRoot("GameData")]
public class GameData
{
    public int playerLives;
    public string currentLevel;

    public float checkpointX;
    public float checkpointY;
    public float checkpointZ;
}
