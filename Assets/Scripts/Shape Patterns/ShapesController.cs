using UnityEngine;

/// <summary>
/// Класс связывает работу модели шаблонов фигур
/// и его панель для отрисовки.
/// 
/// Так же здесь происходит обмен информацией с другими блоками
/// игры.
/// 
/// </summary>

public class ShapesController : MonoBehaviour
{
    public AbstractShapesModel shapesModel;
    public AbstractShapesView shapesView;

    public AbstractDrawingModel drawingModel;

    public AbstarctGamePlayModel gamePlayModel;

    void OnEnable()
    {
        drawingModel.PointsCollected += CheckEnteredPoints;

        shapesModel.CurrentShapePatternChanged += ShapesModel_CurrentShapePatternChanged;

        shapesModel.CurrentShapePatternChanged += OnStartSpriteInstallation;

        shapesModel.CurrentShapeRecognized += ShapesModel_CurrentShapeRecognized;

        gamePlayModel.GameStart += GamePlayModel_GameStart;

        gamePlayModel.GameOver += GamePlayModel_GameOver;
    }

    void OnDisable()
    {
        drawingModel.PointsCollected -= CheckEnteredPoints;

        shapesModel.CurrentShapePatternChanged -= ShapesModel_CurrentShapePatternChanged;

        shapesModel.CurrentShapePatternChanged -= OnStartSpriteInstallation;

        shapesModel.CurrentShapeRecognized -= ShapesModel_CurrentShapeRecognized;

        gamePlayModel.GameStart -= GamePlayModel_GameStart;

        gamePlayModel.GameOver -= GamePlayModel_GameOver;
    }

    private void GamePlayModel_GameStart()
    {
        shapesModel.Initialize();
    }

    void CheckEnteredPoints(Vector2[] points)
    {
        shapesModel.CheckUserEneterdPoints(points);
    }

    private void ShapesModel_CurrentShapePatternChanged(Sprite newSprite)
    {
        shapesView.SetNewPicture(newSprite);
    }

    private void ShapesModel_CurrentShapeRecognized(bool isRecognized)
    {
        gamePlayModel.OnRoundWon(isRecognized);
    }

    void OnStartSpriteInstallation(Sprite newSprite)
    {
        shapesModel.CurrentShapePatternChanged -= OnStartSpriteInstallation;

        gamePlayModel.StartFirstRound();
    }

    private void GamePlayModel_GameOver()
    {
        shapesModel.CurrentShapePatternChanged += OnStartSpriteInstallation;
    }
}
