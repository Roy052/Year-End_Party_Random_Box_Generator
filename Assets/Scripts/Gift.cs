using UnityEngine;
using UnityEngine.UI;

public class Gift : MonoBehaviour
{
    public Image icon;
    public Image imgX;
    public Image imgCurrent;
    public Text textName;
    public Text textCount;

    public void Set(int num, string name, int ticketCount, int picked)
    {
        icon.sprite = Singleton.sm.sprites[num];
        textName.text = name;
        textCount.text = ticketCount.ToString();

        imgX.gameObject.SetActive(picked != (int)PickType.NotPicked && picked != (int)PickType.Current);
        imgCurrent.gameObject.SetActive(picked == (int)PickType.Current);
    }
}
