using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterMaster : MonoBehaviour
{
    public static LetterMaster _instance;

    public List<Letter> letters = new List<Letter>();
    protected Dictionary<LetterId, Letter> letterDictionary;

    protected Dictionary<LetterId, Letter> unlockedLetters = new Dictionary<LetterId, Letter>();

    protected int unreadLetters = 0;

    public delegate void LettersChange(int newNumber);
    public event LettersChange onLettersAmountChange;

    private void Awake()
    {
        if (_instance)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
            InitializeDictioaries();
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        UnlockLetter(LetterId.welcome);
        MarkLettersAsRead();
        UILetterMaster.GetInstance().LoadLetter(letterDictionary[LetterId.welcome]);
        UnlockLetter(LetterId.tutorial1);
        UnlockLetter(LetterId.tutorial2);
    }

    private void InitializeDictioaries()
    {
        letterDictionary = new Dictionary<LetterId, Letter>();
        foreach (Letter l in letters)
            letterDictionary.Add(l.GetId(), l);
    }

    public static LetterMaster GetInstance() { return _instance; }

    public void UnlockLetter(LetterId id)
    {
        Letter letter = letterDictionary[id];
        if (!unlockedLetters.ContainsKey(id))
        {
            unlockedLetters.Add(id, letter);
            unreadLetters++;
            onLettersAmountChange?.Invoke(unreadLetters);
        }
    }
    public void MarkLettersAsRead()
    {
        unreadLetters = 0;
        onLettersAmountChange?.Invoke(0);
    }

    public Dictionary<LetterId, Letter> GetUnlockedLetters()
    {
        return unlockedLetters;
    }
}
