using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using static GameQuizSO;

public class Portal : MonoBehaviour
{

    public List<GameTaskSO> senceTasks; // List of all tasks is this scene
    public List<GameQuizSO> senceQuizzes;
    public bool isAllTasksCompleted;
    public GameObject portalLight;

    [SerializeField] private string targetSceneName;

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
                //Teleport the player to the target scene
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player.transform.parent != null)
                {
                    player.transform.parent.position = Vector3.zero;
                    player.transform.position = Vector3.zero;
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
                MessageUI.Instance.Show("Complete all tasks and questions first");
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
