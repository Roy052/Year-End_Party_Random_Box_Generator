using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWithButton : Player
{
    public InputField inputTicketCount;

    int playerNum;
    int ticketCount;

    public void Set(int playerNum, string name, int ticketCount)
    {
        this.playerNum = playerNum;
        this.ticketCount = ticketCount;
        textName.text = name;
        inputTicketCount.text = ticketCount.ToString();
    }

    public void OnChangeTicket(int ticketCount)
    {
        int count = this.ticketCount + ticketCount;
        Singleton.sm.ChangeTicket(playerNum, ticketCount);
        this.ticketCount = count;
        inputTicketCount.text = count.ToString();
    }

    public void OnChangeTicket(string ticketCountStr)
    {
        try
        {
            int count = int.Parse(ticketCountStr);
            Singleton.sm.ChangeTicket(playerNum, count - Singleton.sm.data.playerTicketCountList[playerNum]);
            this.ticketCount = count;
            inputTicketCount.text = count.ToString();
        }
        catch
        {
            Debug.LogError("Put Int");
        }
    }
}
