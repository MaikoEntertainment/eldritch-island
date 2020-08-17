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

    public Transform toolList;
    public Transform clothesList;
    public Transform skillsList;

    public Button plusToolButton;
    public Button plusClothesButton;

    public UIMonsterViewerTool toolPrefab;
    public UIMonsterViewerClothes clothesPrefab;
    public UITaskMonsterSkill skillPrefab;

    private Monster monster;

    public void Load(Monster m)
    {
        monster = m;
        icon.sprite = m.GetIcon();
        species.text = m.GetSpecies();
        ability.text = m.GetAbility();
        stress.text = m.GetStress() + "/"+m.GetStressMax();
        UpdateEquipment();
        UpdateSkills();
    }

    public void UpdateEquipment()
    {
        foreach (Transform t in toolList)
        {
            if (t.gameObject!=plusToolButton.gameObject)
                Destroy(t.gameObject);
        }
        foreach (Transform c in clothesList)
        {
            if (c.gameObject != plusClothesButton.gameObject)
                Destroy(c.gameObject);
        }
        int toolSlots = monster.GetToolSlots();
        int clothesSlots = monster.GetClothesSlots();
        foreach (Tool t in monster.GetTools())
        {
            Instantiate(toolPrefab.gameObject, toolList).GetComponent<UIMonsterViewerTool>().Load(t, monster);
            toolSlots--;
        }

        if (toolSlots > 0)
            plusToolButton.gameObject.SetActive(true);
        else
            plusToolButton.gameObject.SetActive(false);
        if (clothesSlots > 0)
            plusClothesButton.gameObject.SetActive(true);
        else
            plusClothesButton.gameObject.SetActive(false);

        foreach (Clothes c in monster.GetClothes())
        {
            Instantiate(clothesPrefab.gameObject, toolList).GetComponent<UIMonsterViewerClothes>().Load(c, monster);
        }
    }

    public void UpdateSkills()
    {
        foreach(Transform skill in skillsList)
        {
            Destroy(skill.gameObject);
        }
        foreach(Skill s in monster.GetSkills().Values.ToList())
        {
            Instantiate(skillPrefab.gameObject, skillsList).GetComponent<UITaskMonsterSkill>().Load(s);
        }
    }

    public void Close()
    {
        UIMonsterViewerMaster.GetInstance().Close();
    }
}
