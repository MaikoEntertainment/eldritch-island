using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMonsterViewerHandler : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI species;
    public TextMeshProUGUI ability;
    public TextMeshProUGUI stress;
    public TextMeshProUGUI toolsUsed;
    public TextMeshProUGUI toolsSlots;
    public TextMeshProUGUI clothesUsed;
    public TextMeshProUGUI clothesSlots;

    public Transform equippedToolList;
    public Transform equippedClothesList;
    public Transform skillsList;
    public Transform toolPicker;
    public Transform clothesPicker;

    public Button plusToolButton;
    public Button plusClothesButton;

    public UIMonsterViewerTool toolPrefab;
    public UIMonsterViewerClothes clothesPrefab;
    public UITaskMonsterSkill skillPrefab;
    public UIMonsterViewerToolsPicker toolPickerPrefab;
    public UIMonsterViewerClothesPicker clothesPickerPrefab;

    private Monster monster;

    public void Load(Monster m)
    {
        monster = m;
        icon.sprite = m.GetIcon();
        species.text = m.GetSpecies();
        ability.text = m.GetAbility();
        stress.text = m.GetStress().ToString("F1") + "/"+m.GetStressMax();
        UpdateEquipment();
        UpdateSkills();
    }

    public void UpdateEquipment()
    {
        UpdateTools();
        UpdateClothes();
    }

    public void UpdateClothes()
    {
        foreach (Transform c in equippedClothesList)
        {
            if (c.gameObject != plusClothesButton.gameObject)
                Destroy(c.gameObject);
        }
        int clothesSlots = monster.GetClothesSlots();
        this.clothesSlots.text = clothesSlots.ToString();
        foreach (Clothes c in monster.GetClothes())
        {
            Instantiate(clothesPrefab.gameObject, equippedClothesList).GetComponent<UIMonsterViewerClothes>().Load(c, monster).onClose += (Clothes to) => { UpdateClothes(); UpdateSkills(); }; ;
            clothesSlots--;
        }
        clothesUsed.text = (monster.GetClothesSlots() - clothesSlots).ToString();
        clothesUsed.color = clothesSlots > 0 ? Utils.GetSuccessColor() : Utils.GetWrongColor();
        if (clothesSlots > 0)
            plusClothesButton.gameObject.SetActive(true);
        else
            plusClothesButton.gameObject.SetActive(false);
    }

    public void UpdateTools()
    {
        foreach (Transform t in equippedToolList)
        {
            if (t.gameObject != plusToolButton.gameObject)
                Destroy(t.gameObject);
        }
        int toolSlots = monster.GetToolSlots();
        toolsSlots.text = toolSlots.ToString();
        foreach (Tool t in monster.GetTools())
        {
            Instantiate(toolPrefab.gameObject, equippedToolList).GetComponent<UIMonsterViewerTool>().Load(t, monster).onClose += (Tool to) => { UpdateTools(); UpdateSkills(); };
            toolSlots--;
        }
        toolsUsed.text = (monster.GetToolSlots() - toolSlots).ToString();
        toolsUsed.color = toolSlots > 0 ? Utils.GetSuccessColor() : Utils.GetWrongColor();
        if (toolSlots > 0)
            plusToolButton.gameObject.SetActive(true);
        else
            plusToolButton.gameObject.SetActive(false);
    }

    public void UpdateSkills()
    {
        foreach(Transform skill in skillsList)
        {
            Destroy(skill.gameObject);
        }
        foreach(Skill s in monster.GetFinalSkills().Values.ToList())
        {
            Instantiate(skillPrefab.gameObject, skillsList).GetComponent<UITaskMonsterSkill>().Load(s);
        }
    }

    public void OpenToolsPicker()
    {
        foreach (Transform t in toolPicker)
            Destroy(t.gameObject);
        Instantiate(toolPickerPrefab.gameObject, toolPicker).GetComponent<UIMonsterViewerToolsPicker>().Load(monster);
    }

    public void OpenClothesPicker()
    {
        foreach (Transform t in clothesPicker)
            Destroy(t.gameObject);
        Instantiate(clothesPickerPrefab.gameObject, clothesPicker).GetComponent<UIMonsterViewerClothesPicker>().Load(monster);
    }

    public void Close()
    {
        UIMonsterViewerMaster.GetInstance().Close();
        UITasklessMonsterMaster.GetInstance().UpdateTasklessMonsters();
    }
}
