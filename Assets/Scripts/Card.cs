using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] PickSM pickSM;
    [SerializeField] int num;
    [SerializeField] Sprite front, back;

    private void OnMouseDown()
    {
        pickSM.Pick(num);
        this.GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(Flip());
    }

    IEnumerator Flip()
    {
        Quaternion tempQuat;
        tempQuat = Quaternion.Euler(0, 0, 0);
        float tempTime = 0;

        pickSM.SoundON("flip");
        while(tempTime < 0.25f)
        {
            tempQuat.eulerAngles += new Vector3(0, 90, 0) * 4 * Time.deltaTime;
            transform.localRotation = tempQuat;
            tempTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        this.GetComponent<SpriteRenderer>().sprite = front;

        tempTime = 0;
        while (tempTime < 0.25f)
        {
            tempQuat.eulerAngles -= new Vector3(0, 90, 0) * 4 * Time.deltaTime;
            transform.localRotation = tempQuat;
            tempTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    public void ResetCard()
    {
        this.GetComponent<SpriteRenderer>().sprite = back;
        this.GetComponent<BoxCollider2D>().enabled = true;
    }
}
