using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAction : Action
{
    private Transform transform;
    private Quaternion start;
    private Quaternion end;
    private LerpType lerpType;
    private AnimationCurve curve;
    private float progress;

    public RotationAction(Transform transform)
    {
        this.transform = transform;

        task = Task;
    }

    public void SetRotation(float degree, Vector3 axis, Space space, LerpType lerpType, AnimationCurve curve = null, Del taskInit = null, Del taskCallback = null)
    {
        start = transform.rotation;
        transform.Rotate(axis, degree, space);
        end = transform.rotation;
        transform.rotation = start;

        this.lerpType = lerpType;
        this.curve = curve ?? AnimationCurve.Linear(0, 0, 1f, 1f);
        this.taskInit = taskInit;
        this.taskCallback = taskCallback;
    }

    public void SetRotation(Quaternion targetRot, LerpType lerpType, AnimationCurve curve = null, Del taskInit = null, Del taskCallback = null)
    {
        start = transform.rotation;
        end = targetRot;

        this.lerpType = lerpType;
        this.curve = curve ?? AnimationCurve.Linear(0, 0, 1f, 1f);
        this.taskInit = taskInit;
        this.taskCallback = taskCallback;
    }



    private ActionState Task(float dt)
    {
        progress += dt / lerpType.Time;

        transform.rotation = Quaternion.Lerp(start, end, curve.Evaluate(progress));

        return progress >= 1f ? ActionState.Success : ActionState.Running;
    }

    public override void Init()
    {
        base.Init();
        progress = 0;
    }
}