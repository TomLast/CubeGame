using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Map map;
    [SerializeField] private CoroutineHandler coroutineHandler;
    [SerializeField] private VectorVariable localX;
    [SerializeField] private VectorVariable localZ;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private FloatVariable playerSpeed;

    private Events.PlayerMoveEvent moveEvent;

    private float stepLength;
    private AnimationCurve curve;
    private ActionManager actionManager;

    void Awake()
    {
        SetStartPosition();
        stepLength = map.CellDistance;

        curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
        actionManager = new ActionManager(coroutineHandler);

        eventSystem.AddListener<Events.KeyDownEvent>(Move);
        moveEvent = new Events.PlayerMoveEvent();
    }

    private void SetStartPosition()
    {
        Cell c = map.CUBESIDES[Vector3.up].Cells[12];
        transform.position = c.Pos + Vector3.up * 0.5f;
        transform.up = c.Normal;

        localX.Value = Vector3.right;
        localZ.Value = Vector3.Cross(localX, transform.up);
    }

    private void Move(Events.BaseEvent e)
    {
        if (actionManager.UsesAction())
            return;

        Events.KeyDownEvent keyEvent = (Events.KeyDownEvent)e;

        Vector3 dir = Vector3.zero;
        Vector3 rotDir = Vector3.zero;

        switch (keyEvent.Value)
        {
            case KeyCode.W:
                dir = localZ.Value;
                rotDir = localX;
                break;
            case KeyCode.A:
                dir = -localX.Value;
                rotDir = localZ.Value;
                break;
            case KeyCode.S:
                dir = -localZ.Value;
                rotDir = -localX.Value;
                break;
            case KeyCode.D:
                dir = localX.Value;
                rotDir = -localZ.Value;
                break;
        }
        Vector3 forward = Vector3.Cross(localX, localZ);
        Action move = map.CheckBoundary((transform.position + forward * 0.5f) + dir * stepLength, -forward) ? NormalMove(dir, rotDir, keyEvent.Value) : CornerMove(dir, rotDir, keyEvent.Value);

        actionManager.UseAction(move);
    }

    private Action NormalMove(Vector3 moveDir, Vector3 rotDir, KeyCode key)
    {
        MoveAction moveAction = new MoveAction(transform);
        RotationAction rotationAction = new RotationAction(transform);
        ActionParallel actionParallel = new ActionParallel(new List<Action> { moveAction, rotationAction });

        Vector3 start = transform.position;
        Vector3 target = transform.position + moveDir * stepLength;

        moveAction.SetMovement(start, target, new Lerp { Time = playerSpeed.Value }, curve);
        rotationAction.SetRotation(90, rotDir, Space.World, new Lerp { Time = playerSpeed.Value }, curve);

        RaiseMoveEvent(start, target, key);

        return actionParallel;
    }

    private Action CornerMove(Vector3 moveDir, Vector3 rotDir, KeyCode key)
    {
        MoveAction moveAction = new MoveAction(transform);
        RotationAction rotationAction = new RotationAction(transform);
        ActionParallel actionParallel = new ActionParallel(new List<Action> { moveAction, rotationAction });
        ActionSequence actionSequence = new ActionSequence(new List<Action> { actionParallel, actionParallel });

        Vector3 start = transform.position;
        Vector3 half = transform.position + moveDir * stepLength;
        Vector3 forward = Vector3.Cross(localX, localZ);

        if (moveDir == localX || moveDir == -localX.Value)
            localX.Value = moveDir == localX ? Vector3.Cross(localX, localZ) : -Vector3.Cross(localX, localZ);
        else
            localZ.Value = moveDir == localZ ? Vector3.Cross(localX, localZ) : -Vector3.Cross(localX, localZ);

        Vector3 target = (transform.position + forward * 0.5f) + moveDir * stepLength / 2f + forward * stepLength / 2f - Vector3.Cross(localX, localZ) * 0.5f;

        moveAction.SetMovement(start, half, new Lerp { Time = playerSpeed.Value / 2f }, null, null, default, 0, null, () => moveAction.SetMovement(half, target, new Lerp { Time = playerSpeed.Value / 2 }));
        rotationAction.SetRotation(90, rotDir, Space.World, new Lerp { Time = playerSpeed.Value });

        RaiseMoveEvent(start, target, key);

        return actionSequence;
    }

    private void RaiseMoveEvent(Vector3 start, Vector3 target, KeyCode key)
    {
        moveEvent.start = start;
        moveEvent.target = target;
        moveEvent.key = key;
        eventSystem.RaiseEvent<Events.PlayerMoveEvent>(moveEvent);
    }

    private void OnDisable()
    {
        eventSystem.RemoveListener<KeyDownEvent>(Move);
    }
}