using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI amount;

    public void Load(Item item, bool invalid=false)
    {
        icon.sprite = item?.GetIcon();
        amount.text = ""+Utils.ToFormat(item.GetAmount());
        if (invalid)
            amount.color = Utils.GetWrontColor();
    }


}
