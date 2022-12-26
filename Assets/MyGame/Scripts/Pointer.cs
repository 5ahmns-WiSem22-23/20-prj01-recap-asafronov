using UnityEngine;
using UnityEngine.UI;

public class Pointer : MonoBehaviour
{
    public float distance = 5;
    public Collectables collectable;

    private void Update()
    {
        collectable = FindObjectOfType<Collectables>();

        Transform pickup = collectable.transform;

        var img = GetComponentInChildren<Image>();
        img.enabled = Vector3.Distance(transform.position, pickup.position) > distance;

        Vector3 dir = pickup.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
