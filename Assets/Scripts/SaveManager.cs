using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public enum PickType
{
    NotPicked = -2,
    Current = -1,
}

public enum GradeType
{
    GradeOne = 0,
    GradeTwo = 1,
    GradeThree = 2,
}

public enum StateType
{
    None        = 0,
    SetTicket   = 1,
    SetGift     = 2,
    PickGift    = 3,
    Gacha       = 4,
}

public class SaveManager : Singleton
{
    public List<Sprite> sprites;

    public SaveData data;

    void Awake()
    {
        DontDestroyOnLoad(this);
        if (sm == null)
            sm = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        LoadImage();
        LoadData();
    }

    void LoadData()
    {
        data = SaveDataScript.LoadFromJson();
        if(data == null)
        {
            Debug.Log("No Data");
            data = new SaveData();
        }
    }

    public void DataReset()
    {
        data = new SaveData();
    }

    public void SaveData()
    {
        SaveDataScript.SaveIntoJson(data);
    }

    public void AddPlayer(string name, int value)
    {
        //이미 있는 유저인가
        int existPlayerPos = data.playerNameList.IndexOf(name);

        if (existPlayerPos != -1)
        {
            data.playerTicketCountList[existPlayerPos] += value;
        }
        else
        {
            data.playerNameList.Add(name);
            data.playerTicketCountList.Add(value);
        }
    }

    public void AddGift(string name, int grade, int value)
    {
        //이미 있는 선물인가
        int existItemPos = data.giftNameList.IndexOf(name);

        if(existItemPos != -1)
        {
            data.giftTicketCountList[existItemPos] += value; 
        }
        else
        {
            data.giftNameList.Add(name);
            data.giftGradeList.Add(grade);
            data.giftPickedList.Add((int)PickType.NotPicked);
            data.giftTicketCountList.Add(value);
        }
    }

    public void DeleteGift(int num)
    {
        data.giftNameList.RemoveAt(num);
        data.giftPickedList.RemoveAt(num);
        data.giftTicketCountList.RemoveAt(num);
        SaveData();
    }

    public void Picked(int giftNum, int playerNum)
    {
        data.giftPickedList[giftNum] = playerNum;
        SaveData();
    }

    public bool IsPicked()
    {
        bool picked = Random.Range(0, data.giftTicketCountList[data.currentGift] + 1) == 0;
        if (picked)
            Picked(data.currentGift, data.currentGachaOrder);
        return picked;
    }

    public void ChangeTicket(int playerNum, int num)
    {
        data.playerTicketCountList[playerNum] += num;
        SaveData();
    }

    public void ChangeGiftTicket(int giftNum, int num)
    {
        data.giftTicketCountList[giftNum] += num;
        SaveData();
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
