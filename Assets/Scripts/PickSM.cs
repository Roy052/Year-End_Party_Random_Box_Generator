using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    AudioSource audioSource;
    private void Start()
    {
        for (int i = 0; i < 3; i++)
            cards[i].SetActive(false);

        gameManagerObject = GameObject.Find("GameManager");
        randomBox = gameManagerObject.GetComponent<RandomBox>();
        audioSource = this.GetComponent<AudioSource>();
        StartCoroutine(SetUp());
    }
    public void Pick(int num)
    {
        itemNum = SelectedItem();

        randomBox.Picked(itemNum);
        for (int i = 0; i < 3; i++)
            if (i != num) cards[i].SetActive(false);
        StartCoroutine(ItemObjectSpawn(num));
    }

    int SelectedItem()
    {
        int itemAmount = 0;
        int tempItemNum = 0;
        for (int i = 0; i < randomBox.itemPicked.Count; i++)
        {
            if (randomBox.itemPicked[i] == false)
            {
                itemAmount++;
                probabilty += randomBox.probabilities[i];
            }
        }
        Debug.Log(itemAmount + ", " + probabilty);
        int temp = Random.Range(0, probabilty + 1);
        for (int i = 0; i < randomBox.probabilities.Count; i++)
        {
            if (randomBox.itemPicked[i] == false)
            {
                temp -= randomBox.probabilities[i];
                if (temp <= 0)
                {
                    tempItemNum = i;
                    break;
                }
            }
        }
        Debug.Log(tempItemNum);

        return tempItemNum;
    }

    IEnumerator ItemObjectSpawn(int num)
    {
        itemObject.transform.position = cards[num].transform.position;
        yield return new WaitForSeconds(1.5f);
        itemObject.GetComponent<SpriteRenderer>().sprite = itemImage;
        StartCoroutine(FadeManager.FadeIn(itemObject.GetComponent<SpriteRenderer>(), 1));

    }

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
        yield return new WaitForSeconds(2);
        StartCoroutine(PutCard());
    }

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
            yield return new WaitForSeconds(0.2f);
            cards[i].SetActive(true);
            while (hand.transform.position.y < 11)
            {
                tempHandVector.y += 10 * Time.deltaTime;
                hand.transform.position = tempHandVector;
                yield return new WaitForEndOfFrame();
            }
        }
    }

    public void ResetCard()
    {
        for (int i = 0; i < 3; i++)
            cards[i].SetActive(false);
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

