using System.Collections;
using UnityEngine;

public class Action
{
    public ActionState State { get; protected set; } = ActionState.Running;

    protected ActionStateDel task;
    protected Del taskInit;
    protected Del taskCallback;

    public Action(ActionStateDel task, Del taskInit = null, Del taskCallback = null)
    {
        this.task = task;
        this.taskInit = taskInit;
        this.taskCallback = taskCallback;
    }

    protected Action()
    {
    }

    public virtual IEnumerator Execute()
    {
        State = ActionState.Running;
        taskInit?.Invoke();

        while (State == ActionState.Running)
        {
            State = task(Time.deltaTime);

            if(State != ActionState.Running)
                taskCallback?.Invoke();

            yield return null;
        }
    }

    public virtual void Init()
    {
        State = ActionState.Running;
    }
}

public enum ActionState
{
    Running,
    Failed,
    Success
}

public delegate ActionState ActionStateDel(float dt);
