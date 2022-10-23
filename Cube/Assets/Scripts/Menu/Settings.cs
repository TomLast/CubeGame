using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private CoroutineHandler _coroutineHandler;

    private bool _pause = false;
    public void TogglePause() => _coroutineHandler.isRunning = !(_pause = !_pause);
}
