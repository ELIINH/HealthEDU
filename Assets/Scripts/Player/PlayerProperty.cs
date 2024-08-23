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

    public float energyDecreaseRate = 1f; //energy decrease per 2 seconds
    private float energyDecreaseTimer = 0f;
    private Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        propertyDict = new Dictionary<PropertyType, List<Property>>();
        animator = GetComponentInChildren<Animator>();
        EventCenter.OnEnemyDied += OnEnemyDied;
    }

    void Update()
    {
        // decrease energy value per second
        energyDecreaseTimer += Time.deltaTime;
        if (energyDecreaseTimer >= 2f)
        {
            energyValue -= energyDecreaseRate;
            energyDecreaseTimer = 0f;

            //make sure energy value is in the range of 0 to maxEnergy
            //energyValue = Mathf.Clamp(energyValue, 0, maxEnergy);
            //hpValue = Mathf.Clamp(hpValue, 0, maxHp);
            if(energyValue > maxEnergy)
            {
                energyValue= maxEnergy;
            }
            if (hpValue > maxHp)
            {
                hpValue = maxHp;
            }
            if (energyValue == 0)
            {
                hpValue -= 5;
            }
            if (hpValue == 0)
            {
                Die();
            }
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
                hpValue = Mathf.Clamp(hpValue, 0, maxHp);
                return;
            case PropertyType.EnergyValue:
                energyValue += value;
                energyValue = Mathf.Clamp(energyValue, 0, maxEnergy);
                
                return;
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
        }
        PlayerPropertyUI.Instance.UpdatePlayerPropertyUI();
    }

    public void levelUp()
    {
        level++;
        hpValue = maxHp;
        energyValue = maxEnergy;
        //mentalValue = 100;
        attackValue += 5;
        MessageUI.Instance.Show("Level UP");
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
        //GameManager.Instance.LoadGame();

        MessageUI.Instance.Show("Fail!");
        GetComponent<Collider>().enabled = false;
        StartCoroutine(WaitForDeathAnimation());
    }
    private IEnumerator WaitForDeathAnimation()
    {
        animator.SetTrigger("IsDead");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        GameManager.Instance.LoadGame();
        //Destroy(this.gameObject);
    }

}
