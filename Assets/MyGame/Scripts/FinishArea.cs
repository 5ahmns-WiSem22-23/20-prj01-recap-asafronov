using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FinishArea : MonoBehaviour
{
    public UnityEvent WhenInsideArea;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Collectable.canCollect == false)
        {
            WhenInsideArea.Invoke();
        }
    }
}