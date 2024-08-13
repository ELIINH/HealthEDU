using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using System.Collections.Generic;
using TMPro;

public class TXT_OPRATION : MonoBehaviour
{
    //public TextAsset textTxt;
    //读取文档
    string[][] ArrayX;//题目数据
    string[] lineArray;//读取到题目数据

    public TextMeshProUGUI stuText;
    string[] items = new string[7] { "题号：", "题目：", "A:", "B:", "C:", "D:", "答案：" };

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
        //把csv中的数据储存在二维数组中  
        for (int i = 0; i < allLineText.Length; i++)
        {
            ArrayX[i] = allLineText[i].Split(':');
        }
        //查看保存的题目数据
        int k = 0;
        string texts = "";
        for (int i = 0; i < ArrayX.Length; i++)
        {
            //texts += "题号:";
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
