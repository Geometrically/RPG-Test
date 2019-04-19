using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool isClaimed = false;

    public Sprite claimedSprite;

    public GameObject[] enemiesToDelete;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isClaimed)
        {
            collision.gameObject.GetComponent<PlayerState>().SetCheckpoint(this);

            isClaimed = true;
            GetComponent<SpriteRenderer>().sprite = claimedSprite;
        }
    }
}
