using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSM : MonoBehaviour
{
    GameManager gm;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void ToPickUp()
    {
        gm.MenuToPick();
    }

    public void ToSetUp()
    {
        gm.MenuToSetting();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
