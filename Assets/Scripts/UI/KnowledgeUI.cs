using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnowledgeUI : MonoBehaviour
{
    public GameObject uiContainer;

    // Start is called before the first frame update
    void Start()
    {
        uiContainer = transform.Find("UI").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenKnowledgeUI()
    {
        uiContainer.SetActive(true);
        Time.timeScale = 0;
    }
}
