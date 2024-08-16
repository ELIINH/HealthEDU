using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerUI : MonoBehaviour
{
    public void OnSaveButtonClicked()
    {
        GameManager.Instance.SaveGame();
    }

    public void OnLoadButtonClicked()
    {
        GameManager.Instance.LoadGame();
    }
}
