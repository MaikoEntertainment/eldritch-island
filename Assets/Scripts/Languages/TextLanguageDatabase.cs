using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class TextLanguageDatabase : ScriptableObject
{
    [SerializeField]
    protected LanguageSkills languageSkills;

    public string GetSkillName(SkillIds id) { return languageSkills.GetSkillName(id); }
}
