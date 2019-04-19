using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerState : MonoBehaviour
{
    public int respawnCount = 3;

    private Checkpoint lastCheckpoint;

    public void Start()
    {
        //Load Data
        GameData gameData = DataManagement.dataManagement.LoadData();

        respawnCount = gameData.playerLives;

        var checkpointPos = new Vector3(gameData.checkpointX, gameData.checkpointY, gameData.checkpointZ);

        transform.position = checkpointPos;
    }
    
    public void SetCheckpoint(Checkpoint checkpoint)
    {
        lastCheckpoint = checkpoint;
        DataManagement.dataManagement.SaveData(respawnCount, SceneManager.GetActiveScene().name, checkpoint);
    }

    public void Respawn()
    {
        DataManagement.dataManagement.SaveData(respawnCount - 1, SceneManager.GetActiveScene().name, lastCheckpoint);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
