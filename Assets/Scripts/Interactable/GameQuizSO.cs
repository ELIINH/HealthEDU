using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static GameTaskSO;

[CreateAssetMenu(fileName = "NewGameQuizSO", menuName = "ScriptableObjects/GameQuizSO", order = 1)]
public class GameQuizSO : ScriptableObject
{
    public enum GameQuizState
    {
        Waiting,
        Executing,
        Passed,
        End
    }
    public int quizID;
    public GameQuizState state = GameQuizState.Waiting;
    public DialogueEntry[] startDialogue;
    public DialogueEntry[] endPassDialogue;
    public DialogueEntry[] endFailDialogue;
    public DialogueEntry[] quizEndDialogue;
    public List<QuizQuestion> quizQuestions;
    public ItemSO startReward;
    public ItemSO endReward;
    public TextAsset questions;

    public void StartQuiz()
    {
        state = GameQuizState.Executing;
        EventCenter.OnQuizEnd += OnQuizEnd;
    }

    public void OnQuizEnd(bool passed)
    {
        Debug.Log("OnQuizEnd");
        if (passed)
        {
            state = GameQuizState.Passed;
        }
        else
        {
            state = GameQuizState.Waiting;
        }
    }

    public void EndQuiz()
    {
        state = GameQuizState.End;
        EventCenter.OnQuizEnd += OnQuizEnd;
    }
}

[System.Serializable]
public class QuizQuestion
{
    public string question;
    public string[] options;
    public int correctAnswerIndex;
}

[System.Serializable]
public class Reward
{
    public string itemName;
    public int quantity;
}
