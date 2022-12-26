using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RandomBox : MonoBehaviour
{
    public List<int> spriteNums;
    public List<string> names;
    public List<int> amounts;

    public List<Sprite> sprites;
    private void Start()
    {
        spriteNums = new List<int>();
        names = new List<string>();
        amounts = new List<int>();

        LoadImage();
        LoadGifts();
    }

    void LoadGifts()
    {
        GiftList giftListData = SaveDataScript.LoadFromJson();
        if(giftListData == null)
        {
            SaveGifts();
            Debug.Log("A");
        }
        else
        {
            spriteNums = giftListData.GetSpriteNums();
            names = giftListData.GetNames();
            amounts = giftListData.GetAmounts();
        }
    }

    public void SaveGifts()
    {
        GiftList giftListData = new GiftList(spriteNums,names,amounts);
        SaveDataScript.SaveIntoJson(giftListData);
    }

    public void AddGift(int spriteNum, string name, int amount)
    {
        //이미 있는 선물인가
        int existItemPos = -1;
        for(int i = 0; i < names.Count; i++)
        {
            if(names[i] == name)
            {
                existItemPos = i;
                break;
            }
        }

        if(existItemPos != -1)
        {
            amounts[existItemPos] += amount; 
        }
        else
        {
            spriteNums.Add(spriteNum);
            names.Add(name);
            amounts.Add(amount);
        }
    }

    public void DeleteGift(int num)
    {
        spriteNums.RemoveAt(num);
        names.RemoveAt(num);
        amounts.RemoveAt(num);
    }

    public void Picked(int giftNum)
    {
        amounts[giftNum] -= 1;
    }

    public bool IsItemExist()
    {
        if (names.Count == 0) return false;
        else return true;
    }

    public void LoadImage()
    {
        sprites = new List<Sprite>();
        int count = 0;
        try
        {
            while (count < 100)
            {
                string path = "./Assets/GiftInfo/" + count + ".png";
                //Debug.Log(path);
                if (File.Exists(path))
                {
                    byte[] data = File.ReadAllBytes(path);
                    Texture2D texture = new Texture2D(64, 64);
                    texture.LoadImage(data);
                    texture.name = count.ToString();
                    Sprite sprite = Sprite.Create(texture, 
                        new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                    sprites.Add(sprite);
                }
                else
                {
                    break;
                }
                count++;
            }
        }
        catch (FileNotFoundException e)
        {
            Debug.Log("The file was not found:" + e.Message);
        }
        catch (DirectoryNotFoundException e)
        {
            Debug.Log("The directory was not found: " + e.Message);
        }
        catch (IOException e)
        {
            Debug.Log("The file could not be opened:" + e.Message);
        }
    }
}
