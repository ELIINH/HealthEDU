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
        // 显示测验界面并处理测验逻辑
        // 这里你需要实现测验界面的显示和用户交互逻辑
        // 当测验结束时，调用 onQuizEnd(true) 或 onQuizEnd(false) 来传递测验结果
        uiGameObject.SetActive(true);
        // 模拟测验结束并传递结果
        //bool passed = true; // 假设测验通过
        //onQuizEnd?.Invoke(passed);
    }

    public void Hide()
    {
        uiGameObject.SetActive(false);
    }

}