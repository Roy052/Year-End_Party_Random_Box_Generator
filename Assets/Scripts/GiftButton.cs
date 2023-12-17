using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftButton : Gift
{
    public SetUpSM setUpSM;
    public void DeleteGift()
    {
        setUpSM.DeleteGift(num);
    }
}
