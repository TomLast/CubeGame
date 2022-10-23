using UnityEngine;

public class MoveAction : Action
{
    private Transform transform;
    private Vector3 start;
    private Vector3 end;
    private LerpType lerpType;
    private AnimationCurve curve;
    private AnimationCurve offset;
    private Vector3 offsetAxis;
    private float offsetStrength;
    private float progress;

    public MoveAction(Transform transform)
    {
        this.transform = transform;

        task = Task;
    }

    public void SetMovement(Vector3 start, Vector3 end, LerpType lerpType, AnimationCurve curve = null, AnimationCurve offset = null, Vector3 offsetAxis = default, float offsetStrength = 0, Del taskInit = null, Del taskCallback = null)
    {
        this.start = start;
        this.end = end;
        this.lerpType = lerpType;
        this.curve = curve ?? AnimationCurve.Linear(0, 0, 1f, 1f);
        this.offset = offset ?? AnimationCurve.Constant(0f, 0f, 0f);
        this.offsetAxis = offsetAxis;
        this.offsetStrength = offsetStrength;
        this.taskInit = taskInit;
        this.taskCallback = taskCallback;
    }

    private ActionState Task(float dt)
    {
        progress += dt / lerpType.Time;

        transform.position = Vector3.Lerp(start, end, curve.Evaluate(progress));
        transform.position += (offsetStrength * offset.Evaluate(progress)) * offsetAxis;

        return transform.position == end ? ActionState.Success : ActionState.Running;
    }

    public override void Init()
    {
        base.Init();
        progress = 0;
    }
}
