using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using System;
public class QuizAnswer : MonoBehaviour
{
    //读取文档
    //读取文档
    string[][] ArrayX;//题目数据
    string[] lineArray;//读取到题目数据
    private int topicMax = 0;//最大题数
    private List<bool> isAnserList = new List<bool>();//存放是否答过题的状态

    //加载题目
    public GameObject tipsbtn;//提示按钮
    public TextMeshProUGUI tipsText;//提示信息
    public List<Toggle> toggleList;//答题Toggle
    public TextMeshProUGUI indexText;//当前第几题
    public TextMeshProUGUI TM_Text;//当前题目
    public List<TextMeshProUGUI> DA_TextList;//选项
    private int topicIndex = 0;//第几题

    //按钮功能及提示信息
   // public Button BtnBack;//上一题
    public Button BtnNext;//下一题
    public Button BtnTip;//消息提醒
    //public Button BtnJump;//跳转题目
   // public TMP_InputField jumpInput;//跳转题目
    public TextMeshProUGUI TextAccuracy;//正确率
    private int anserint = 0;//已经答过几题
    private int isRightNum = 0;//正确题数
    private int isWrongNum = 0;//错误题数
    bool isPassed=false;

    public static QuizAnswer Instance;
    private GameObject uiGameObject;

    private Action OnQuizEnd;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        //TextCsv();
        //LoadAnswer();
    }

    void Start()
    {
        toggleList[0].onValueChanged.AddListener((isOn) => AnswerRightRrongJudgment(isOn, 0));
        toggleList[1].onValueChanged.AddListener((isOn) => AnswerRightRrongJudgment(isOn, 1));
        toggleList[2].onValueChanged.AddListener((isOn) => AnswerRightRrongJudgment(isOn, 2));
        toggleList[3].onValueChanged.AddListener((isOn) => AnswerRightRrongJudgment(isOn, 3));

        BtnTip.onClick.AddListener(() => Select_Answer(0));
        //BtnBack.onClick.AddListener(() => Select_Answer(1));
        //BtnNext.onClick.AddListener(() => Select_Answer(2));
        // BtnJump.onClick.AddListener(() => Select_Answer(3));
        uiGameObject = transform.Find("UI").gameObject;
        //Hide();
        //CloseUI();
    }

    public void Show(TextAsset questions, System.Action onQuizEnd=null)
    {
        // 显示测验界面并处理测验逻辑
        // 这里你需要实现测验界面的显示和用户交互逻辑
        // 当测验结束时，调用 onQuizEnd(true) 或 onQuizEnd(false) 来传递测验结果
        Debug.Log("Show method called");
        
        uiGameObject.SetActive(true);
        Time.timeScale = 0;
        Debug.Log("quizui show. timeScale=0");
        ResetQuizState();
        // 模拟测验结束并传递结果
        //bool passed = true; // 假设测验通过
        //onQuizEnd?.Invoke(passed);
        this.OnQuizEnd = onQuizEnd;
    }

    void OnEnable()
    {
        // 重置测验状态
        //ResetQuizState();
    }
    void OnDisable()
    {
        //Debug.Log("OnDisable method called");
        // 其他代码...
    }

    void ResetQuizState()
    {

        // 重置题目索引
        topicIndex = 0;

        // 重置答题状态
        for (int i = 0; i < isAnserList.Count; i++)
        {
            isAnserList[i] = false;
        }

        // 重置正确率相关变量
        anserint = 0;
        isRightNum = 0;
        isWrongNum = 0;
        TextAccuracy.text = "right rate：0%";
        BtnNext.GetComponentInChildren<TextMeshProUGUI>().text = "next";
        BtnNext.onClick.RemoveAllListeners();
        BtnNext.onClick.AddListener(() => Select_Answer(2));

        // 加载初始题目
        TextCsv();
        LoadAnswer();
    }

   

    /*****************读取txt数据******************/
    void TextCsv()
    {
        string UnityPath1 = Application.dataPath + "/StreamingAssets/5.txt";
        string[] allLineText = File.ReadAllLines(UnityPath1);
        for (int i = 0; i < allLineText.Length; i++)
        {
            Debug.Log(allLineText.Length);
        }
        ArrayX = new string[allLineText.Length][];
        //把csv中的数据储存在二维数组中  
        for (int i = 0; i < allLineText.Length; i++)
        {
            ArrayX[i] = allLineText[i].Split(':');
        }
        /*
        //查看保存的题目数据
        for (int i = 0; i < ArrayX.Length; i++)
        {
            for (int j = 0; j < ArrayX[i].Length; j++)
            {
                Debug.Log(ArrayX[i][j]);
            }
        }
        */
        //设置题目状态
        topicMax = allLineText.Length;
        for (int x = 0; x < topicMax; x++)
        {
            isAnserList.Add(false);
        }
    }

    /*****************加载题目******************/
    void LoadAnswer()
    {
        for (int i = 0; i < toggleList.Count; i++)
        {
            toggleList[i].isOn = false;
        }
        for (int i = 0; i < toggleList.Count; i++)
        {
            toggleList[i].interactable = true;
        }

        tipsbtn.SetActive(false);
        tipsText.text = "";

        indexText.text = "quiz" + (topicIndex + 1) + ":";//第几题
        TM_Text.text = ArrayX[topicIndex][1];//题目
        int idx = ArrayX[topicIndex].Length - 3;//有几个选项
        for (int x = 0; x < idx; x++)
        {
            DA_TextList[x].text = ArrayX[topicIndex][x + 2];//选项
        }
    }

    /*****************按钮功能******************/
    void Select_Answer(int index)
    {
        switch (index)
        {
            case 0://提示
                int idx = ArrayX[topicIndex].Length - 1;
                int n = int.Parse(ArrayX[topicIndex][idx]);
                string nM = "";
                switch (n)
                {
                    case 1:
                        nM = "A";
                        break;
                    case 2:
                        nM = "B";
                        break;
                    case 3:
                        nM = "C";
                        break;
                    case 4:
                        nM = "D";
                        break;
                }
                tipsText.text = "<color=#FFAB08FF>" + "correct answer：" + nM + "</color>";
                break;
            case 1://上一题
                if (topicIndex > 0)
                {
                    topicIndex--;
                    LoadAnswer();
                }
                else
                {
                    tipsText.text = "<color=#27FF02FF>" + "no before!" + "</color>";
                }
                break;
            case 2://下一题
                if (topicIndex < topicMax - 1)
                {
                    topicIndex++;
                    LoadAnswer();
                    if (topicIndex == topicMax - 1)
                    {
                        //把BtnNext的文字改为“end”
                        BtnNext.GetComponentInChildren<TextMeshProUGUI>().text = "end";
                        BtnNext.onClick.RemoveAllListeners();
                        BtnNext.onClick.AddListener(CloseUI);
                        
                    }
                }
                else
                {
                    //tipsText.text = "<color=#27FF02FF>" + "last one" + "</color>";
                }
                break;
            case 3://跳转
                /*int x = int.Parse(jumpInput.text) - 1;
                if (x >= 0 && x < topicMax)
                {
                    topicIndex = x;
                    jumpInput.text = "";
                    LoadAnswer();
                }
                else
                {
                    tipsText.text = "<color=#27FF02FF>" + "out of range" + "</color>";
                }*/
                break;
        }
    }

    void CloseUI()
    {
        Debug.Log("quiz closeui method called.time set to 1");
        Time.timeScale = 1;
        EventCenter.QuizEnd(isPassed);
        OnQuizEnd?.Invoke();

        // 关闭UI
        
        uiGameObject.SetActive(false);
        
        return;
    }

    /*****************题目对错判断******************/
    void AnswerRightRrongJudgment(bool check, int index)
    {
        if (check)
        {
            //判断题目对错
            bool isRight;
            int idx = ArrayX[topicIndex].Length - 1;
            int n = int.Parse(ArrayX[topicIndex][idx]) - 1;
            if (n == index)
            {
                tipsText.text = "<color=#27FF02FF>" + "right!" + "</color>";
                isRight = true;
                tipsbtn.SetActive(true);
            }
            else
            {
                tipsText.text = "<color=#FF0020FF>" + "sorry,wrong!" + "</color>";
                isRight = false;
                tipsbtn.SetActive(true);
            }

            //正确率计算
            if (isAnserList[topicIndex])
            {
                tipsText.text = "<color=#FF0020FF>" + "这道题已答过！" + "</color>";
            }
            else
            {
                anserint++;
                if (isRight)
                {
                    isRightNum++;
                }
                else
                {
                    isWrongNum++;
                }
                isAnserList[topicIndex] = true;
                TextAccuracy.text = "right rate：" + ((float)isRightNum / anserint * 100).ToString("f2") + "%";
            }

            //禁用掉选项
            for (int i = 0; i < toggleList.Count; i++)
            {
                toggleList[i].interactable = false;
            }
            Debug.Log("isRightNum:" + isRightNum + " ,topicMax:" + topicMax);
            if(isRightNum == topicMax)
            {
                isPassed = true;
            }
            else
            {
                isPassed = false;
            }
        }
    }

}
