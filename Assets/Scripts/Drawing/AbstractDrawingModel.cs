using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDrawingModel : MonoBehaviour
{
    public event System.Action<Vector2[]> PointsCollected;

    protected void OnPointsCollected(Vector2[] points)
    {
        if (PointsCollected!=null)
        {
            PointsCollected(points);
        }
    }

    public event System.Action InitDone;

    protected void OnInitDone()
    {
        if (InitDone!=null)
        {
            InitDone();
        }
    }

    public abstract void GetCollectedPoints(List<Vector2> points);
}
