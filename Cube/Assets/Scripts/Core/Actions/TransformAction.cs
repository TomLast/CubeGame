using UnityEngine;

//public class TransformAction : Action 
//{
//    private Transform transform;
//    private TransformActionType type;
//    private Vector3 start;
//    private Vector3 target;
//    private LerpType lerpType;
//    private AnimationCurve curve;

//    public TransformAction(Transform transform, Del actionInit = null, Del actionCallback = null) : base(actionInit,
//        actionCallback)
//    {
//        this.transform = transform;
//    }

//    public override void Init()
//    {
//        base.Init();

//        switch(type)
//        {
//            case TransformActionType.Position:
//                task = Task.Lerp(lerpType, dt => transform.position = Vector3.Lerp(start, target, dt), curve);
//                taskCondition = () => transform.position == target ? ActionState.Success : ActionState.Running;
//                break;
//            case TransformActionType.Rotation:
//                task = Task.Lerp(lerpType, dt => transform.rotation = Quaternion.Euler(Vector3.Lerp(start, target, dt)),curve);
//                taskCondition = () => transform.rotation.eulerAngles == target ? ActionState.Success : ActionState.Running;
//                break;
//            case TransformActionType.Scale:
//                task = Task.Lerp(lerpType, dt => transform.localScale = Vector3.Lerp(start, target, dt),curve);
//                taskCondition = () => transform.localScale == target ? ActionState.Success : ActionState.Running;
//                break;
//        }
//    }

//    public void SetTransformAction(TransformActionType type, Vector3 start, Vector3 target, LerpType lerpType, AnimationCurve curve = null)
//    {
//        this.type = type;
//        this.start = start;
//        this.target = target;
//        this.lerpType = lerpType;
//        this.curve = curve;
//        Init();
//    }
//}

public enum TransformActionType
{
    Position,
    Rotation,
    Scale
}