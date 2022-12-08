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
    private void Start()
    {
        gameManagerObject = GameObject.Find("GameManager");
        randomBox = gameManagerObject.GetComponent<RandomBox>();

        int itemAmount = 0;
        for(int i = 0; i < randomBox.itemPicked.Count; i++)
        {
            if (randomBox.itemPicked[i] == false)
            {
                itemAmount++;
                probabilty += randomBox.probabilities[i];
            }
        }
        Debug.Log(itemAmount + ", " + probabilty);
        int temp = Random.Range(0, probabilty + 1);
        for(int i = 0; i < randomBox.probabilities.Count; i++)
        {
            if(randomBox.itemPicked[i] == false)
            {
                temp -= randomBox.probabilities[i];
                if(temp <= 0)
                {
                    itemNum = i;
                    break;
                }
            }
        }
        Debug.Log(itemNum);
    }
    public void Pick(int num)
    {
        randomBox.Picked(itemNum);
        for (int i = 0; i < 3; i++)
            if (i != num) cards[i].SetActive(false);
    }
}

