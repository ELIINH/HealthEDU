using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class KnowledgeBase : MonoBehaviour
{
    string[] knowledgePoints;
    private int pageIndex = 0; 
    private int pointsPerPage = 3; //number of knowledge points per page

    public TextMeshProUGUI[] knowledgeTexts;
    public Button btnPrevious; 
    public Button btnNext; 
    public Button btnJump;
    public TMP_InputField jumpInput; 
    public TextMeshProUGUI pageIndexText;//page number display
    public Button btnClose; 
    public GameObject uiContainer;

    void Start()
    {
        uiContainer = transform.Find("UI").gameObject;

        btnPrevious.onClick.AddListener(() => ChangePage(-1));
        btnNext.onClick.AddListener(() => ChangePage(1));
        btnJump.onClick.AddListener(JumpToPage);
        btnClose.onClick.AddListener(CloseUI);

        LoadKnowledgePoints();
        DisplayPage();
    }

    void LoadKnowledgePoints()
    {
        string filePath = Application.dataPath + "/StreamingAssets/knowledge.txt";
        if (File.Exists(filePath))
        {
            knowledgePoints = File.ReadAllLines(filePath);
        }
        else
        {
            knowledgePoints = new string[0];
            Debug.LogError("Knowledge file not found at " + filePath);
        }
    }

    void DisplayPage()
    {
        int startIndex = pageIndex * pointsPerPage;
        for (int i = 0; i < pointsPerPage; i++)
        {
            if (startIndex + i < knowledgePoints.Length)
            {
                knowledgeTexts[i].text = knowledgePoints[startIndex + i];
            }
            else
            {
                knowledgeTexts[i].text = "";
            }
        }
        int totalPages = Mathf.CeilToInt((float)knowledgePoints.Length / pointsPerPage);
        pageIndexText.text = "Page " + (pageIndex + 1) + " of " + totalPages;
    }


    void ChangePage(int direction)
    {
        pageIndex += direction;
        pageIndex = Mathf.Clamp(pageIndex, 0, (knowledgePoints.Length - 1) / pointsPerPage);
        DisplayPage();
    }

    void JumpToPage()
    {
        int targetPage;
        if (int.TryParse(jumpInput.text, out targetPage))
        {
            Debug.Log("Jumping to page: " + targetPage);
            pageIndex = Mathf.Clamp(targetPage - 1, 0, (knowledgePoints.Length - 1) / pointsPerPage);
            DisplayPage();
        }
        else
        {
            Debug.LogError("Invalid page number: " + jumpInput.text);
        }
    }

    void CloseUI()
    {
        uiContainer.SetActive(false);
        Time.timeScale = 1;
    }
}
