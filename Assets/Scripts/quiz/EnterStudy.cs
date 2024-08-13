using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

public class EnterStudy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnStudyButtonClick()
    {
        //SceneManager.LoadScene("ѧϰ����");

        // ����'menu' UI
        transform.gameObject.SetActive(false);

        // �����밴ť������ͬ������Ϊ "Study" �� UI
        Transform studyUI = transform.parent.Find("Study");

        if (studyUI != null)
        {
            studyUI.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("δ�ҵ���Ϊ 'Study' �� UI");           
        }
    }

    public void OnQuizButtonClick()
    {

        // ����'menu' UI
        transform.gameObject.SetActive(false);

        // �����밴ť������ͬ������Ϊ "Study" �� UI
        Transform quizUI = transform.parent.Find("Quiz");

        if (quizUI != null)
        {
            quizUI.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("δ�ҵ���Ϊ 'quiz' �� UI");
        }
    }

}
