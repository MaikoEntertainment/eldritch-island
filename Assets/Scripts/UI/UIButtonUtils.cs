using UnityEngine;
using System.Runtime.InteropServices;
using System;

public class UIButtonUtils : MonoBehaviour
{
    public void OpenMonsterDraft()
    {
        UIMonsterPickerMaster.GetInstance().ShowMonsterDraft();
    }

    public void CloseMonsterDraft()
    {
        UIMonsterPickerMaster.GetInstance().HideMonsterDraft();
    }

    public void OpenMenu()
    {
        UIMenuMaster.GetInstance().ShowMenu();
    }
    #if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void DownloadJson(string jsonString);
    #endif

    public void DownloadSave()
    {
        SaveFile save = SavingMaster.GetInstace().cacheSave;
        string jsonString = JsonUtility.ToJson(save).ToString();

    #if UNITY_WEBGL && !UNITY_EDITOR
        Console.WriteLine("downloading...");
        DownloadJson(jsonString);
    #endif
    }
}
