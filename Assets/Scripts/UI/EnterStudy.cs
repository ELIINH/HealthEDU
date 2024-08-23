using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using static UnityEditor.Progress;

public class EnterStudy : MonoBehaviour
{

    public void OnStudyButtonClick()
    {
        transform.gameObject.SetActive(false);

        Transform studyUI = transform.parent.Find("Study");

        if (studyUI != null)
        {
            studyUI.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Study UI null");           
        }
    }

    public void OnQuizButtonClick()
    {
        transform.gameObject.SetActive(false);
        Transform quizUI = transform.parent.Find("Quiz");

        if (quizUI != null)
        {
            quizUI.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("QuizUI is null");
        }
    }

}
