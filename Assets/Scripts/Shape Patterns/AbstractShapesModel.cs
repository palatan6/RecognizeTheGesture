using UnityEngine;

public abstract class AbstractShapesModel : MonoBehaviour
{
    public abstract void SetNewCurrentShapePattern();

    public abstract void CheckUserEneterdPoints(Vector2[] points);

    public event System.Action<bool> CurrentShapeRecognized;

    protected void OnCurrentShapeRecognized(bool val)
    {
        if (CurrentShapeRecognized != null)
        {
            CurrentShapeRecognized(val);
        }
    }

    public event System.Action<Sprite> CurrentShapePatternChanged;

    protected void OnCurrentShapePatternChanged(Sprite newSprite)
    {
        if (CurrentShapePatternChanged != null)
        {
            CurrentShapePatternChanged(newSprite);
        }
    }

    public abstract void Initialize();
}
