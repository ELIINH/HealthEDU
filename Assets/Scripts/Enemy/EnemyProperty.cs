using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class EnemyProperty : MonoBehaviour
{
    public string enemyName = "Enemy1";
    public float HP = 100;
    public float maxHP = 100;
    public int exp = 20;
    public int attackValue = 50;
    public float detectionRadius = 10f; // ¼ì²âÍæ¼ÒµÄ°ë¾¶
    public float attackRange = 2f; // ¹¥»÷·¶Î§
    public float attackCooldown = 0.5f; // ¹¥»÷ÀäÈ´Ê±¼ä
    public int pickableCount = 4;

    public Image hpProgressBar;

    // Start is called before the first frame update
    void Start()
    {
        HP = maxHP;
        hpProgressBar = transform.Find("Canvas/HPBar/ProgressBar").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //hpProgressBar = transform.Find("Canvas/HPBar/ProgressBar").GetComponent<Image>();
        UpdataHPBar();
    }

    public void UpdataHPBar()
    {
        if (hpProgressBar != null)
        {
            hpProgressBar.fillAmount = (float)HP / maxHP;
            //Debug.Log("HP: " + HP + " maxHP: " + maxHP + " fillAmount: " + hpProgressBar.fillAmount);
        }
        else
        {
            Debug.LogError("hpBar is not assigned in the Inspector");
        }
    }
}
