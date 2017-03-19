using UnityEngine;
using UnityEngine.UI;

public class ShapesView : AbstractShapesView
{
    [SerializeField]
    private Image _controlledUiImage;

    public override void SetNewPicture(Sprite newSprite)
    {
        _controlledUiImage.sprite = newSprite;
    }
}
