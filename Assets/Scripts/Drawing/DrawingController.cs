using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс отвечает за связь поля для рисования с его моделью.
/// Так же сохраняет точки от ввода пользователя.
/// </summary>

public class DrawingController : MonoBehaviour
{
    public AbstractDrawingModel drawingModel;

    public AbstractDrawingView drawingView;

    private Coroutine _pointerMoving;

    void OnEnable()
    {
        drawingModel.InitDone += OnInitDone;
        drawingView.PointerDown += MovingPointer;
    }

    void OnDisable()
    {
        drawingModel.InitDone -= OnInitDone;
        drawingView.PointerDown -= MovingPointer;
    }

    void OnInitDone()
    {
        drawingView.InitView();
    }

    void MovingPointer(bool val)
    {
        if (val)
        {
            if (_pointerMoving == null)
            {
                _pointerMoving = StartCoroutine(PointerMoving());
            }
        }
        else
        {
            if (_pointerMoving != null)
            {
                StopCoroutine(_pointerMoving);
                _pointerMoving = null;

                drawingModel.GetCollectedPoints(_drawnPoints);
            }
        }

    }

    List<Vector2> _drawnPoints = new List<Vector2>();

    IEnumerator PointerMoving()
    {
        _drawnPoints = new List<Vector2>();

        while (true)
        {
            _drawnPoints.Add(Input.mousePosition);

            drawingView.MoveGraphiicObjects();

            yield return null;
        }
    }
}
