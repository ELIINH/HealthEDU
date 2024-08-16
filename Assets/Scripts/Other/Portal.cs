using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{

    public List<GameTaskSO> gameTasks; // ����������б�
    public bool isAllTasksCompleted; // �Ƿ��������������
    public GameObject portalLight;

    [SerializeField] private string targetSceneName; // Ŀ�곡������

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
                // ������ҵ�Ŀ�곡��
                SceneManager.LoadScene(targetSceneName);
            }
            else
            {
                // ��ʾ��ʾ��Ϣ
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
