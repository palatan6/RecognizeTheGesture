using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Класс отвечает за основу деятельности 
/// поля для рисования
/// </summary>

public class DrawingModel : AbstractDrawingModel
{
    private GameObject _pointerPrefab;
    private GameObject _tailPrefab;
    
    void Start ()
    {
	    InitPointerLook();
	}

    void InitPointerLook()
    {
        OnInitDone();
    }
    
    public override void GetCollectedPoints(List<Vector2> points)
    {
        OnPointsCollected(points.ToArray());
    }
}
