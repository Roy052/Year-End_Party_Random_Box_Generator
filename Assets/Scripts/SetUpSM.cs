using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetUpSM : MonoBehaviour
{
    GameObject itemObject;
    [SerializeField] GameObject giftTemplate;
    [SerializeField] Transform giftTransform;
    [SerializeField] Text tableHead;
    [SerializeField] Sprite[] giftSprites;
    RandomBox randomBox;
    GameManager gm;
    public List<GameObject> giftButtons;

    //InputDatas
    [SerializeField] Dropdown spriteNumDropDown;
    [SerializeField] InputField[] inputFields;
    [SerializeField] Text warningText;
    [SerializeField] SpriteRenderer giftImage;
    int tempNum, tempSpriteNum;
    string tempName, tempAmount;

    //Audio
    [SerializeField] AudioClip[] audioClips;
    AudioSource audioSource;
    private void Start()
    {
        giftButtons = new List<GameObject>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        randomBox = GameObject.Find("GameManager").GetComponent<RandomBox>();
        audioSource = this.GetComponent<AudioSource>();

        for (int i = 0; i < randomBox.names.Count; i++)
            CreateGiftBoard(i, randomBox.spriteNums[i], randomBox.names[i], randomBox.amounts[i]);

        spriteNumDropDown.options.Clear();
        for (int i = 0; i < randomBox.sprites.Count; i++)
        {
            spriteNumDropDown.options.Add(new Dropdown.OptionData() { text = i.ToString() });
        }
        spriteNumDropDown.onValueChanged.AddListener(delegate { DropdownItemSelected(spriteNumDropDown); });
            
        giftImage.sprite = randomBox.sprites[0];
    }

    void DropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;
        giftImage.sprite = randomBox.sprites[index];
    }

    public void CreateGift()
    {
        tempNum = giftButtons.Count;
        tempSpriteNum = spriteNumDropDown.value;
        tempName = inputFields[0].text;
        tempAmount = inputFields[1].text;
        

        if(tempName == "")
        {
            warningText.text = "이름을 입력하세요";
            StartCoroutine(FadeManager.FadeOut(warningText, 2));
        }
        else if(tempAmount == "")
        {
            warningText.text = "수량을 입력하세요";
            StartCoroutine(FadeManager.FadeOut(warningText, 2));
        }
        else
        {
            try
            {
                CreateGift(tempNum, tempSpriteNum,tempName, int.Parse(tempAmount));
                AudioON("add");
            }
            catch
            {
                warningText.text = "수량은 숫자로 입력하세요";
            }
        }
    }

    public void CreateGift(int num, int spriteNum, string giftName,int giftAmount)
    {
        bool itemExist = false;
        int itemPos = -1;
        for(int i = 0; i < giftButtons.Count; i++)
        {
            if (randomBox.names[i] == giftName)
            {
                itemExist = true;
                itemPos = i;
                break;
            }
        }

        randomBox.AddGift(spriteNum ,giftName, giftAmount);

        if (itemExist)
        {
            giftButtons[itemPos].transform.GetChild(1).GetComponent<Text>().text = randomBox.amounts[itemPos] + "개";
        }
        else
        {
            CreateGiftBoard(num, spriteNum, giftName, giftAmount);
        }
    }

    public void CreateGiftBoard(int num, int spriteNum, string giftName, int giftAmount)
    {
        GameObject temp = Instantiate(giftTemplate, giftTransform);
        temp.transform.GetChild(0).GetComponent<Text>().text = giftName;
        temp.transform.GetChild(1).GetComponent<Text>().text = giftAmount + "개";
        temp.GetComponent<GiftButton>().num = num;
        temp.GetComponent<GiftButton>().setUpSM = this;
        giftButtons.Add(temp);
        AudioON("add");
    }

    public void DeleteGift(int num)
    {
        randomBox.DeleteGift(num);
        Destroy(giftButtons[num]);
        giftButtons.RemoveAt(num);

        for (int i = num; i < giftButtons.Count; i++)
        {
            giftButtons[i].GetComponent<GiftButton>().num -= 1;
        }
    }

    public void SaveGifts()
    {
        AudioON("save");
        randomBox.SaveGifts();
    }

    public void ResetGifts()
    {
        AudioON("reset");
        while (giftButtons.Count != 0)
            DeleteGift(0);
        
        //SaveDataScript.DeleteSave();
    }

    public void ReloadImage()
    {
        randomBox.LoadImage();

        spriteNumDropDown.options.Clear();
        for (int i = 0; i < randomBox.sprites.Count; i++)
        {
            spriteNumDropDown.options.Add(new Dropdown.OptionData() { text = i.ToString() });
        }

        giftImage.sprite = randomBox.sprites[0];
    }

    public void ToMenu()
    {
        gm.ToMenu();
    }

    void AudioON(string clipName)
    {
        if(clipName == "add")
        {
            audioSource.clip = audioClips[0];
            audioSource.Play();
        }
        else if (clipName == "reset")
        {
            audioSource.clip = audioClips[1];
            audioSource.Play();
        }
        else if (clipName == "save")
        {
            audioSource.clip = audioClips[2];
            audioSource.Play();
        }
    }
}
