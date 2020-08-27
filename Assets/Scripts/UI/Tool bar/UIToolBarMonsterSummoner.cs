using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIToolBarMonsterSummoner : MonoBehaviour
{
    public TextMeshProUGUI summonsAvailable;
    public Color availableColor;
    public AudioClip onNewSummon;

    private int previousAmount = 0;

    public void UpdateAvailableSummons(int number)
    {
        summonsAvailable.text = number.ToString();
        summonsAvailable.color = number > 0 ? availableColor : Color.white;
        GetComponent<Button>().interactable = number > 0;
        if (previousAmount < number)
        {
            SoundMaster.GetInstance().PlayEffect(onNewSummon, 0.5f);
        }
        previousAmount = number;
    }

    public void OpenMonsterPicker()
    {
        UIMonsterPickerMaster.GetInstance().ShowMonsterDraft();
    }
}
