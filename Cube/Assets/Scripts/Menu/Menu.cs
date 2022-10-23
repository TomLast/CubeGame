using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] List<GameObject> _panels;

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void SetActivePanel(GameObject go)
    {
        foreach (var panel in _panels)
        {
            if (panel == go)
                panel.SetActive(true);
            else
                panel.SetActive(false);
        }
    }
}
