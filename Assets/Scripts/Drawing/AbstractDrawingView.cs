using UnityEngine;

public abstract class AbstractDrawingView : MonoBehaviour
{
    public event System.Action<bool> PointerDown;

    protected void OnPointerDown(bool isDown)
    {
        if (PointerDown != null)
        {
            PointerDown(isDown);
        }
    }

    public abstract void InitView();

    public abstract void MoveGraphiicObjects();
}
