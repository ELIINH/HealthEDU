using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[Serializable]
public class SaveData
{
    public PlayerData playerData;
    public List<ItemData> inventory;
    public string currentScene;
    public List<TaskData> tasks;
    public List<QuizData> quizzes;
    public Vector3Data playerPosition;
}


[Serializable]
public class PlayerData
{
    public int hpValue;
    public float energyValue;
    public int mentalValue;
    public int level;
    public int currentExp;
    public int attackValue;
}

[Serializable]
public class ItemData
{
    public string itemName;
    public int quantity;
}

[Serializable]
public class TaskData
{
    public int taskID;
    public GameTaskState state;
    public int currentEnemyCount;
}

[Serializable]
public class QuizData
{
    public int quizID;
    public GameQuizSO.GameQuizState state;
}

[Serializable]
public class Vector3Data
{
    public float x;
    public float y;
    public float z;

    public Vector3Data(Vector3 vector)
    {
        x = vector.x;
        y = vector.y;
        z = vector.z;
    }

    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }
}
