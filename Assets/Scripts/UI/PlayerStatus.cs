using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public Image healthBar;

    private float initialHealth;
    private float currentHealth;

    public GameObject[] respawnHearts;

    private GameObject player;

    private int oldRespawns = 3;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        initialHealth = player.GetComponent<DamagableObject>().health;
    }

    private void Update()
    {
        currentHealth = player.GetComponent<DamagableObject>().health;

        healthBar.rectTransform.localScale = currentHealth / initialHealth >= 0 ? new Vector3(currentHealth / initialHealth, 1, 1) : new Vector3(0, 1, 1);

        //Check for Respawn Count Change
        if(player.GetComponent<PlayerState>().respawnCount != oldRespawns)
        {
            SetRespawns(player.GetComponent<PlayerState>().respawnCount);
        }

        oldRespawns = player.GetComponent<PlayerState>().respawnCount;
    }

    public void SetRespawns(int respawns)
    {
        for (int i = 1; i <= (3 - respawns); i++)
        {
            respawnHearts[3 - i].SetActive(false);
        }
    }
}
