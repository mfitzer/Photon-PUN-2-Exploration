using UnityEngine;

public class XRInputActionTest : MonoBehaviour
{
    public void button()
    {
        Debug.Log("button");
    }

    public void axis(float value)
    {
        Debug.Log("axis: " + value);
    }

    public void axis2DValued(Vector2 value)
    {
        Debug.Log("axis 2D valued: " + value);
    }

    public void axis2DDirectional()
    {
        Debug.Log("axis 2D directional");
    }
}
