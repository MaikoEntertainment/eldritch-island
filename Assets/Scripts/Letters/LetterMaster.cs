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
        }
    }

    void Start()
    {
        if (!unlockedLetters.ContainsKey(LetterId.welcome))
        {
            UnlockLetter(LetterId.welcome);
            UnlockLetter(LetterId.tutorial1);
            MarkLettersAsRead();
            UILetterMaster.GetInstance().LoadLetter(letterDictionary[LetterId.tutorial1]);
            UILetterMaster.GetInstance().LoadLetter(letterDictionary[LetterId.welcome], true);
        }
        if (!unlockedLetters.ContainsKey(LetterId.tutorial2))
            InventoryMaster.GetInstance().GetItem(0).onAmountChange += CheckForLevelUpTutorial;
    }

    public void CheckForLevelUpTutorial(Item i, long change)
    {
        if (i.GetAmount() >= 25 && !unlockedLetters.ContainsKey(LetterId.tutorial2))
        {
            UnlockLetter(LetterId.tutorial2);
            InventoryMaster.GetInstance().GetItem(0).onAmountChange -= CheckForLevelUpTutorial;
        }
    }

    public void Load(List<SaveLetter> savedLetters)
    {
        foreach(SaveLetter sl in savedLetters)
        {
            Letter letter = letterDictionary[sl.GetId()];
            if (letter && !unlockedLetters.ContainsKey(sl.GetId()))
                unlockedLetters.Add(sl.GetId(), letter);
        }
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
