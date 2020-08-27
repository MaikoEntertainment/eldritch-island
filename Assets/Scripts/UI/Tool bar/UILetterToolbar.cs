using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UILetterToolbar : MonoBehaviour
{
    public TextMeshProUGUI lettersAmount;
    public Color unreadColor;
    public AudioClip newLetter;

    private void Awake()
    {
        LetterMaster.GetInstance().onLettersAmountChange += UpdateUnread;
    }

    public void OpenLetters()
    {
        UILetterMaster.GetInstance().OpenLetterList();
    }

    public void UpdateUnread(int amount)
    {
        lettersAmount.text = amount.ToString();
        if (amount > 0)
        {
            lettersAmount.color = unreadColor;
            SoundMaster.GetInstance().PlayEffect(newLetter);
        }
        else
            lettersAmount.color = Color.white;
    }
}
