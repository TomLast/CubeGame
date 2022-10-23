using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GraphicSettings : MonoBehaviour
{
    [SerializeField] private BoolVariable _bloomOn;
    [SerializeField] private Toggle _toggle;

    private void Start()
    {
        _toggle.isOn = _bloomOn.Value;
    }

    public void SetBloomOn(bool on)
    {
        _bloomOn.Value = on;
    }
}
