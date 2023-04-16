using UnityEngine;

public class DiceSide : MonoBehaviour
{
    bool onGround;
    public int sideValue;
    //public int sideValue1;

    void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Ground"))
        {
            onGround = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Ground"))
        {
            onGround = false;
        }
    }

    public bool OnGround()
    {
        return onGround;
    }
}
