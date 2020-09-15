using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class SaveLetter
{
    public LetterId id;

    public SaveLetter(Letter l)
    {
        id = l.GetId();
    }

    public LetterId GetId() { return id; }
}
