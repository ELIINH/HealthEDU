using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using System.Collections.Generic;
using TMPro;

public class TXT_OPRATION : MonoBehaviour
{
    //public TextAsset textTxt;
    //��ȡ�ĵ�
    string[][] ArrayX;//��Ŀ����
    string[] lineArray;//��ȡ����Ŀ����

    public TextMeshProUGUI stuText;
    string[] items = new string[7] { "��ţ�", "��Ŀ��", "A:", "B:", "C:", "D:", "�𰸣�" };

    void Start()
    {
        LoadTxt();
        //Debug.Log(textTxt.text);
    }

    private void LoadTxt()
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
        //�鿴�������Ŀ����
        int k = 0;
        string texts = "";
        for (int i = 0; i < ArrayX.Length; i++)
        {
            //texts += "���:";
            for (int j = 0; j < ArrayX[i].Length; j++)
            {
                //Debug.Log(ArrayX[i][j]);
                texts += items[k];
                texts += ArrayX[i][j];
                texts += '\n';
                k++;
                if (k == 7)
                {
                    k = 0;
                }
            }
        }

        stuText.text = texts;

    }

}
