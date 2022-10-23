using System.Collections.Generic;
using UnityEngine;

public class SOSystemUpdater : MonoBehaviour
{
    [SerializeField] private List<SOSystem> initSystems = new List<SOSystem>();
    [SerializeField] private List<SOSystem> updateSystems = new List<SOSystem>();

    public void Awake()
    {
        foreach (SOSystem system in initSystems)
        {
            system?.Init(this);
        }
        foreach (SOSystem system in updateSystems)
        {
            system?.Init(this);
        }
    }

    private void Update()
    {
        for (int i = updateSystems.Count - 1; i >= 0; i--)
        {
            updateSystems[i]?.Update();
        }
    }

    public void AddToUpdate(SOSystem system)
    {
        if (!updateSystems.Contains(system)) updateSystems.Add(system);
    }

    public void RemoveFromUpdate(SOSystem system)
    {
        if (updateSystems.Contains(system)) updateSystems.Remove(system);
    }
}