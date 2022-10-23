using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTest : MonoBehaviour
{
    //MoveAction a;
    //RotationAction r;
    //ActionSequence seq;
    //ActionSequence seq2;
    //ActionParallel parallel;
    //private ActionManager actionManager;
    //private AnimationCurve curve;
    //const float speed = 0.5f;


    //private void Start()
    //{
    //    actionManager = new ActionManager(this);
    //    curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
    //    r = new RotationAction(transform);
    //    a = new MoveAction(transform);

    //    parallel = new ActionParallel(new List<Action> { a, r });
    //    seq2 = new ActionSequence(new List<Action> { a });
    //    seq = new ActionSequence(new List<Action> { a, parallel, seq2 });
    //}

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.W))
    //    {
    //        if (actionManager.UseAction(parallel))
    //        {
    //            r.SetRotation(180, transform.right, new Lerp { Time = speed }, curve);
    //            a.SetMovement(transform.position, transform.position + Vector3.forward * 3, new Lerp { Time = speed }, curve);
    //        }
    //    }
    //    if (Input.GetKeyDown(KeyCode.A))
    //    {
    //        if (actionManager.UseAction(parallel))
    //        {
    //            r.SetRotation(180, transform.forward, new Lerp { Time = speed }, curve);
    //            a.SetMovement(transform.position, transform.position + Vector3.left * 3, new Lerp { Time = speed }, curve);
    //        }
    //    }
    //    if (Input.GetKeyDown(KeyCode.S))
    //    {
    //        if (actionManager.UseAction(parallel))
    //        {
    //            r.SetRotation(180, -transform.right, new Lerp { Time = speed }, curve);
    //            a.SetMovement(transform.position, transform.position + Vector3.back * 3, new Lerp { Time = speed }, curve);
    //        }
    //    }
    //    if (Input.GetKeyDown(KeyCode.D))
    //    {
    //        if (actionManager.UseAction(parallel))
    //        {
    //            r.SetRotation(180, -transform.forward, new Lerp { Time = speed }, curve);
    //            a.SetMovement(transform.position, transform.position + Vector3.right * 3, new Lerp { Time = speed }, curve);
    //        }
    //    }
    //}
}
