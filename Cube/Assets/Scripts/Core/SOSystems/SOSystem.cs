using UnityEngine;

public abstract class SOSystem : ScriptableObject
{
    private SOSystemUpdater systemUpdater;
    public virtual void Init(SOSystemUpdater systemUpdater)
    {
        this.systemUpdater = systemUpdater;
    }

    public virtual void Update()
    {
    }
}