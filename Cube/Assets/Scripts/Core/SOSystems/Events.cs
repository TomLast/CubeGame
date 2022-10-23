using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events
{
    public class BaseEvent { }

    public class KeyEvent : BaseEvent
    {
        public KeyCode Value { get; set; }
    }
    public class KeyDownEvent : KeyEvent
    {
    }

    public class PlayerMoveEvent : BaseEvent
    {
        public Vector3 start;
        public Vector3 target;
        public KeyCode key;
    }

    public class PlayerDeathEvent : BaseEvent { }
}
