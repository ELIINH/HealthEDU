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

    public DialogueEntry[] startDialogue; // 修改为 DialogueEntry 数组
    public DialogueEntry[] endDialogue;

    public ItemSO startReward;
    public ItemSO endReward;

    public int enemyCountNeed = 10;

    public int currentEnemyCount = 0;

    public void Start()
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
            MessageUI.Instance.Show("任务完成，请前去领赏！");
        }
    }


    public void End()
    {
        state = GameTaskState.End;
        EventCenter.OnEnemyDied -= OnEnemyDied;

        // 显示结束对话内容
        if (endDialogue != null && endDialogue.Length > 0)
        {
            DialogueUI.Instance.Show(endDialogue);
        }
    }

}
