using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSequence : Action
{
    private List<Action> actions;
    private Action currentAction;

    public ActionSequence(List<Action> actions) : base()
    {
        this.actions = actions;
    }

    public override IEnumerator Execute()
    {
        State = ActionState.Running;
        for (int i = 0; i < actions.Count; i++)
        {
            currentAction = actions[i];
            currentAction.Init();
            IEnumerator coroutine = currentAction.Execute();

            while (coroutine.MoveNext())
            {
                if (currentAction.State == ActionState.Failed)
                {
                    State = ActionState.Failed;
                    yield break;
                }
                yield return null;
            }
        }
        State = ActionState.Success;
        yield return null;
    }
}