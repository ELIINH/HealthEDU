using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuizUI : MonoBehaviour
{
    public static QuizUI Instance;

    public GameObject uiGameObject;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

        uiGameObject = transform.Find("UI").gameObject;
        //Hide();
    }


    public void Show(TextAsset questions, System.Action onQuizEnd)
    {
        uiGameObject.SetActive(true);
    }

    public void Hide()
    {
        uiGameObject.SetActive(false);
    }

}