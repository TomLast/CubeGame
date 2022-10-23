using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private FloatVariable _time;

    private Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = $"Score: {(int)_time.Value}";
    }
}
