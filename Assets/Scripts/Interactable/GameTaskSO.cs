using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameTaskState
{
    Waiting,
    Executing,
    Completed,
    End
}
[CreateAssetMenu()]
public class GameTaskSO:ScriptableObject
{
    public GameTaskState state;
    public string taskName;
    public string taskDescription;
    public int taskID;

    //public string[] diague;
    [Serializable]
    public struct DialogueEntry
    {
        public string Name;
        public string Content;
    }

    public DialogueEntry[] startDialogue;
    public DialogueEntry[] endDialogue;

    public ItemSO startReward;
    public ItemSO endReward;
    

    public int enemyCountNeed = 10;

    public int currentEnemyCount = 0;

    public void TaskStart()
    {
        currentEnemyCount = 0;
        state = GameTaskState.Executing;
        EventCenter.OnEnemyDied += OnEnemyDied;
    }

    private void OnEnemyDied(Enemy enemy)
    {
        if (state == GameTaskState.Completed) return;
        currentEnemyCount++;
        if(currentEnemyCount>= enemyCountNeed)
        {
            state = GameTaskState.Completed;
            MessageUI.Instance.Show("Task completed!");
        }
    }


    public void End()
    {
        state = GameTaskState.End;
        EventCenter.OnEnemyDied -= OnEnemyDied;

        // show end dialogue
        if (endDialogue != null && endDialogue.Length > 0)
        {
            DialogueUI.Instance.Show(endDialogue);
        }
    }

}
