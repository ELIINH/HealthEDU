using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameTaskSO;

public class NPCObject : InteractableObject
{
    public string npcName;
    //public string[] contentList;
    public DialogueEntry[] contentList;

    protected override void Interact()
    {
        //DialogueUI.Instance.Show(npcName, contentList);
        DialogueUI.Instance.Show(contentList);
    }
}
