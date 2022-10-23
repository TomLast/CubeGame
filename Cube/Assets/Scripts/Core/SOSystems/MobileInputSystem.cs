using UnityEngine;
[CreateAssetMenu]
public class MobileInputSystem : SOSystem
{
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private float _swipeDistance;


    private Events.KeyDownEvent keyDownEvent = new Events.KeyDownEvent();
    private Touch _touch;
    private Vector3 _beginTouch;
    private Vector3 _endTouch;
    private Vector3 _swipeVector;

    public override void Update()
    {
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);

            switch (_touch.phase)
            {
                case TouchPhase.Began:
                    _beginTouch = _touch.position;
                    break;
                case TouchPhase.Ended:
                    _endTouch = _touch.position;
                    _swipeVector = _endTouch - _beginTouch;
                    if (_swipeVector.magnitude > _swipeDistance)
                    {
                        if (Vector3.Angle(_swipeVector, Vector3.right) < 45)
                        {
                            keyDownEvent.Value = _beginTouch.x < _endTouch.x ? KeyCode.D : KeyCode.A;
                        }
                        else
                        {
                            keyDownEvent.Value = _beginTouch.y < _endTouch.y ? KeyCode.W : KeyCode.S;
                        }
                        eventSystem?.RaiseEvent<Events.KeyDownEvent>(keyDownEvent);
                    }
                    break;
            }
        }
    }
}
