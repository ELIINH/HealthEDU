using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
//using static UnityEditor.Progress;
//using static UnityEditor.Timeline.Actions.MenuPriority;

public class InventoryManager : MonoBehaviour
{
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
        // Update InventoryUI when the scene is loaded
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
            items.Add(new ItemData { itemName = item.name, quantity = 1 }); 
        }
        return items;
    }
    public void SetItems(List<ItemData> items)
    {
        itemList.Clear();
        List<ItemSO> loadedItems = new List<ItemSO>();
        foreach (var itemData in items)
        {
            ItemSO item = Resources.Load<ItemSO>("Items/" + itemData.itemName); 
            // Item Resources are in "Resources/Items" folder
            if (item != null)
            {
                itemList.Add(item);
                loadedItems.Add(item);
            }
        }
    }

}
