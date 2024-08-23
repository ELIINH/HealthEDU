using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Progress;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance { get; private set; }
    private GameObject uiGameObject;
    public GameObject content;
    public GameObject itemPrefab;
    private bool isShow=false;

    public ItemDetailUI itemDetailUI;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        uiGameObject = transform.Find("UI").gameObject;
        content = transform.Find("UI/ListBg/Scroll View/Viewport/Content").gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        uiGameObject = transform.Find("UI").gameObject;
        content = transform.Find("UI/ListBg/Scroll View/Viewport/Content").gameObject;
        Hide();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (isShow)
            {
                Hide();
                isShow = false;
            }
            else
            {
                Show();
                isShow = true;
            }
        }
    }


    public void Show()
    {
        uiGameObject.SetActive(true);
        //itemDetailUI.gameObject.SetActive(true);
    }
    public void Hide()
    {
        uiGameObject.SetActive(false);
    }
    public void AddItem(ItemSO itemSO)
    {
        if (Instance == null)
        {
            Debug.LogError("InventoryUI instance is null.");
            return;
        }

        GameObject itemGo = GameObject.Instantiate(itemPrefab);
        content = transform.Find("UI/ListBg/Scroll View/Viewport/Content").gameObject;
        itemGo.transform.SetParent(content.transform);
        RectTransform rectTransform = itemGo.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            rectTransform.localScale = Vector3.one;
        }
        ItemUI itemUI = itemGo.GetComponent<ItemUI>();
        
        itemUI.InitItem(itemSO);

    }
    public void OnItemClick(ItemSO itemSO,ItemUI itemUI)
    {
        
        itemDetailUI.UpdateItemDetailUI(itemSO,itemUI);
    }

    public void OnItemUse(ItemSO itemSO,ItemUI itemUI)
    {
        Destroy(itemUI.gameObject);
        InventoryManager.Instance.RemoveItem(itemSO);

        GameObject.FindGameObjectWithTag(Tag.PLAYER).GetComponent<Player>().UseItem(itemSO);
    }
    public void ClearInventory()
    {
        if (content == null)
        {
            Debug.LogError("Content is not initialized.");
            return;
        }
        //clear all children
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void UpdateInventory(List<ItemSO> items)
    {
        ClearInventory();
        foreach (var item in items)
        {
            AddItem(item);
        }
    }
}
