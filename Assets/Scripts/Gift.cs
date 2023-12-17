using UnityEngine;
using UnityEngine.UI;

public class Gift : MonoBehaviour
{
    public int num;

    public Image icon;
    public Image imgX;
    public Image imgCurrent;
    public Text textGrade;
    public Text textName;
    public Text textCount;

    public void Set(int num, int gradeNum, string name, int ticketCount, int picked)
    {
        icon.sprite = Singleton.sm.sprites[num];
        textGrade.text = $"[{Extended.ConvertToRoman(gradeNum + 1)}]";
        textName.text = name;
        textCount.text = ticketCount.ToString();

        imgX.gameObject.SetActive(picked != (int)PickType.NotPicked && picked != (int)PickType.Current);
        imgCurrent.gameObject.SetActive(picked == (int)PickType.Current);
    }
}
