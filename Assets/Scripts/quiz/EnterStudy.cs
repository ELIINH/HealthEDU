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
        //SceneManager.LoadScene("学习界面");

        // 隐藏'menu' UI
        transform.gameObject.SetActive(false);

        // 查找与按钮父物体同级的名为 "Study" 的 UI
        Transform studyUI = transform.parent.Find("Study");

        if (studyUI != null)
        {
            studyUI.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("未找到名为 'Study' 的 UI");           
        }
    }

    public void OnQuizButtonClick()
    {

        // 隐藏'menu' UI
        transform.gameObject.SetActive(false);

        // 查找与按钮父物体同级的名为 "Study" 的 UI
        Transform quizUI = transform.parent.Find("Quiz");

        if (quizUI != null)
        {
            quizUI.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("未找到名为 'quiz' 的 UI");
        }
    }

}
