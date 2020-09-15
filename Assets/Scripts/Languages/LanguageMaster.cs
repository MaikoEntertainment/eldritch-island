using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageMaster : MonoBehaviour
{
    private static LanguageMaster _instance;

    [SerializeField]
    protected TextLanguageDatabase database;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public static LanguageMaster GetInstance() { return _instance; }

    public string GetSkillName(SkillIds id)
    {
        return database.GetSkillName(id);
    }
}
