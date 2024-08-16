using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperty : MonoBehaviour
{
    public Dictionary<PropertyType, List<Property>> propertyDict;
    public int hpValue=100;
    public int maxHp=100;
    public float energyValue=100;
    public float maxEnergy =100;
    public int mentalValue = 100;
    public int level = 1;
    public int currentExp = 0;
    public int attackValue = 10;

    public int energyDecreaseRate = 1; // 每秒减少的能量值
    private float energyDecreaseTimer = 0f;

    // Start is called before the first frame update
    void Awake()
    {
        propertyDict = new Dictionary<PropertyType, List<Property>>();
        
        //propertyDict.Add(PropertyType.SpeedValue, new List<Property>());
        //propertyDict.Add(PropertyType.AttackValue, new List<Property>());

        //AddProperty(PropertyType.SpeedValue, 5);
        //AddProperty(PropertyType.AttackValue, 20);

        EventCenter.OnEnemyDied += OnEnemyDied;
    }

    void Update()
    {
        // 每秒减少能量值
        energyDecreaseTimer += Time.deltaTime;
        if (energyDecreaseTimer >= 1f)
        {
            energyValue -= energyDecreaseRate;
            energyDecreaseTimer = 0f;

            // 确保能量值在0-maxEnergy之间

            energyValue = Mathf.Clamp(energyValue, 0, maxEnergy);
            if (energyValue == 0)
            {
                hpValue -= 5;
            }
        }
        // 假设按下 K 键模拟玩家死亡
        if (Input.GetKeyDown(KeyCode.K))
        {
            Die();
        }
    }

    public void UseDrug(ItemSO itemSO)
    {
        foreach(Property p in itemSO.propertyList)
        {
            AddProperty(p.propertyType, p.value);
        }
    }

    public void AddProperty(PropertyType pt,int value)
    {
        switch (pt)
        {
            case PropertyType.HPValue:
                hpValue += value;
                return;
            case PropertyType.EnergyValue:
                energyValue += value;
                return;
            //case PropertyType.MentalValue:
             //   mentalValue += value;
             //   return;
            case PropertyType.AttackValue:
                attackValue += value;
                return;
        }

        List<Property> list;
        propertyDict.TryGetValue(pt, out list);
        list.Add(new Property(pt,value));
    }
    public void RemoveProperty(PropertyType pt, int value)
    {
        switch (pt)
        {
            case PropertyType.HPValue:
                hpValue -= value;
                return;
            case PropertyType.EnergyValue:
                energyValue -= value;
                return;
            //case PropertyType.MentalValue:
            //    mentalValue -= value;
             //   return;
        }

        List<Property> list;
        propertyDict.TryGetValue(pt, out list);

        list.Remove(list.Find(x => x.value == value));
    }
    private void OnDestroy()
    {
        EventCenter.OnEnemyDied -= OnEnemyDied;
    }
    private void OnEnemyDied(Enemy enemy)
    {
        this.currentExp += enemy.enemyProperty.exp;

        if (currentExp >= level * 30)
        {
            currentExp -= level * 30;
            levelUp();
            //level++;
        }
        PlayerPropertyUI.Instance.UpdatePlayerPropertyUI();
    }

    public void levelUp()
    {
        level++;
        hpValue = 100;
        energyValue = 100;
        mentalValue = 100;
        attackValue += 10;
    }

    public void TakeDamage(int damage)
    {
        hpValue -= damage;

        if (hpValue <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player Die");
        GameManager.Instance.LoadGame();
    }

    
}
