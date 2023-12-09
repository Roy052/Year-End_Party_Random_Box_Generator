using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Button btnDelete;
    public Text textName;
    public Text textTicketCount;

    public void Set(string name, int ticketCount)
    {
        textName.text = name;
        textTicketCount.text = ticketCount.ToString();
    }
}
