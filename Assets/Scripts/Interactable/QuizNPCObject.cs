using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameQuizSO;

public class QuizNPCObject : InteractableObject
{
    public string npcName;
    public GameQuizSO gameQuizSO;

    private void Start()
    {
       // gameQuizSO.state = GameQuizState.Waiting;
    }

    protected override void Interact()
    {
        Debug.Log("Interact");
        switch (gameQuizSO.state)
        {
            case GameQuizState.Waiting:
                DialogueUI.Instance.Show(gameQuizSO.startDialogue, OnStartDialogueEnd);
                break;
            case GameQuizState.Executing:
                DialogueUI.Instance.Hide();
                QuizAnswer.Instance.Show(gameQuizSO.questions, OnQuizEnd);
                break;
            case GameQuizState.Passed:
                DialogueUI.Instance.Show(gameQuizSO.endPassDialogue, OnDialogueEnd);
                break;
            case GameQuizState.End:
                DialogueUI.Instance.Show(gameQuizSO.quizEndDialogue);
                break;
            default:
                break;
        }
    }

    private void OnStartDialogueEnd()
    {
        gameQuizSO.StartQuiz();
        QuizAnswer.Instance.Show(gameQuizSO.questions, OnQuizEnd);
    }

    private void OnQuizEnd()
    {
        //gameQuizSO.CompleteQuiz(passed);
        if (gameQuizSO.state== GameQuizState.Passed)
        {
            DialogueUI.Instance.Show(gameQuizSO.endPassDialogue, OnDialogueEnd);
        }
        else
        {
            DialogueUI.Instance.Show(gameQuizSO.endFailDialogue);
        }
    }

    public void OnDialogueEnd()
    {
        switch (gameQuizSO.state)
        {
            case GameQuizState.Passed:
                gameQuizSO.EndQuiz();
                InventoryManager.Instance.AddItem(gameQuizSO.endReward);
                MessageUI.Instance.Show("quiz“—pass£°");
                break;
            case GameQuizState.End:
                break;
            default:
                break;
        }
    }
}
