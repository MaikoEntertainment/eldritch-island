using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireBuilding : Building
{
    public override void LevelUp()
    {
        base.LevelUp();
        switch (GetLevel())
        {
            case 1:
                LetterMaster.GetInstance().UnlockLetter(LetterId.tutorial3);
                break;
            case 2:
                LetterMaster.GetInstance().UnlockLetter(LetterId.tutorial4);
                break;
        }
    }
}
