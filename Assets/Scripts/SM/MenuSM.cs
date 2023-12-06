using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSM : MonoBehaviour
{
    public void OnGo()
    {
        GameManager.instance.ToScene("Go");
    }

    public void OnSetUp()
    {
        GameManager.instance.ToScene("SetUp");
    }

    public void OnQuit()
    {
        GameManager.instance.Quit();
    }
}
