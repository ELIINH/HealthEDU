using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPropertyUI : MonoBehaviour
{
    public static PlayerPropertyUI Instance { get; private set; }

    private GameObject uiGameObject;

    private Image hpProgressBar;
    private TextMeshProUGUI hpText;

    private Image energyBar; 
    private TextMeshProUGUI energyText;

    private Image levelProgressBar;
    private TextMeshProUGUI levelText;

    private GameObject propertyGrid;
    private TextMeshProUGUI propertyText;
    //private Image weaponIcon;

    private PlayerProperty pp;
    private PlayerAttack pa;

    private void Awake()
    {
        if(Instance!=null &&Instance != this)
        {
            Destroy(gameObject);return;
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        uiGameObject = transform.Find("UI").gameObject;
        hpProgressBar = transform.Find("UI/HPProgressBar/ProgressBar").GetComponent<Image>();
        hpText = transform.Find("UI/HPProgressBar/HPText").GetComponent<TextMeshProUGUI>();

        energyBar = transform.Find("UI/EnergyBar/ProgressBar").GetComponent<Image>();
        energyText = transform.Find("UI/EnergyBar/EnergyText").GetComponent<TextMeshProUGUI>();

        levelProgressBar = transform.Find("UI/LevelProgressBar/ProgressBar").GetComponent<Image>();
        levelText = transform.Find("UI/LevelProgressBar/LevelText").GetComponent<TextMeshProUGUI>();

        propertyGrid = transform.Find("UI/PropertyGrid").gameObject;
        propertyText = transform.Find("UI/PropertyGrid/Property").GetComponent<TextMeshProUGUI>();
 

        GameObject player= GameObject.FindGameObjectWithTag(Tag.PLAYER);
        pp = player.GetComponent<PlayerProperty>();
 
        UpdatePlayerPropertyUI();
        Show();
    }

    private void Update()
    {
        UpdatePlayerPropertyUI();
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (uiGameObject.activeSelf)
            {
                Hide();
            }
            else
            {
                Show();
            }

        }
    }

    public void UpdatePlayerPropertyUI()
    {
        hpProgressBar.fillAmount = (float)pp.hpValue / pp.maxHp;
        hpText.text = pp.hpValue + "/" + pp.maxHp;

        energyBar.fillAmount = pp.energyValue / pp.maxEnergy;
        energyText.text = pp.energyValue + "/"+pp.maxEnergy;

        levelProgressBar.fillAmount = pp.currentExp*1.0f / (pp.level*30);
        levelText.text = pp.level.ToString();

        ClearGrid();

        AddProperty("Attack: " + pp.attackValue);

        foreach (var item in pp.propertyDict)
        {
            string propertyName = "";
            switch (item.Key)
            {
                case PropertyType.HPValue:
                    propertyName = "HP: ";
                    break;
                case PropertyType.EnergyValue:
                    propertyName = "Energy: ";
                    break;
                case PropertyType.AttackValue:
                    propertyName = "Attack:";
                    break;
                default:
                    break;
            }

            int sum = 0;
            foreach (var item1 in item.Value)
            {
                sum += item1.value;
            }
            AddProperty(propertyName + sum);
        }

    }
    private void ClearGrid()
    {
        foreach (Transform child in propertyGrid.transform)
        {
            if (child.gameObject.activeSelf)
            {
                Destroy(child.gameObject);
            }
        }
    }
    private void AddProperty( string propertyStr)
    {
        propertyText.text = propertyStr;


    }

    private void Show()
    {
        uiGameObject.SetActive(true);
    }
    private void Hide()
    {
        uiGameObject.SetActive(false);
    }
}
