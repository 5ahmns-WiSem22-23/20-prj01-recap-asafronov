using UnityEngine;

public class Gamelogic : MonoBehaviour
{
    public int collected;
    public bool canCollect = true;

    private void Update()
    {
        Debug.Log("Collected: " + collected);
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
