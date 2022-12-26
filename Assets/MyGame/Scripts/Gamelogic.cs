using UnityEngine;
using UnityEngine.UI;

public class Gamelogic : MonoBehaviour
{
    public int collected;
    public bool canCollect = true;

    [Header("UI")]
    public Text collectedNumbers;

    private void Update()
    {
        Debug.Log("Collected: " + collected);
        collectedNumbers.text = collected.ToString();
    }

    //for the Unityevent to invoke this Method.
    public void addCollectable()
    {
        if(canCollect) collected++;
    }

    public void MakeCollectTrue()
    {
        canCollect = true;
    }

    public void MakeCollectFalse()
    {
        canCollect = false;
    }
}
