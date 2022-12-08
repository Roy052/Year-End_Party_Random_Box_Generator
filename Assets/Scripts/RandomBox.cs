using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBox : MonoBehaviour
{
    public List<string> names;
    public List<int> probabilities;
    public List<bool> itemPicked;

    private void Start()
    {
        names = new List<string>();
        probabilities = new List<int>();
        itemPicked = new List<bool>();

        ItemList itemList = new ItemList();

        for(int i = 0; i < itemList.itemNames.Length; i++)
        {
            AddItem(itemList.itemNames[i], itemList.itemProbabilities[i]);
        }
    }

    public void AddItem(string name, int probability)
    {
        names.Add(name);
        probabilities.Add(probability);
        itemPicked.Add(false);
    }

    public void Picked(int giftNum)
    {
        itemPicked[giftNum] = true;
    }
}
