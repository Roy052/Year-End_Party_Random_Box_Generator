using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveDataScript : MonoBehaviour
{
    static public void SaveIntoJson(GiftList giftListData)
    {
        GiftList saveData = new GiftList(giftListData);
        string save = JsonUtility.ToJson(saveData);
        //Debug.Log(save);
        File.WriteAllText("./Assets/GiftInfo/" + "SaveData.json", save);
    }

    static public GiftList LoadFromJson()
    {
        try
        {
            string path = "./Assets/GiftInfo/SaveData.json";
            //Debug.Log(path);
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                //Debug.Log(json);
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
            //Debug.Log(path);
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
public class GiftList
{
    public List<int> spriteNums;
    public List<string> names;
    public List<int> amounts;

    public GiftList()
    {
        spriteNums = new List<int>();
        names = new List<string>();
        amounts = new List<int>();
    }

    public GiftList(List<int> copySpriteNums, List<string> copyNames, List<int> copyAmounts)
    {
        spriteNums = copySpriteNums;
        names = copyNames;
        amounts = copyAmounts;
    }

    public GiftList(GiftList giftList)
    {
        this.spriteNums = giftList.GetSpriteNums();
        this.names = giftList.GetNames();
        this.amounts = giftList.GetAmounts();
    }

    public List<int> GetSpriteNums()
    {
        return spriteNums;
    }

    public List<string> GetNames()
    {
        return names;
    }

    public List<int> GetAmounts()
    {
        return amounts;
    }
}
