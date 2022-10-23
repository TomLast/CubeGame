using System.Collections;

public class ActionManager
{
    private bool updateActions;
    private Action currentAction;
    private IEnumerator currentCoroutine;

    public ActionManager(CoroutineHandler coroutineHandler)
    {
        updateActions = true;
        coroutineHandler.StartCoroutine(ActionLoop());
    }

    public void UseAction(Action action)
    {
        if(currentAction == null || currentAction.State != ActionState.Running)
        {
            currentAction = action;
            currentAction.Init();
            currentCoroutine = currentAction.Execute();
        }
    }

    public bool UsesAction() => currentAction != null && currentAction.State == ActionState.Running;

    private IEnumerator ActionLoop()
    {
        while(true)
        {
            while(updateActions)
            {
                if (currentAction != null && currentAction.State == ActionState.Running)
                    currentCoroutine.MoveNext();
                yield return null;
            }

            yield return null;
        }
    }
}
