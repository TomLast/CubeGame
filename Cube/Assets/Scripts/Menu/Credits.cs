using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] private Menu _menu;
    [SerializeField] private GameObject _menuGo;
    [SerializeField] private RectTransform _panel;
    [SerializeField] private float _time;

    private void OnEnable()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        Vector2 start = new Vector2(0, -_panel.rect.height);
        Vector2 end = new Vector2(0, _panel.rect.height);
        float progress = 0f;
        while(progress < 1f)
        {
            progress += Time.deltaTime / _time;

            _panel.anchoredPosition = Vector2.Lerp(start, end, progress);
            yield return null;
        }
        _menu.SetActivePanel(_menuGo);
    }
}
