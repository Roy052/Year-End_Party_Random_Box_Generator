using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveDataScript : MonoBehaviour
{
    static public void SaveIntoJson(GiftList giftListData)
    {
        GiftList saveData = giftListData;
        string save = JsonUtility.ToJson(saveData);
        File.WriteAllText("./Assets/GiftInfo/" + "SaveData.json", save);
    }

    static public GiftList LoadFromJson()
    {
        try
        {
            string path = "./Assets/GiftInfo/SaveData.json";
            Debug.Log(path);
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                Debug.Log(json);
                GiftList gl = JsonUtility.FromJson<GiftList>(json);
                return gl;
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
        return default;
    }

    static public void DeleteSave()
    {
        try
        {
            string path = "./Assets/Save/SaveData.json";
            Debug.Log(path);
            if (File.Exists(path))
            {
                File.Delete(path);
                SaveDataScript.RefreshEditor();
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
    static public void RefreshEditor()
    {
        #if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
        #endif
    }
}



[System.Serializable]
public class Gift
{
    string name;
    int amount;

    public Gift(string name, int amount)
    {
        this.name = name;
        this.amount = amount;
    }
    
    public string GetName()
    {
        return name;
    }

    public int GetAmount()
    {
        return amount;
    }
}
public class GiftList
{
    List<Gift> giftList;

    public GiftList()
    {
        giftList = new List<Gift>();
    }

    public GiftList(GiftList copy)
    {
        giftList = copy.GetGiftList();
    }

    public GiftList(List<Gift> giftList)
    {
        this.giftList = giftList;
    }

    public List<Gift> GetGiftList()
    {
        return giftList;
    }
}
