using System;
using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    [SerializeField] private EventSystem eventSystem;

    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;

    //[SerializeField]
    //private bool detectSwipeOnlyAfterRelease = false;

    [SerializeField]
    private float minDistanceForSwipe = 20f;

    [SerializeField]
    private Events.KeyDownEvent keyDownEvent = new Events.KeyDownEvent();

    private void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUpPosition = touch.position;
                fingerDownPosition = touch.position;
            }

            //if (!detectSwipeOnlyAfterRelease && touch.phase == TouchPhase.Moved)
            //{
            //    fingerDownPosition = touch.position;
            //    DetectSwipe();
            //}

            if (touch.phase == TouchPhase.Ended)
            {
                fingerDownPosition = touch.position;
                DetectSwipe();
            }
        }
    }

    private void DetectSwipe()
    {
        if (SwipeDistanceCheckMet())
        {
            if (IsVerticalSwipe())
            {
                var direction = fingerDownPosition.y - fingerUpPosition.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
                SendSwipe(direction);
            }
            else
            {
                var direction = fingerDownPosition.x - fingerUpPosition.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
                SendSwipe(direction);
            }
            fingerUpPosition = fingerDownPosition;
        }
    }

    private bool IsVerticalSwipe()
    {
        return VerticalMovementDistance() > HorizontalMovementDistance();
    }

    private bool SwipeDistanceCheckMet()
    {
        return VerticalMovementDistance() > minDistanceForSwipe || HorizontalMovementDistance() > minDistanceForSwipe;
    }

    private float VerticalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.y - fingerUpPosition.y);
    }

    private float HorizontalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.x - fingerUpPosition.x);
    }

    private void SendSwipe(SwipeDirection direction)
    {
        switch(direction)
        {
            case SwipeDirection.Up:
                keyDownEvent.Value = KeyCode.W;
                break;
            case SwipeDirection.Down:
                keyDownEvent.Value = KeyCode.S;
                break;
            case SwipeDirection.Right:
                keyDownEvent.Value = KeyCode.D;
                break;
            case SwipeDirection.Left:
                keyDownEvent.Value = KeyCode.A;
                break;
        }
        eventSystem.RaiseEvent<Events.KeyDownEvent>(keyDownEvent);

    }
}

public enum SwipeDirection
{
    Up,
    Down,
    Left,
    Right
}