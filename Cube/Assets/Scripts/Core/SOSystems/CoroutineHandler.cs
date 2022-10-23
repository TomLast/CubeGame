using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CoroutineHandler", menuName = "Core/Systems/CoroutineHandler")]
public class CoroutineHandler : SOSystem
{
    public bool isRunning;

    private List<IEnumerator> coroutines = new List<IEnumerator>();

    public override void Init(SOSystemUpdater systemUpdater)
    {
        base.Init(systemUpdater);
        isRunning = true;
        coroutines.Clear();
    }

    public override void Update()
    {
        if (!isRunning)
            return;
        for (int i = coroutines.Count - 1; i >= 0; i--)
        {
            if (!coroutines[i].MoveNext())
                coroutines.Remove(coroutines[i]);
        }
    }

    public void StartCoroutine(IEnumerator coroutine)
    {
        coroutines.Add(coroutine);
    }

    public void RemoveCoroutine(IEnumerator coroutine)
    {
        if (coroutines.Contains(coroutine))
            coroutines.Remove(coroutine);
    }
}