using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.IO;

using TMPro;

public class LoadGameMenu : MonoBehaviour
{
    public GameObject scrollView;
    public Button gameItemPrefab;

    public void Start()
    {
        var offset = 0f;

        foreach(var file in Directory.EnumerateFiles(Path.Combine(Application.persistentDataPath, "saves"))){

            var entry = Instantiate(gameItemPrefab, scrollView.transform);
            entry.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 142.5f - offset, 0);

            offset += 13.5f;

            entry.transform.Find("SaveName").GetComponent<TextMeshProUGUI>().text = Path.GetFileNameWithoutExtension(file);
            entry.onClick.AddListener(delegate {
                DataManagement.saveName = Path.GetFileNameWithoutExtension(file);

                SceneManager.LoadScene(DataManagement.dataManagement.LoadData().currentLevel);
            });

        }
    }
}
