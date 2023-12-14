using UnityEngine;
using UnityEngine.UI;

public class GoSM : Singleton
{
    public GameObject playerPrefab;
    public GameObject giftPrefab;
    public Text textInfoName;

    public GameObject empty;

    private void Awake()
    {
        if (goSM == null)
            goSM = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        textInfoName.text = sm.data.dataName;
        int count = sm.data.playerNameList.Count;
        for(int i = 0; i < count; i++)
        {
            GameObject temp = Instantiate(playerPrefab, playerPrefab.transform.parent);
            temp.SetActive(true);
            Player tempPlayer = temp.GetComponent<Player>();
            tempPlayer.Set(sm.data.playerNameList[i], sm.data.playerTicketCountList[i]);
        }

        count = sm.data.giftNameList.Count;
        for(int i = 0; i < count; i++)
        {
            GameObject temp = Instantiate(giftPrefab, giftPrefab.transform.parent);
            temp.SetActive(true);
            Gift tempGift = temp.GetComponent<Gift>();
            tempGift.Set(i, sm.data.giftNameList[i], sm.data.giftValueList[i], sm.data.giftPickedList[i]);
        }

        empty.SetActive(string.IsNullOrEmpty(sm.data.dataName));
    }

    private void OnDestroy()
    {
        goSM = null;
    }

    public void OnSetTicket()
    {
        gm.ToScene("SetTicket");
    }

    public void OnPickGift()
    {
        gm.ToScene("PickGift");
    }

    public void OnGetGift()
    {
        gm.ToScene("GetGift");
    }

    public void OnGacha()
    {
        gm.ToScene("Gacha");
    }

    public void OnSetUp()
    {
        gm.ToScene("SetUp");
    }
}
