using UnityEngine;

public class Collectables : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Collecting(GameObject.FindWithTag("Player"));
        }
    }

    public void Collecting(GameObject player)
    {
        if (player.transform.childCount == 0)
        {
            transform.SetParent(player.transform);
            GetComponent<Collider2D>().enabled = false;
        }
    }
}