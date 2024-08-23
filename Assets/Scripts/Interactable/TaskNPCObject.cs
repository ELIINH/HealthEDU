using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameTaskSO;

public class TaskNPCObject : InteractableObject
{
    public string npcName;
    public GameTaskSO gameTaskSO;

    public DialogueEntry[] contentInTaskExecuting;
    public DialogueEntry[] contentInTaskCompleted;
    public DialogueEntry[] contentInTaskEnd; 


    protected override void Interact()
    {
        Debug.Log("Interact");
        switch (gameTaskSO.state)
        {
            case GameTaskState.Waiting:
                DialogueUI.Instance.Show(gameTaskSO.startDialogue, OnDialogueEnd);
                break;
            case GameTaskState.Executing:
                DialogueUI.Instance.Show(contentInTaskExecuting);
                break;
            case GameTaskState.Completed:
                DialogueUI.Instance.Show(contentInTaskCompleted, OnDialogueEnd);
                break;
            case GameTaskState.End:
                DialogueUI.Instance.Show(contentInTaskEnd);
                break;
            default:
                break;
        }
    }

    public void OnDialogueEnd()
    {
        switch (gameTaskSO.state)
        {
            case GameTaskState.Waiting:
                gameTaskSO.TaskStart();
                InventoryManager.Instance.AddItem(gameTaskSO.startReward);
                MessageUI.Instance.Show("Task accepted!");
                break;
            case GameTaskState.Executing:
                break;
            case GameTaskState.Completed:
                gameTaskSO.End();
                InventoryManager.Instance.AddItem(gameTaskSO.endReward);
                MessageUI.Instance.Show("Task submitted!");
                //SaveGame
                GameManager.Instance.SaveGame();
                break;
            case GameTaskState.End:
                break;
            default:
                break;
        }


    }
}
