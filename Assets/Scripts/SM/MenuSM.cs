using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSM : Singleton
{
    public void OnGo()
    {
        gm.ToScene("Go");
    }

    public void OnSetUp()
    {
        gm.ToScene("SetUp");
    }

    public void OnQuit()
    {
        gm.Quit();
    }
}
