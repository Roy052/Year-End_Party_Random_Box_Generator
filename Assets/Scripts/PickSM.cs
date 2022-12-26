using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickSM : MonoBehaviour
{
    GameObject gameManagerObject;
    GameManager gm;
    RandomBox randomBox;

    int itemNum = -1;
    int pickedNum;
    int probabilty = 0;
    public int animationSpeed;

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

    private void Start()
    {
        for (int i = 0; i < 3; i++)
            cards[i].SetActive(false);

        gameManagerObject = GameObject.Find("GameManager");
        randomBox = gameManagerObject.GetComponent<RandomBox>();
        gm = gameManagerObject.GetComponent<GameManager>();
        audioSource = this.GetComponent<AudioSource>();

        if (randomBox.IsItemExist())
            StartCoroutine(SetUp());
        else
            StartCoroutine(NotSetUp());
    }
    public void Pick(int num)
    {
        pickedNum = num;
        itemNum = SelectedItem();
        if (itemNum == -1)
        {
            StartCoroutine(NotSetUp());
            for (int i = 0; i < 3; i++)
                cards[i].SetActive(false);
        }
        else
        {
            randomBox.Picked(itemNum);
            for (int i = 0; i < 3; i++)
                if (i != num) cards[i].SetActive(false);
            StartCoroutine(ItemObjectSpawn(num));
            randomBox.SaveGifts();
            repickBtn.SetActive(true);
            repickText.SetActive(true);
        }
    }

    int SelectedItem()
    {
        int itemAmount = randomBox.names.Count;
        int tempItemNum = -1;

        //Reset
        probabilty = 0;
        for (int i = 0; i < itemAmount; i++)
            probabilty += randomBox.amounts[i];

        if (probabilty <= 0)
            return tempItemNum;

        //Debug.Log(itemAmount + ", " + probabilty);
        int temp = Random.Range(0, probabilty + 1);
        for (int i = 0; i < itemAmount; i++)
        {
            temp -= randomBox.amounts[i];
            if (temp <= 0)
            {
                tempItemNum = i;
                break;
            }
        }
        //Debug.Log(temp + ", " + tempItemNum);

        return tempItemNum;
    }

    IEnumerator ItemObjectSpawn(int num)
    {
        itemObject.transform.position = cards[num].transform.position;
        yield return new WaitForSeconds(1.5f);
        itemObject.GetComponent<SpriteRenderer>().sprite = randomBox.sprites[randomBox.spriteNums[itemNum]];
        StartCoroutine(FadeManager.FadeIn(itemObject.GetComponent<SpriteRenderer>(), 1));
        itemTexts[num].text = randomBox.names[itemNum];
        StartCoroutine(FadeManager.FadeIn(itemTexts[itemNum], 1.5f));
    }

    //선물이 있을 때 세팅 애니메이션
    IEnumerator SetUp()
    {
        repickBtn.SetActive(false);
        repickText.SetActive(false);

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
        setupText.text = "남아있는 선물이 없어요!";
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
        if(SelectedItem() == -1)
        {
            StartCoroutine(NotSetUp());
            for (int i = 0; i < 3; i++)
            {
                cards[i].SetActive(false);
                cards[i].GetComponent<Card>().ResetCard();
                itemTexts[i].text = "";
            }
            itemObject.GetComponent<SpriteRenderer>().sprite = null;

            
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                cards[i].SetActive(false);
                cards[i].GetComponent<Card>().ResetCard();
                itemTexts[i].text = "";
            }
            itemObject.GetComponent<SpriteRenderer>().sprite = null;

            StartCoroutine(PutCard());
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

    public void ToMenu()
    {
        gm.ToMenu();
    }
}

