using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyProperty : MonoBehaviour
{
    public string enemyName = "Enemy1";
    public int HP = 100;
    public int exp = 20;
    public int attackValue = 50;
    public float detectionRadius = 10f; // ¼ì²âÍæ¼ÒµÄ°ë¾¶
    public float attackRange = 2f; // ¹¥»÷·¶Î§
    public float attackCooldown = 0.5f; // ¹¥»÷ÀäÈ´Ê±¼ä
    public int pickableCount = 4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
