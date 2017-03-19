using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrawingView : AbstractDrawingView, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private Transform _parentForCustomPointer;
    [SerializeField]
    private GameObject _pointer;
    [SerializeField]
    private GameObject _tail;

    Transform _pointerTransform;
    Transform _tailTransform;

    public void OnPointerDown(PointerEventData eventData)
    {
        SwitchCustomPointers(true);

        OnPointerDown(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SwitchCustomPointers(false);

        OnPointerDown(false);
    }

    void SwitchCustomPointers(bool isActive)
    {
        _pointer.SetActive(isActive);
        _tail.SetActive(isActive);
    }

    public override void MoveGraphiicObjects()
    {
        _pointerTransform.position = Input.mousePosition;

        float z = _tailTransform.position.z;

        _tailTransform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _tailTransform.position = new Vector3(_tailTransform.position.x, _tailTransform.position.y, z);
    }

    public override void InitView()
    {
        _pointer = Instantiate(_pointer);

        _tail = Instantiate(_tail);

        _pointerTransform = _pointer.transform;
        _tailTransform = _tail.transform;

        _pointerTransform.SetParent(_parentForCustomPointer);
        _tailTransform.SetParent(_parentForCustomPointer);

        SwitchCustomPointers(false);
    }
}
