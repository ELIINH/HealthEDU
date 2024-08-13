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
    //��ȡ�ĵ�
    //��ȡ�ĵ�
    string[][] ArrayX;//��Ŀ����
    string[] lineArray;//��ȡ����Ŀ����
    private int topicMax = 0;//�������
    private List<bool> isAnserList = new List<bool>();//����Ƿ������״̬

    //������Ŀ
    public GameObject tipsbtn;//��ʾ��ť
    public TextMeshProUGUI tipsText;//��ʾ��Ϣ
    public List<Toggle> toggleList;//����Toggle
    public TextMeshProUGUI indexText;//��ǰ�ڼ���
    public TextMeshProUGUI TM_Text;//��ǰ��Ŀ
    public List<TextMeshProUGUI> DA_TextList;//ѡ��
    private int topicIndex = 0;//�ڼ���

    //��ť���ܼ���ʾ��Ϣ
   // public Button BtnBack;//��һ��
    public Button BtnNext;//��һ��
    public Button BtnTip;//��Ϣ����
    //public Button BtnJump;//��ת��Ŀ
   // public TMP_InputField jumpInput;//��ת��Ŀ
    public TextMeshProUGUI TextAccuracy;//��ȷ��
    private int anserint = 0;//�Ѿ��������
    private int isRightNum = 0;//��ȷ����
    private int isWrongNum = 0;//��������
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
        // ��ʾ������沢��������߼�
        // ��������Ҫʵ�ֲ���������ʾ���û������߼�
        // ���������ʱ������ onQuizEnd(true) �� onQuizEnd(false) �����ݲ�����
        Debug.Log("Show method called");
        
        uiGameObject.SetActive(true);
        Time.timeScale = 0;
        Debug.Log("quizui show. timeScale=0");
        ResetQuizState();
        // ģ�������������ݽ��
        //bool passed = true; // �������ͨ��
        //onQuizEnd?.Invoke(passed);
        this.OnQuizEnd = onQuizEnd;
    }

    void OnEnable()
    {
        // ���ò���״̬
        //ResetQuizState();
    }
    void OnDisable()
    {
        //Debug.Log("OnDisable method called");
        // ��������...
    }

    void ResetQuizState()
    {

        // ������Ŀ����
        topicIndex = 0;

        // ���ô���״̬
        for (int i = 0; i < isAnserList.Count; i++)
        {
            isAnserList[i] = false;
        }

        // ������ȷ����ر���
        anserint = 0;
        isRightNum = 0;
        isWrongNum = 0;
        TextAccuracy.text = "right rate��0%";
        BtnNext.GetComponentInChildren<TextMeshProUGUI>().text = "next";
        BtnNext.onClick.RemoveAllListeners();
        BtnNext.onClick.AddListener(() => Select_Answer(2));

        // ���س�ʼ��Ŀ
        TextCsv();
        LoadAnswer();
    }

   

    /*****************��ȡtxt����******************/
    void TextCsv()
    {
        string UnityPath1 = Application.dataPath + "/StreamingAssets/5.txt";
        string[] allLineText = File.ReadAllLines(UnityPath1);
        for (int i = 0; i < allLineText.Length; i++)
        {
            Debug.Log(allLineText.Length);
        }
        ArrayX = new string[allLineText.Length][];
        //��csv�е����ݴ����ڶ�ά������  
        for (int i = 0; i < allLineText.Length; i++)
        {
            ArrayX[i] = allLineText[i].Split(':');
        }
        /*
        //�鿴�������Ŀ����
        for (int i = 0; i < ArrayX.Length; i++)
        {
            for (int j = 0; j < ArrayX[i].Length; j++)
            {
                Debug.Log(ArrayX[i][j]);
            }
        }
        */
        //������Ŀ״̬
        topicMax = allLineText.Length;
        for (int x = 0; x < topicMax; x++)
        {
            isAnserList.Add(false);
        }
    }

    /*****************������Ŀ******************/
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

        indexText.text = "quiz" + (topicIndex + 1) + ":";//�ڼ���
        TM_Text.text = ArrayX[topicIndex][1];//��Ŀ
        int idx = ArrayX[topicIndex].Length - 3;//�м���ѡ��
        for (int x = 0; x < idx; x++)
        {
            DA_TextList[x].text = ArrayX[topicIndex][x + 2];//ѡ��
        }
    }

    /*****************��ť����******************/
    void Select_Answer(int index)
    {
        switch (index)
        {
            case 0://��ʾ
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
                tipsText.text = "<color=#FFAB08FF>" + "correct answer��" + nM + "</color>";
                break;
            case 1://��һ��
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
            case 2://��һ��
                if (topicIndex < topicMax - 1)
                {
                    topicIndex++;
                    LoadAnswer();
                    if (topicIndex == topicMax - 1)
                    {
                        //��BtnNext�����ָ�Ϊ��end��
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
            case 3://��ת
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

        // �ر�UI
        
        uiGameObject.SetActive(false);
        
        return;
    }

    /*****************��Ŀ�Դ��ж�******************/
    void AnswerRightRrongJudgment(bool check, int index)
    {
        if (check)
        {
            //�ж���Ŀ�Դ�
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

            //��ȷ�ʼ���
            if (isAnserList[topicIndex])
            {
                tipsText.text = "<color=#FF0020FF>" + "������Ѵ����" + "</color>";
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
                TextAccuracy.text = "right rate��" + ((float)isRightNum / anserint * 100).ToString("f2") + "%";
            }

            //���õ�ѡ��
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
