﻿using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMonsterViewerClothesPicker : MonoBehaviour
{
    public Transform clothesList;
    public GameObject clothesViewer;
    public GameObject removeMultiplePanel;

    public Image icon;
    public TextMeshProUGUI clothesName;
    public TextMeshProUGUI durability;
    public TextMeshProUGUI level;
    public TextMeshProUGUI description;
    public Transform skillBonusList;
    public Text deleteMultipleLevelGap;

    public UIMonsterViewerSkillBonus skillPrefab;
    public UITaskMonsterClothes clothesPrefab;

    private Clothes clothes;
    private Monster currentMonster;

    public void Load(Monster m)
    {
        currentMonster = m;
        LoadToolList();
    }

    public void LoadToolList()
    {
        List<Clothes> clothesList = InventoryMaster.GetInstance().GetClothes();
        foreach (Transform t in this.clothesList)
        {
            Destroy(t.gameObject);
        }
        foreach (Clothes t in clothesList)
        {
            UITaskMonsterClothes uiTool = Instantiate(clothesPrefab.gameObject, this.clothesList).GetComponent<UITaskMonsterClothes>().Load(t);
            uiTool.onClick += View;
        }
    }

    public void Close()
    {
        Destroy(gameObject);
    }

    public void View(Clothes c)
    {
        clothes = c;
        clothesViewer.SetActive(true);
        icon.sprite = c.GetClothes().GetIcon();
        clothesName.text = c.GetClothes().GetName();
        description.text = c.GetDescription();
        durability.text = c.GetDurabilityLeft().ToString("F0");
        level.text = c.GetTier().ToString();
        foreach (Transform skill in skillBonusList)
            Destroy(skill.gameObject);
        foreach (SkillBonus s in clothes.GetSkillBonuses())
        {
            Instantiate(skillPrefab.gameObject, skillBonusList).GetComponent<UIMonsterViewerSkillBonus>().Load(s);
        }
    }

    public void CloseView()
    {
        clothesViewer.SetActive(false);
    }

    public void Equip()
    {
        currentMonster.EquipClothes(clothes);
        CloseView();
        LoadToolList();
        UIMonsterViewerMaster.GetInstance().Load(currentMonster);
    }

    public void Remove()
    {
        InventoryMaster.GetInstance().RemoveClothes(clothes);
        CloseView();
        LoadToolList();
    }
    public void ToogleRemoveMultipleView()
    {
        removeMultiplePanel.SetActive(!removeMultiplePanel.activeSelf);
    }
    public void RemoveMultiple()
    {
        int value = int.Parse(deleteMultipleLevelGap.text);
        InventoryMaster.GetInstance().RemoveClothesLevelGap(value);
        ToogleRemoveMultipleView();
        LoadToolList();
    }
}
