using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class InventoryManager : MonoBehaviour
{
    //单例模式
    public static InventoryManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance!=null && Instance != this)
        {
            Destroy(gameObject);return;
        }
        Instance = this;
    }

    public List<ItemSO> itemList;
    public ItemSO defaultWeapon;
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 场景加载完成后更新 InventoryUI
        InventoryUI.Instance.UpdateInventory(itemList);
    }
    public void AddItem(ItemSO item)
    {
        itemList.Add(item);
        InventoryUI.Instance.AddItem(item);

        MessageUI.Instance.Show("You get a: " + item.name);
    }
    public void RemoveItem(ItemSO itemSO)
    {
        itemList.Remove(itemSO);
    }

    public List<ItemData> GetItems()
    {
        List<ItemData> items = new List<ItemData>();
        foreach (var item in itemList)
        {
            items.Add(new ItemData { itemName = item.name, quantity = 1 }); // 假设每个物品的数量为1
        }
        return items;
    }
    public void SetItems(List<ItemData> items)
    {
        itemList.Clear();
        List<ItemSO> loadedItems = new List<ItemSO>();
        foreach (var itemData in items)
        {
            ItemSO item = Resources.Load<ItemSO>("Items/" + itemData.itemName); // 假设物品资源在 "Resources/Items" 文件夹中
            if (item != null)
            {
                itemList.Add(item);
                loadedItems.Add(item);
                //UnityEngine.Debug.Log("加载物品：" + item.name);
            }
        }
        //InventoryUI.Instance.UpdateInventory(loadedItems);
    }

}
