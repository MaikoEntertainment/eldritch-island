using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToolBarSave : MonoBehaviour
{
    public void Save()
    {
        SavingMaster.GetInstace().Save();
    }
}
