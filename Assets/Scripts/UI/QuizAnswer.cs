using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using System;
using System.Linq;
public class QuizAnswer : MonoBehaviour
{
    string[][] ArrayX;
    private int topicMax = 0;
    private List<bool> isAnserList = new List<bool>();

    public GameObject tipsbtn;
    public TextMeshProUGUI tipsText;
    public List<Toggle> toggleList;
    public TextMeshProUGUI indexText;
    public TextMeshProUGUI TM_Text;
    public List<TextMeshProUGUI> DA_TextList;
    private int topicIndex = 0;

    public Button BtnNext;
    public Button BtnTip;
    public TextMeshProUGUI TextAccuracy;
    private int anserint = 0;
    private int isRightNum = 0;
    private int isWrongNum = 0;
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

    }

    void Start()
    {
        toggleList[0].onValueChanged.AddListener((isOn) => AnswerRightRrongJudgment(isOn, 0));
        toggleList[1].onValueChanged.AddListener((isOn) => AnswerRightRrongJudgment(isOn, 1));
        toggleList[2].onValueChanged.AddListener((isOn) => AnswerRightRrongJudgment(isOn, 2));
        toggleList[3].onValueChanged.AddListener((isOn) => AnswerRightRrongJudgment(isOn, 3));

        BtnTip.onClick.AddListener(() => Select_Answer(0));

        uiGameObject = transform.Find("UI").gameObject;

    }

    public void Show(TextAsset questions, System.Action onQuizEnd=null)
    {
        Debug.Log("Show method called");
        
        uiGameObject.SetActive(true);
        Time.timeScale = 0;
        Debug.Log("quizui show. timeScale=0");
        ResetQuizState(questions);
        this.OnQuizEnd = onQuizEnd;
    }


    void ResetQuizState(TextAsset questions)
    {

        topicIndex = 0;

        //reset state of all questions
        for (int i = 0; i < isAnserList.Count; i++)
        {
            isAnserList[i] = false;
        }

        anserint = 0;
        isRightNum = 0;
        isWrongNum = 0;
        TextAccuracy.text = "Accuracy: 0%";
        BtnNext.GetComponentInChildren<TextMeshProUGUI>().text = "NEXT";
        BtnNext.onClick.RemoveAllListeners();
        BtnNext.onClick.AddListener(() => Select_Answer(2));

        TextCsv(questions);
        LoadAnswer();
    }


    void TextCsv(TextAsset questions)
{
    if (questions == null)
    {
        Debug.LogError("Questions TextAsset is null");
        return;
    }

    string[] allLineText = questions.text.Split('\n');
    Debug.Log("Total questions: " + allLineText.Length);

    ArrayX = new string[allLineText.Length][];
    isAnserList.Clear();

    for (int i = 0; i < allLineText.Length; i++)
    {
        string line = allLineText[i].Trim();
        if (!string.IsNullOrEmpty(line))
        {
            ArrayX[i] = line.Split(':');
            isAnserList.Add(false);
        }
    }

        //remove null elements
        ArrayX = ArrayX.Where(x => x != null).ToArray();

    topicMax = ArrayX.Length;
    Debug.Log("Processed questions: " + topicMax);
}

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

        indexText.text = "quiz" + (topicIndex + 1) + ":";//index
        TM_Text.text = ArrayX[topicIndex][1];//text of question
        int idx = ArrayX[topicIndex].Length - 3;//how many options
        for (int x = 0; x < idx; x++)
        {
            DA_TextList[x].text = ArrayX[topicIndex][x + 2];//options
        }
    }

    void Select_Answer(int index)
    {
        switch (index)
        {
            case 0://tips
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
                tipsText.text = "<color=#FFAB08FF>" + "Correct answer:" + nM + "</color>";
                break;
            case 1://last question
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
            case 2://next
                if (topicIndex < topicMax - 1)
                {
                    topicIndex++;
                    LoadAnswer();
                    if (topicIndex == topicMax - 1)
                    {
                        BtnNext.GetComponentInChildren<TextMeshProUGUI>().text = "END";
                        BtnNext.onClick.RemoveAllListeners();
                        BtnNext.onClick.AddListener(CloseUI);
                        
                    }
                }

                break;

        }
    }

    void CloseUI()
    {
        Debug.Log("quiz closeui method called.time set to 1");
        Time.timeScale = 1;
        EventCenter.QuizEnd(isPassed);
        OnQuizEnd?.Invoke();
        
        uiGameObject.SetActive(false);
        
        return;
    }

    void AnswerRightRrongJudgment(bool check, int index)
    {
        if (check)
        {
            bool isRight;
            int idx = ArrayX[topicIndex].Length - 1;
            int n = int.Parse(ArrayX[topicIndex][idx]) - 1;
            if (n == index)
            {
                tipsText.text = "<color=#00AB88>" + "Right!" + "</color>";//green
                isRight = true;
                tipsbtn.SetActive(true);
            }
            else
            {
                tipsText.text = "<color=#FF0020FF>" + "Wrong!" + "</color>";
                isRight = false;
                tipsbtn.SetActive(true);
            }

  
            if (isAnserList[topicIndex])
            {
                tipsText.text = "<color=#FF0020FF>" + "This question has been answered" + "</color>";
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
                TextAccuracy.text = "Accuracy: " + ((float)isRightNum / anserint * 100).ToString("f2") + "%";
            }

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
