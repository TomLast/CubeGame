using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TomStarField : MonoBehaviour
{
    [SerializeField] private Transform _canvas;
    [SerializeField] private int _amount;
    [SerializeField] private GameObject _image;
    [SerializeField] private float _time;
    [SerializeField] private Vector2 _size;

    private void OnEnable()
    {
        SpawnToms();
    }


    private void SpawnToms()
    {
        for (int i = 0; i < _amount; i++)
        {
            StartCoroutine(PingPong(Instantiate(_image, _canvas).GetComponent<Image>()));
        }
    }

    private IEnumerator PingPong(Image i)
    {
        Vector2 target = _size;
        Vector2 start = Vector2.zero;
        float progress = Random.value;

        while(gameObject.activeSelf)
        {
            if(target == _size)
                i.rectTransform.position = new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), -1);
             
            while(progress < 1f)
            {
                progress += Time.deltaTime / _time;
                i.rectTransform.localScale = Vector2.Lerp(start, target, progress);
                yield return null;
            }
            progress = 0f;
            target = target == _size ? Vector2.zero : _size;
            start = start == Vector2.zero ? _size : Vector2.zero;
        }
    }
}
