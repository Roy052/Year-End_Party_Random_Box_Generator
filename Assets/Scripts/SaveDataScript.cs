using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveDataScript
{
    static public void SaveIntoJson(SaveData data)
    {
        string save = JsonUtility.ToJson(data);
        //Debug.Log(save);
        File.WriteAllText("./Assets/GiftInfo/" + "SaveData.json", save);
    }

    static public SaveData LoadFromJson()
    {
        try
        {
            string path = "./Assets/GiftInfo/SaveData.json";
            //Debug.Log(path);
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                //Debug.Log(json);
                SaveData gl = JsonUtility.FromJson<SaveData>(json);
                return gl;
            }
        }
        catch (FileNotFoundException e)
        {
            Debug.Log("The file was not found:" + e.Message);
            return default;
        }
        catch (DirectoryNotFoundException e)
        {
            Debug.Log("The directory was not found: " + e.Message);
            return default;
        }
        catch (IOException e)
        {
            Debug.Log("The file could not be opened:" + e.Message);
            return default;
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
public class SaveData
{
    public string dataName;

    //Player
    public List<string> playerNameList = new List<string>();
    public List<int> playerTicketCountList = new List<int>();

    //Gift
    public List<string> giftNameList = new List<string>();
    public List<int> giftGradeList = new List<int>();
    public List<int> giftPickedList = new List<int>();
    public List<int> giftValueList = new List<int>();

    //Current State
    public byte currentState = (byte)StateType.None;
    public int currentGift = -1;
    public int currentGachaOrder;
}
