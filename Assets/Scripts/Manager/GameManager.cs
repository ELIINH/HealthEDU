using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using static GameQuizSO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public PlayerProperty playerProperty;
    public InventoryManager inventoryManager;
    public List<GameTaskSO> gameTasks;
    public List<GameQuizSO> gameQuizzes; // ��Ӷ� GameQuizSO ������

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // �Զ���ȡ PlayerProperty �� InventoryManager ���
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerProperty = player.GetComponent<PlayerProperty>();
        }

        // ֱ���� Manager ���������� InventoryManager ���
        inventoryManager = GetComponent<InventoryManager>();

        if (playerProperty == null)
        {
            Debug.LogError("PlayerProperty ���δ�ҵ�����ȷ����������һ������ PlayerProperty �������Ҷ��󣬲��ұ�ǩΪ 'Player'��");
        }

        if (inventoryManager == null)
        {
            Debug.LogError("InventoryManager ���δ�ҵ�����ȷ�� Manager ��������һ������ InventoryManager ����Ķ���");
        }
    }



    private void Start()
    {
        InitializeTaskStates();
        InitializeQuizStates();
        SaveGame();
    }

    private void InitializeTaskStates()
    {
        foreach (var task in gameTasks)
        {
            task.state = GameTaskState.Waiting; // ���� TaskState ��һ��ö�����ͣ��� Waiting ������һ��ֵ
        }
    }
    private void InitializeQuizStates()
    {
        foreach (var quiz in gameQuizzes)
        {
            quiz.state = GameQuizSO.GameQuizState.Waiting; // ���� GameQuizState ��һ��ö�����ͣ��� Waiting ������һ��ֵ
        }
    }
    public void SaveGame()
    {
        SaveData saveData = new SaveData
        {
            playerData = new PlayerData
            {
                hpValue = playerProperty.hpValue,
                energyValue = playerProperty.energyValue,
                mentalValue = playerProperty.mentalValue,
                level = playerProperty.level,
                currentExp = playerProperty.currentExp,
                attackValue = playerProperty.attackValue
            },
            inventory = inventoryManager.GetItems(),
            currentScene = SceneManager.GetActiveScene().name,
            tasks = GetTaskData(),
            quizzes = GetQuizData(),
            playerPosition = new Vector3Data(playerProperty.transform.position) // �������λ��
        };

        SaveSystem.SaveGame(saveData);
    }

    public void LoadGame()
    {
        SaveData saveData = SaveSystem.LoadGame();
        if (saveData != null)
        {
            playerProperty.hpValue = saveData.playerData.hpValue;
            playerProperty.energyValue = saveData.playerData.energyValue;
            playerProperty.mentalValue = saveData.playerData.mentalValue;
            playerProperty.level = saveData.playerData.level;
            playerProperty.currentExp = saveData.playerData.currentExp;
            playerProperty.attackValue = saveData.playerData.attackValue;

            
            SceneManager.sceneLoaded += (scene, mode) => OnSceneLoaded(scene, mode, saveData.playerPosition);
            SceneManager.LoadScene(saveData.currentScene);
            inventoryManager.SetItems(saveData.inventory);
            SetTaskData(saveData.tasks);
            SetQuizData(saveData.quizzes);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode, Vector3Data playerPosition)
    {
        // �ָ����λ��
        playerProperty.transform.position = playerPosition.ToVector3();

        // ���� NavMeshAgent Ŀ��λ��
        NavMeshAgent navMeshAgent = playerProperty.GetComponent<NavMeshAgent>();
        if (navMeshAgent != null)
        {
            navMeshAgent.Warp(playerPosition.ToVector3());
        }

        // ȡ�������¼�
        SceneManager.sceneLoaded -= (s, m) => OnSceneLoaded(s, m, playerPosition);
    }

    private List<TaskData> GetTaskData()
    {
        List<TaskData> taskDataList = new List<TaskData>();
        foreach (var task in gameTasks)
        {
            taskDataList.Add(new TaskData
            {
                taskID = task.taskID,
                state = task.state,
                currentEnemyCount = task.currentEnemyCount
            });
        }
        return taskDataList;
    }

    private void SetTaskData(List<TaskData> taskDataList)
    {
        foreach (var taskData in taskDataList)
        {
            var task = gameTasks.Find(t => t.taskID == taskData.taskID);
            if (task != null)
            {
                task.state = taskData.state;
                task.currentEnemyCount = taskData.currentEnemyCount;
            }
        }
    }
    private List<QuizData> GetQuizData()
    {
        List<QuizData> quizDataList = new List<QuizData>();
        foreach (var quiz in gameQuizzes)
        {
            quizDataList.Add(new QuizData
            {
                quizID = quiz.quizID,
                state = quiz.state
            });
        }
        return quizDataList;
    }

    private void SetQuizData(List<QuizData> quizDataList)
    {
        foreach (var quizData in quizDataList)
        {
            var quiz = gameQuizzes.Find(q => q.quizID == quizData.quizID);
            if (quiz != null)
            {
                quiz.state = quizData.state;
            }
        }
    }
    public bool AreAllTasksQuizCompleted()
    {
        foreach (var task in gameTasks)
        {
            if (task.state != GameTaskState.End)
            {
                return false;
            }
        }
        foreach (var quiz in gameQuizzes)
        {
            if (quiz.state != GameQuizState.End)
            {
                return false;
            }
        }
        return true;
    }
}
