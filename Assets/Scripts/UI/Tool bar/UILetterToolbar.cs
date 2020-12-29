using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UILetterToolbar : MonoBehaviour
{
    public TextMeshProUGUI lettersAmount;
    public UIBeat unreadBeat;
    public Color unreadColor;
    public AudioClip newLetter;

    private void Start()
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
            unreadBeat.enabled = true;
            SoundMaster.GetInstance().PlayEffect(newLetter);
        }
        else
        {
            lettersAmount.color = Color.white;
            unreadBeat.enabled = false;
        }
    }
}
