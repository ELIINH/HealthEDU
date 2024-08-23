using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnowledgeUI : MonoBehaviour
{
    public GameObject uiContainer;


    public void OpenKnowledgeUI()
    {
        uiContainer.SetActive(true);
        Time.timeScale = 0;
    }
}
