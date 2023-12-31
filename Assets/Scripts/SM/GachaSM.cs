using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaSM : Singleton
{
    public enum GachaType
    {
        Normal = 0,
        Last = 1,
    }

    public int animationSpeed;

    int currentPlayerOrder = 0;
    int pickedNum = 1;

    [SerializeField] GameObject[] cards;

    [SerializeField] Sprite itemImage;
    [SerializeField] GameObject itemObject;
    [SerializeField] Text[] itemTexts;

    //SetUp
    [SerializeField] GameObject hand, deck;
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] Text setupText;
    AudioSource audioSource;

    //Repick
    [SerializeField] GameObject repickBtn, repickText;
    public GameObject bottomBtns;

    //Top UI
    public Sprite spriteNotPicked;
    public Image currentGift;
    public GameObject playerPrefab;
    public GameObject objLast;

    List<PlayerWithToggle> playerList = new List<PlayerWithToggle>();
    List<int> currentGiftNumList = new List<int>();
    List<int> currengPlayerNumList = new List<int>();
    List<int> ticketValueByPlayer = new List<int>();

    GachaType gachaType = GachaType.Last;

    private void Start()
    {
        for (int i = 0; i < 3; i++)
            cards[i].SetActive(false);

        audioSource = GetComponent<AudioSource>();

        gachaType = sm.data.isLast ? GachaType.Last : GachaType.Normal;
        Debug.Log(gachaType + ", " + sm.data.isLast);

        for (int i = 0; i < sm.data.giftNameList.Count; i++)
            if (sm.data.giftPickedList[i] == (int)PickType.Current)
                currentGiftNumList.Add(i);

        if (currentGiftNumList.Count == 0)
            StartCoroutine(NotSetUp());
        else
            StartCoroutine(SetUp());
    }

    public void Pick(int num)
    {
        pickedNum = num;
        if(gachaType == GachaType.Normal)
        {
            sm.data.currentGift = currentGiftNumList[0];
            if (sm.IsPicked() == false)
            {
                StartCoroutine(NotSetUp());
                for (int i = 0; i < 3; i++)
                    cards[i].SetActive(false);
            }
            else
            {
                for (int i = 0; i < 3; i++)
                    if (i != num) cards[i].SetActive(false);

                bool isGiftPicked = IsGiftPicked();
                StartCoroutine(ItemObjectSpawn(num, isGiftPicked));
                if (isGiftPicked)
                    sm.Picked(currentGiftNumList[0], currengPlayerNumList[currentPlayerOrder]);
                else
                    sm.ChangeGiftTicket(currentGiftNumList[0], -1);
                currentGiftNumList.RemoveAt(0);

                repickBtn.SetActive(true);
                repickText.SetActive(true);
                bottomBtns.SetActive(true);
            }
        }
        else
        {
            int pickedGiftOrder = Random.Range(0, currentGiftNumList.Count);
            sm.data.currentGift = currentGiftNumList[pickedGiftOrder];
            for (int i = 0; i < 3; i++)
                    if (i != num) cards[i].SetActive(false);

            StartCoroutine(ItemObjectSpawn(num, true));
            sm.Picked(sm.data.currentGift, currengPlayerNumList[currentPlayerOrder]);
            currentGiftNumList.RemoveAt(pickedGiftOrder);

            repickBtn.SetActive(true);
            repickText.SetActive(true);
            bottomBtns.SetActive(true);
        }
    }

    bool IsGiftPicked()
    {
        int probabilty = sm.data.giftTicketCountList[sm.data.currentGift];
        if (probabilty == 0) return false;
        return Random.Range(0, probabilty + 1) == 0;
    }

    IEnumerator ItemObjectSpawn(int num, bool isGiftPicked)
    {
        itemObject.transform.position = cards[num].transform.position;
        yield return new WaitForSeconds(1.5f);
        itemObject.GetComponent<SpriteRenderer>().sprite = isGiftPicked ? sm.sprites[sm.data.currentGift] : spriteNotPicked;
        StartCoroutine(FadeManager.FadeIn(itemObject.GetComponent<SpriteRenderer>(), 1));
        itemTexts[num].text = sm.data.giftNameList[sm.data.currentGift];
        StartCoroutine(FadeManager.FadeIn(itemTexts[num], 1.5f));
    }

    //선물이 있을 때 세팅 애니메이션
    IEnumerator SetUp()
    {
        repickBtn.SetActive(false);
        repickText.SetActive(false);
        bottomBtns.SetActive(false);
        objLast.SetActive(gachaType == GachaType.Last);

        SetPlayer();

        yield return new WaitForSeconds(0.5f);
        Vector3 tempHandVector = hand.transform.position;
        while(hand.transform.position.y > 1.14f)
        {
            tempHandVector.y -= animationSpeed * Time.deltaTime;
            hand.transform.position = tempHandVector;
            yield return new WaitForEndOfFrame();
        }

        Vector3 tempDeckVector = deck.transform.position;
        while(hand.transform.position.y < 11)
        {
            tempHandVector.y += animationSpeed * Time.deltaTime;
            tempDeckVector.y += animationSpeed * Time.deltaTime;
            hand.transform.position = tempHandVector;
            deck.transform.position = tempDeckVector;
            yield return new WaitForEndOfFrame();
        }
        SoundON("shuffle");
        yield return new WaitForSeconds(2.5f);
        StartCoroutine(PutCard());
    }

    //선물이 없을 때 세팅 애니메이션
    IEnumerator NotSetUp()
    {
        repickBtn.SetActive(false);
        repickText.SetActive(false);
        bottomBtns.SetActive(true);

        Vector3 tempHandVector = hand.transform.position;
        tempHandVector.x = cards[pickedNum].transform.position.x;
        while (hand.transform.position.y > 1.14f)
        {
            tempHandVector.y -= animationSpeed * Time.deltaTime;
            hand.transform.position = tempHandVector;
            yield return new WaitForEndOfFrame();
        }

        Vector3 tempDeckVector = deck.transform.position;
        while (hand.transform.position.y < 11)
        {
            tempHandVector.y += animationSpeed * Time.deltaTime;
            tempDeckVector.y += animationSpeed * Time.deltaTime;
            hand.transform.position = tempHandVector;
            deck.transform.position = tempDeckVector;
            yield return new WaitForEndOfFrame();
        }
        setupText.text = "현재 선택된 선물이 없어요!";
        StartCoroutine(FadeManager.FadeIn(setupText, 1));
    }

    //카드 놓는 애니메이션
    IEnumerator PutCard()
    {
        Vector3 tempHandVector = hand.transform.position;
        for(int i = 0; i < 3; i++)
        {
            tempHandVector.x = cards[i].transform.position.x;
            while (hand.transform.position.y > 1.14f)
            {
                tempHandVector.y -= animationSpeed * Time.deltaTime;
                hand.transform.position = tempHandVector;
                yield return new WaitForEndOfFrame();
            }
            SoundON("put");
            cards[i].SetActive(true);
            while (hand.transform.position.y < 11)
            {
                tempHandVector.y += animationSpeed * Time.deltaTime;
                hand.transform.position = tempHandVector;
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void ResetCard()
    {
        repickBtn.SetActive(false);
        repickText.SetActive(false);
        bottomBtns.SetActive(false);

        for (int i = 0; i < 3; i++)
        {
            cards[i].SetActive(false);
            cards[i].GetComponent<Card>().ResetCard();
            itemTexts[i].text = "";
        }
        itemObject.GetComponent<SpriteRenderer>().sprite = null;

        if (currentGiftNumList.Count == 0)
            StartCoroutine(NotSetUp());
        else
            StartCoroutine(PutCard());

        if (playerList.Count > 0)
        {
            Destroy(playerList[0].gameObject);
            playerList.RemoveAt(0);
        }
    }

    public void SoundON(string name)
    {
        if(name == "shuffle")
        {
            audioSource.clip = audioClips[0];
            audioSource.Play();
        }
        else if(name == "put")
        {
            audioSource.clip = audioClips[1];
            audioSource.Play();
        }
        else if(name == "flip")
        {
            audioSource.clip = audioClips[2];
            audioSource.Play();
        }
    }

    public void SetPlayer()
    {
        for(int i = 0; i < sm.data.playerNameList.Count; i++)
        {
            if(ticketValueByPlayer.Count <= i)
                ticketValueByPlayer.Add(0);
            if (sm.data.playerTicketCountList[i] > 0)
            {
                GameObject temp = Instantiate(playerPrefab, playerPrefab.transform.parent);
                temp.SetActive(true);
                PlayerWithToggle tempPlayer = temp.GetComponent<PlayerWithToggle>();
                tempPlayer.Set(sm.data.playerNameList[i], sm.data.playerTicketCountList[i]);
                playerList.Add(tempPlayer);
            }
        }

        for(int i = 0; i < sm.data.giftTicketCountList.Count; i++)
        {
            if (sm.data.giftPickedList[i] >= 0)
                ticketValueByPlayer[sm.data.giftPickedList[i]] += sm.data.giftTicketCountList[i];
        }

        currentPlayerOrder = 0;
        //currentPlayerOrder = Mathf.Min(sm.data.currentGachaOrder, playerList.Count - 1);
    }

    public void OnBack()
    {
        gm.ToScene("Go");
    }

    public void OnHome()
    {
        gm.ToScene("Menu");
    }
}

