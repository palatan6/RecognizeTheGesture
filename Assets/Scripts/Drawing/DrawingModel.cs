using UnityEngine;
using System.Collections.Generic;

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
