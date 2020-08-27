using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UILetterItem : MonoBehaviour
{
    public TextMeshProUGUI title;
    public AudioClip onOpenSound;

    private Letter letter;
    public UILetterItem Load(Letter l)
    {
        letter = l;
        title.text = l.GetTitle();
        return this;
    }

    public void OnClick()
    {
        UILetterMaster.GetInstance().LoadLetter(letter);
        SoundMaster.GetInstance().PlayEffect(onOpenSound, Random.Range(0.8f, 1.25f));
    }
}
