using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using static GameQuizSO;

public class Portal : MonoBehaviour
{

    public List<GameTaskSO> senceTasks; // ����������б�
    public List<GameQuizSO> senceQuizzes;
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
        isAllTasksCompleted= AreAllTasksQuizCompleted();
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
            if (AreAllTasksQuizCompleted())
            {
                // ������ҵ�Ŀ�곡��
                //�����λ����Ϊ������λ��
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                //player.transform.position = transform.position;
                ///player.transform.position = Vector3.zero;
                if (player.transform.parent != null)
                {
                    player.transform.parent.position = Vector3.zero;
                    Debug.Log("parent position set to zero");
                    player.transform.position = Vector3.zero;
                    //Transform parentTransform = player.transform.parent;
                   /* foreach (Transform child in parentTransform)
                    {
                        child.position = Vector3.zero;
                    }*/
                    NavMeshAgent navMeshAgent = player.GetComponent<NavMeshAgent>();
                    if (navMeshAgent != null)
                    {
                        navMeshAgent.SetDestination(Vector3.zero);
                    }
                }
                SceneManager.LoadScene(targetSceneName);
            }
            else
            {
                // ��ʾ��ʾ��Ϣ
                MessageUI.Instance.Show("task and quiz not completed");
            }
        }
    }

    public bool AreAllTasksQuizCompleted()
    {
        foreach (var task in senceTasks)
        {
            if (task.state != GameTaskState.End)
            {
                return false;
            }
        }
        foreach (var quiz in senceQuizzes)
        {
            if (quiz.state != GameQuizState.End)
            {
                return false;
            }
        }
        return true;
    }
}
