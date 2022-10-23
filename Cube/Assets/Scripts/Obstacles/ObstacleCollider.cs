using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollider : MonoBehaviour
{
    public Action<Collider> OnCollided;

    private void OnTriggerEnter(Collider other)
    {
        OnCollided(other);
    }
}
