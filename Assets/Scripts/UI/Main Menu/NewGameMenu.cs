using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine;

public class NewGameMenu : MonoBehaviour
{
    private string worldName;
    public GameObject error;

    public void SetWorldName(string _worldName)
    {
        worldName = _worldName;
    }

    public void CreateNewWorld()
    {
        error.SetActive(false);

        if (!File.Exists(Path.Combine(Application.persistentDataPath, "saves", worldName + ".dat")) && worldName != null){
            DataManagement.saveName = worldName;

            SceneManager.LoadScene(1);
        } else 
        {
            error.SetActive(true);
        }
    }
}
