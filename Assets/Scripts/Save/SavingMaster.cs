using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavingMaster: MonoBehaviour
{
    public static SavingMaster _instance;
    public SaveFile cacheSave = new SaveFile();
    
    private static string fileName = "player.soul0";

    public float saveInterval = 30;
    private float timePassed;

    void Awake()
    {
        if (_instance)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        //Load();
        LoadJson();
    }

    private void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > saveInterval)
        {
            timePassed = 0;
            Save();
        }
    }

    public static SavingMaster GetInstace() { return _instance; }

    public void Save()
    {
        SaveInventory();
        SaveMonsters();
        SaveBuildings();
        SaveLetters();
        SaveStatistics();
        SavePreferences();
        SaveUpgrades();
        SaveToLocalJson();
        NotificationMaster.GetInstance()?.SendSaveNotifcation();
        print("Game Saved!");
    }

    private void SaveToLocalJson()
    {
        string json = JsonUtility.ToJson(cacheSave);
        PlayerPrefs.SetString(fileName, json);
    }

    public void LoadJson()
    {
        if (PlayerPrefs.HasKey(fileName))
        {
            string savedJson = PlayerPrefs.GetString(fileName);
            SaveFile data = JsonUtility.FromJson<SaveFile>(savedJson);
            cacheSave = data;
            LoadIntoGame();
        }

    }

    public void LoadSaveFromJson(string savedJson)
    {
        SaveFile data = JsonUtility.FromJson<SaveFile>(savedJson);
        cacheSave = data;
        SaveToLocalJson();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void DeleteSaveFile()
    {
        cacheSave = new SaveFile();
        PlayerPrefs.DeleteKey(fileName);
    }

    private void LoadIntoGame()
    {
        if (cacheSave.saveInventory != null) InventoryMaster.GetInstance().Load(cacheSave.saveInventory);
        if (cacheSave.savedMonsters != null) MonsterMaster.GetInstance().Load(cacheSave.savedMonsters);
        if (cacheSave.savedBuildings != null) BuildingMaster.GetInstance().Load(cacheSave.savedBuildings);
        if (cacheSave.savedLetters!=null) LetterMaster.GetInstance().Load(cacheSave.savedLetters);
        if (cacheSave.savedStatisticsNumber != null) StatisticsMaster.GetInstance().Load(cacheSave.savedStatisticsNumber);
        if (cacheSave.savedUpgrades != null) UpgradeMaster.GetInstance().Load(cacheSave.savedUpgrades);
        SoundMaster.GetInstance()?.SetEffectVolume(cacheSave.effectsVolume);
        SoundMaster.GetInstance()?.SetMusicVolume(cacheSave.musicVolume);
    }

    public void SavePreferences()
    {
        cacheSave.musicVolume = SoundMaster.GetInstance().GetMusicVolume();
        cacheSave.effectsVolume = SoundMaster.GetInstance().GetEffectVolume();
    }

    public void SaveInventory()
    {
        List<Item> items = InventoryMaster.GetInstance().GetItems().Values.ToList();
        List<Tool> tools = InventoryMaster.GetInstance().GetTools();
        List<Clothes> clothes = InventoryMaster.GetInstance().GetClothes();
        cacheSave.SaveInventory(items, tools, clothes);
    }
    public void SaveMonsters()
    {
        List<Monster> monsters = MonsterMaster.GetInstance().GetActiveMonsters().Values.ToList();
        cacheSave.SaveMonsters(monsters);
    }
    public void SaveBuildings()
    {
        List<Building> buildings = BuildingMaster.GetInstance().GetUnlockedBuildings();
        cacheSave.SaveBuildings(buildings);
    }
    public void SaveLetters()
    {
        List<Letter> letters = LetterMaster.GetInstance().GetUnlockedLetters().Values.ToList();
        cacheSave.SaveLetters(letters);
    }

    public void SaveStatistics()
    {
        List<StatisticValue> statistics = StatisticsMaster.GetInstance().GetStatistics().Values.ToList();
        cacheSave.SaveStatistics(statistics);
    }
    public void SaveUpgrades()
    {
        List<Upgrade> upgrades = UpgradeMaster.GetInstance().GetUpgrades().Values.ToList();
        cacheSave.SaveUpgrades(upgrades);
    }
}
