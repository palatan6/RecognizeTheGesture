using UnityEngine;

public class ShapesModel : AbstractShapesModel
{
    [SerializeField]
    private ShapePattern[] _shapePatterns;

    private ShapePattern _currentSelectedPattern;

    public override void SetNewCurrentShapePattern()
    {
        ShapePattern oldPattern = _currentSelectedPattern;

        while (oldPattern == _currentSelectedPattern)
        {
            _currentSelectedPattern = _shapePatterns[Random.Range(0, _shapePatterns.Length)];
        }

        OnCurrentShapePatternChanged(_currentSelectedPattern.sprite);
    }

    public override void CheckUserEneterdPoints(Vector2[] points)
    {
        bool isRecognized = _currentSelectedPattern.CheckIfPatternMatchesGivenPoint(points);

        OnCurrentShapeRecognized(isRecognized);

        if (isRecognized)
            SetNewCurrentShapePattern();
    }

    public override void Initialize()
    {
        LoadPatterns();
        SetNewCurrentShapePattern();
    }

    void LoadPatterns()
    {
        if (_shapePatterns.Length==0)
        {
            _shapePatterns = Resources.LoadAll<ShapePattern>("Patterns");
        }
    }
}
