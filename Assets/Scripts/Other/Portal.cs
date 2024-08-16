using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{

    public List<GameTaskSO> gameTasks; // 所有任务的列表
    public bool isAllTasksCompleted; // 是否所有任务都已完成
    public GameObject portalLight;

    [SerializeField] private string targetSceneName; // 目标场景名称

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isAllTasksCompleted= GameManager.Instance.AreAllTasksQuizCompleted();
        if (isAllTasksCompleted)
        {
            portalLight.SetActive(true);
        }
        else
        {
            portalLight.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnPortalTriggerEnter");
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.AreAllTasksQuizCompleted())
            {
                // 传送玩家到目标场景
                SceneManager.LoadScene(targetSceneName);
            }
            else
            {
                // 显示提示信息
                MessageUI.Instance.Show("task and quiz not completed");
            }
        }
    }

    /*public bool AreAllTasksCompleted()
    {
        foreach (var task in gameTasks)
        {
            if (task.state != GameTaskState.End)
            {
                return false;
            }
        }
        return true;
    }*/
}
