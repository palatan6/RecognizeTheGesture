using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс отвечает за отрисовку поступающих
/// шаблонов фигур
/// </summary>

public class ShapesView : AbstractShapesView
{
    [SerializeField]
    private Image _controlledUiImage;

    public override void SetNewPicture(Sprite newSprite)
    {
        _controlledUiImage.sprite = newSprite;
    }
}
