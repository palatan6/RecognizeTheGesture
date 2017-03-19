using UnityEngine;

[CreateAssetMenu(menuName = "ShapePattern")]
public class ShapePattern : ScriptableObject
{
    public float eps = 0.02f;
    public float verticesAccuracy = 0.2f;
    public float successPercent = 30;

    public Vector2[] vertices;

    public Sprite sprite;

    private CalculateShapesMatch _calculator;

    private CalculateShapesMatch Calculator
    {
        get { return _calculator ?? (_calculator = new CalculateShapesMatch()); }
        set { _calculator = value; }
    }

    public bool CheckIfPatternMatchesGivenPoint(Vector2[] points)
    {
        return Calculator.CheckPattern(vertices, points, verticesAccuracy, eps)>=successPercent;
    }
}
