using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class mbCell : MonoBehaviour
{
    public Cell Cell { get; set; }

    [SerializeField] private Map _map;
    [SerializeField] private CoroutineHandler _coroutineHandler;
    [SerializeField] private float _pingPongTime;
    [SerializeField] private AnimationCurve _blinkCurve;

    private Material _mat;
    private Color _start;

    private void Start()
    {
        _mat = GetComponent<MeshRenderer>().material;
        transform.localScale = new Vector3(_map.CellDistance / 10, 1, _map.CellDistance / 10);
        _start = _mat.GetColor("_CellColour");
    }

    public void Target()
    {
        _coroutineHandler.StartCoroutine(PingPong());
    }

    private IEnumerator PingPong()
    {
        float t = 0;

        while((t += Time.deltaTime) < _pingPongTime)
        {
            _mat.SetColor("_CellColour", Color.Lerp(_start, Color.red, _blinkCurve.Evaluate(t/_pingPongTime)));
            yield return null;
        }
        _mat.SetColor("_CellColour", _start);

    }
}
