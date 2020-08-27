using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILetterMaster : MonoBehaviour
{
    public static UILetterMaster _instance;

    public Transform letterPanel;
    public Transform letterListPanel;

    public UILetterList letterListPrefab;

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

    public static UILetterMaster GetInstance() { return _instance; }

    public void LoadLetter(Letter letter)
    {
        CloseLetter();
        Instantiate(letter.GetUIPrefab(), letterPanel);
    }

    public void CloseLetter()
    {
        foreach (Transform t in letterPanel)
        {
            Destroy(t.gameObject);
        }
    }

    public void OpenLetterList()
    {
        LetterMaster.GetInstance().MarkLettersAsRead();
        foreach (Transform t in letterListPanel)
        {
            Destroy(t.gameObject);
        }
        Instantiate(letterListPrefab.gameObject, letterListPanel).GetComponent<UILetterList>().Load();
    }
}
