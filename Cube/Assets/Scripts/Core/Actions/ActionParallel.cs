using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionParallel : Action
{
    private List<Action> actions;
    private List<IEnumerator> coroutines;

    public ActionParallel(List<Action> actions)
    {
        this.actions = actions;
        coroutines = new List<IEnumerator>();
    }

    public override IEnumerator Execute()
    {
        State = ActionState.Running;
        taskInit?.Invoke();

        while (coroutines.Count > 0)
        {
            for (int i = coroutines.Count - 1; i >= 0; i--)
            { 
                if (!coroutines[i].MoveNext())
                    coroutines.Remove(coroutines[i]);
            }
            yield return null;
        }
        taskCallback?.Invoke();
        State = ActionState.Success;
    }

    public override void Init()
    {
        base.Init();
        coroutines.Clear();

        for (var index = 0; index < actions.Count; index++)
        {
            var action = actions[index];
            action.Init();
            coroutines.Add(action.Execute());
        }
    }
}
