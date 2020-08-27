using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILetter : MonoBehaviour
{
    public void Close()
    {
        UILetterMaster.GetInstance().CloseLetter();
    }
}
