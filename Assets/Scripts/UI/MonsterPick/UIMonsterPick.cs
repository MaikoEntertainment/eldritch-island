using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMonsterPick : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI species;
    public TextMeshProUGUI ability;
    public GridLayoutGroup skills;

    public UIMonsterPickSkill skillPrefab;

    protected Monster m;

    public void Load(Monster monster)
    {
        m = monster;
        species.text = monster.GetSpecies();
        ability.text = monster.GetAbility();
        icon.sprite = monster.GetIcon();
        List<Skill> list = monster.GetInitialSkills();
        foreach (Skill s in list)
        {
            if (s.GetLevel() != 0)
                Instantiate(skillPrefab.gameObject, skills.transform).GetComponent<UIMonsterPickSkill>().Load(s);
        }
    }

    public void Pick()
    {
        MonsterMaster.GetInstance().PickMonster(m.GetId());
        UIMonsterPickerMaster.GetInstance().HideMonsterDraft();
    }

}
