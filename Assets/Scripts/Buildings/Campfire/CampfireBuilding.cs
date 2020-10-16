using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireBuilding : Building
{
    public override void LevelUp()
    {
        base.LevelUp();
        if (GetLevel() >=1)
            LetterMaster.GetInstance().UnlockLetter(LetterId.tutorial3);
        if(GetLevel() >= 2)
            LetterMaster.GetInstance().UnlockLetter(LetterId.tutorial4);
    }
}
