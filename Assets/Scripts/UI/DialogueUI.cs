using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameTaskSO;

public class DialogueUI : MonoBehaviour
{

    public static DialogueUI Instance { get; private set; }

    private TextMeshProUGUI nameText;
    private TextMeshProUGUI contentText;
    private Button continueButton;

    //private List<string> contentList;
    private List<DialogueEntry> contentList;
    private int contentIndex = 0;

    private GameObject uiGameObject;

    private Action OnDialogueEnd;

    private void Awake()
    {
        if(Instance!=null && Instance!=this)
        {
            Destroy(this.gameObject);return;
        }

        Instance = this;
    }

    private void Start()
    {
        nameText = transform.Find("UI/NameTextBg/NameText").GetComponent<TextMeshProUGUI>();
        contentText = transform.Find("UI/ContentText").GetComponent<TextMeshProUGUI>();
        continueButton = transform.Find("UI/ContinueButton").GetComponent<Button>();
        continueButton.onClick.AddListener(this.OnContinueButtonClick);
        uiGameObject = transform.Find("UI").gameObject;
        Hide();
    }

    public void Show()
    {
        uiGameObject.SetActive(true);
        Time.timeScale = 0;
        Debug.Log("Dialogue show method called.Time.timeScale = 0;");
    }

    /*public void Show(string name,string[] content,Action OnDiagoueEnd=null)
    {
        nameText.text = name;
        contentList = new List<string>();
        contentList.AddRange(content);
        contentIndex = 0;
        contentText.text = contentList[0];
        uiGameObject.SetActive(true);
        this.OnDialogueEnd = OnDiagoueEnd;
    }*/
    public void Show(DialogueEntry[] entries, Action OnDialogueEnd = null)
    {
        contentList = new List<DialogueEntry>(entries);
        contentIndex = 0;
        UpdateDialogue();
        uiGameObject.SetActive(true);
        Time.timeScale = 0;
        Debug.Log("Dialogue show[] method called.Time.timeScale = 0;");
        this.OnDialogueEnd = OnDialogueEnd;
    }

    public void Hide()
    {
        Time.timeScale = 1;
        Debug.Log("Dialogue Hide method called.Time.timeScale = 1;");
        uiGameObject.SetActive(false);
        
    }


    private void OnContinueButtonClick()
    {
        contentIndex++;
        if (contentIndex >= contentList.Count)
        {
            Hide();
            OnDialogueEnd?.Invoke();
            return;
        }
        UpdateDialogue();
        //contentText.text = contentList[contentIndex];
        
    }

    private void UpdateDialogue()
    {
        nameText.text = contentList[contentIndex].Name;
        contentText.text = contentList[contentIndex].Content;
    }

}
