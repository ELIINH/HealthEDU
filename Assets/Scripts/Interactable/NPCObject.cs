using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameTaskSO;

public class NPCObject : InteractableObject
{
    public string npcName;
    public DialogueEntry[] contentList; //public string Name; public string Content;

    protected override void Interact()
    {
        DialogueUI.Instance.Show(contentList);
    }
}
