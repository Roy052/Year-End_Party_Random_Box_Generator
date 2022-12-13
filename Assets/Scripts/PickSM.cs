using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickSM : MonoBehaviour
{
    GameObject gameManagerObject;
    RandomBox randomBox;

    int itemNum = -1;
    int probabilty = 0;

    [SerializeField] GameObject[] cards;

    [SerializeField] Sprite itemImage;
    [SerializeField] GameObject itemObject;

    //SetUp
    [SerializeField] GameObject hand, deck;
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] Text setupText;
    AudioSource audioSource;
    private void Start()
    {
        for (int i = 0; i < 3; i++)
            cards[i].SetActive(false);

        gameManagerObject = GameObject.Find("GameManager");
        randomBox = gameManagerObject.GetComponent<RandomBox>();
        audioSource = this.GetComponent<AudioSource>();
        if (randomBox.IsItemExist())
            StartCoroutine(SetUp());
        else
            StartCoroutine(NotSetUp());
    }
    public void Pick(int num)
    {
        itemNum = SelectedItem();

        randomBox.Picked(itemNum);
        for (int i = 0; i < 3; i++)
            if (i != num) cards[i].SetActive(false);
        StartCoroutine(ItemObjectSpawn(num));
        randomBox.SaveGifts();
    }

    int SelectedItem()
    {
        int itemAmount = randomBox.names.Count;
        int tempItemNum = 0;

        //Reset
        probabilty = 0;
        for (int i = 0; i < itemAmount; i++)
            probabilty += randomBox.amounts[i];

        Debug.Log(itemAmount + ", " + probabilty);
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
        Debug.Log(temp + ", " + tempItemNum);

        return tempItemNum;
    }

    IEnumerator ItemObjectSpawn(int num)
    {
        itemObject.transform.position = cards[num].transform.position;
        yield return new WaitForSeconds(1.5f);
        itemObject.GetComponent<SpriteRenderer>().sprite = itemImage;
        StartCoroutine(FadeManager.FadeIn(itemObject.GetComponent<SpriteRenderer>(), 1));

    }

    //선물이 있을 때 세팅 애니메이션
    IEnumerator SetUp()
    {
        Vector3 tempHandVector = hand.transform.position;
        while(hand.transform.position.y > 1.14f)
        {
            tempHandVector.y -= 10 * Time.deltaTime;
            hand.transform.position = tempHandVector;
            yield return new WaitForEndOfFrame();
        }

        Vector3 tempDeckVector = deck.transform.position;
        while(hand.transform.position.y < 11)
        {
            tempHandVector.y += 10 * Time.deltaTime;
            tempDeckVector.y += 10 * Time.deltaTime;
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
        Vector3 tempHandVector = hand.transform.position;
        while (hand.transform.position.y > 1.14f)
        {
            tempHandVector.y -= 10 * Time.deltaTime;
            hand.transform.position = tempHandVector;
            yield return new WaitForEndOfFrame();
        }

        Vector3 tempDeckVector = deck.transform.position;
        while (hand.transform.position.y < 11)
        {
            tempHandVector.y += 10 * Time.deltaTime;
            tempDeckVector.y += 10 * Time.deltaTime;
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
                tempHandVector.y -= 10 * Time.deltaTime;
                hand.transform.position = tempHandVector;
                yield return new WaitForEndOfFrame();
            }
            SoundON("put");
            cards[i].SetActive(true);
            while (hand.transform.position.y < 11)
            {
                tempHandVector.y += 10 * Time.deltaTime;
                hand.transform.position = tempHandVector;
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void ResetCard()
    {
        for (int i = 0; i < 3; i++)
        {
            cards[i].SetActive(false);
            cards[i].GetComponent<Card>().ResetCard();
        }
        itemObject.GetComponent<SpriteRenderer>().sprite = null;
            
        StartCoroutine(PutCard());
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
}

