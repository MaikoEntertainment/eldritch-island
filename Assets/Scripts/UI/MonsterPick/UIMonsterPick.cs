using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMonsterPick : MonoBehaviour
{
    public TextMeshProUGUI species;
    public TextMeshProUGUI ability;
    public GridLayoutGroup skills;

    public GameObject skillPrefab;

    public void Load(Monster monster)
    {
        species.text = monster.GetSpecies();
        ability.text = monster.GetAbility();
        foreach(Skill s in monster.GetSkills().Values.ToList())
        {
            if (s.GetLevel() != 0)
                Instantiate(skillPrefab, skills.transform);
        }
    }
}
