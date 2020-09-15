using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class SaveMonster
{
    public MonsterIds id;
    public float stress = 0;
    public List<SaveSkill> skills = new List<SaveSkill>();
    public List<SaveTool> tools = new List<SaveTool>();
    public List<SaveClothes> clothes = new List<SaveClothes>();

    public SaveMonster(Monster m)
    {
        id = m.GetId();
        stress = m.GetStress();
        foreach(Skill s in m.GetSkills().Values.ToList())
        {
            skills.Add(new SaveSkill(s));
        }
        foreach (Tool t in m.GetTools())
        {
            tools.Add(new SaveTool(t));
        }
        foreach (Clothes c in m.GetClothes())
        {
            clothes.Add(new SaveClothes(c));
        }
    }

    public MonsterIds GetMonsterId() { return id; }
    public float GetStress() { return stress; }
    public List<SaveSkill> GetSaveSkills() { return skills; }
    public List<SaveTool> GetSaveTools() { return tools; }
    public List<SaveClothes> GetSaveClothes() { return clothes; }
}
