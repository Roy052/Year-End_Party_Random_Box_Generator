using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftButton : MonoBehaviour
{
    public int num;
    public SetUpSM setUpSM;
    public void DeleteGift()
    {
        setUpSM.DeleteGift(num);
    }
}
