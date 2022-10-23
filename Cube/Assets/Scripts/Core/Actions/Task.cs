using System.Collections;
using UnityEngine;

public static class Task
{
    private delegate float ProgressEvaluate();

    public static IEnumerator Lerp(LerpType lerpType, TimeTaskDel task, AnimationCurve curve = null, Del init = null, Del callback = null)
    {
        float progress = 0f;
        ProgressEvaluate progressEvaluate;

        if (curve == null)
            progressEvaluate = () => progress;
        else
            progressEvaluate = () => curve.Evaluate(progress);

        init?.Invoke();

        if (lerpType is Lerp)
        {
            while (progress < 1f)
            {
                progress += Mathf.Clamp01(Time.deltaTime / lerpType.Time);
                task(progressEvaluate());

                yield return null;
            }
        }
        else
        {
            float tickTimer = 0f;
            float tickIntervall = ((Tick)lerpType).TickIntervall;
            
            while (progress <= 1f)
            {
                tickTimer = (tickTimer + Time.deltaTime) >= tickIntervall && ExecuteHelper(task, progressEvaluate()) ? tickIntervall - (tickTimer + Time.deltaTime) : tickTimer + Time.deltaTime;
                progress += Time.deltaTime / lerpType.Time;
                yield return null;
            }
            task(1f);
        }
        callback?.Invoke();
    }

    private static bool ExecuteHelper(TimeTaskDel task, float t)
    {
        task(t);
        return true;
    }
}
public delegate void TimeTaskDel(float dt);
public delegate void Del();