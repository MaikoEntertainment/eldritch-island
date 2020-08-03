using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class ToolBase: ScriptableObject
{
    [SerializeField]
    protected int id;
    [SerializeField]
    protected Sprite icon;
    [SerializeField]
    protected long durability;
    [SerializeField]
    protected TextLanguageOwn name;
    [SerializeField]
    protected TextLanguageOwn description;
    [SerializeField]
    protected List<SkillBonus> skillBonuses;
    [SerializeField]
    protected Tag[] tags;

    public int GetId() { return id; }
    public double GetDurability() { return durability; }
    public Tag[] GetTags() { return tags; }
    public List<SkillBonus> GetSkillBonuses() { return skillBonuses; }
    public Sprite GetIcon() { return icon; }
    public void Use()
    {
        
    }
}
