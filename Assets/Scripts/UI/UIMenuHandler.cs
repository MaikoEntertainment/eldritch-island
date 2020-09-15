using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIMenuHandler : MonoBehaviour
{
    public Slider musicVolume;
    public Slider effectVolume;

    public GameObject saveFile;
    public InputField saveFileText;

    public GameObject loadFile;
    public InputField loadFileText;

    private void Start()
    {
        musicVolume.value = SoundMaster.GetInstance().GetMusicVolume();
        effectVolume.value = SoundMaster.GetInstance().GetEffectVolume();
    }
    public void OnMusicChange(float volume)
    {
        SoundMaster.GetInstance().SetMusicVolume(volume);
    }

    public void OnEffectChange(float volume)
    {
        SoundMaster.GetInstance().SetEffectVolume(volume);
    }

    public void DeleteSave()
    {
        SavingMaster.GetInstace().DeleteSaveFile();
    }

    public void Close()
    {
        Destroy(gameObject);
    }

    public void OpenSaveViewer()
    {
        saveFile.SetActive(true);
        saveFileText.text = JsonUtility.ToJson(SavingMaster.GetInstace().cacheSave);
    }

    public void CloseSaveViewer()
    {
        saveFile.SetActive(false);
    }

    public void OpenLoadViewer()
    {
        loadFile.SetActive(true);
    }

    public void CloseLoadViewer()
    {
        loadFile.SetActive(false);
    }

    public void LoadGame()
    {
        string importedValue = loadFileText.text;
        SavingMaster.GetInstace().LoadSaveFromJson(importedValue);

    }
}
