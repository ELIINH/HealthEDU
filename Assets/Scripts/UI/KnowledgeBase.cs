using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class KnowledgeBase : MonoBehaviour
{
    // 知识点数据
    string[] knowledgePoints;
    private int pageIndex = 0; // 当前页索引
    private int pointsPerPage = 3; // 每页显示的知识点数量

    // UI 元素
    public TextMeshProUGUI[] knowledgeTexts; // 用于显示知识点的 TextMeshProUGUI 数组
    public Button btnPrevious; // 上一页按钮
    public Button btnNext; // 下一页按钮
    public Button btnJump; // 跳转按钮
    public TMP_InputField jumpInput; // 跳转输入框
    public TextMeshProUGUI pageIndexText; // 当前页索引显示
    public Button btnClose; // 关闭按钮 
    //public TextMeshProUGUI totalPagesText; // 总页数显示
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

    // 从txt文档中读取知识点
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

    // 显示当前页的知识点
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
        //totalPagesText.text = "Total Pages: " + totalPages;
    }

    // 切换页面
    void ChangePage(int direction)
    {
        pageIndex += direction;
        pageIndex = Mathf.Clamp(pageIndex, 0, (knowledgePoints.Length - 1) / pointsPerPage);
        DisplayPage();
    }

    // 跳转到指定页面
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
