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
        // ��ʾ������沢��������߼�
        // ��������Ҫʵ�ֲ���������ʾ���û������߼�
        // ���������ʱ������ onQuizEnd(true) �� onQuizEnd(false) �����ݲ�����
        uiGameObject.SetActive(true);
        // ģ�������������ݽ��
        //bool passed = true; // �������ͨ��
        //onQuizEnd?.Invoke(passed);
    }

    public void Hide()
    {
        uiGameObject.SetActive(false);
    }

}