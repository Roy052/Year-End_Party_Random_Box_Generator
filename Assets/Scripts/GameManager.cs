using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton
{
    //Unique GameManager
    public GameObject objConfirmMsg;
    AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;

    void Awake()
    {
        DontDestroyOnLoad(this);
        audioSource = GetComponent<AudioSource>();
        if (gm == null)
            gm = this;
        else
            Destroy(gameObject);
    }

    public ConfirmMsg OpenConfirmMsg()
    {
        GameObject temp = Instantiate(objConfirmMsg);
        return temp.GetComponent<ConfirmMsg>();
    }

    public void AudioON(int num)
    {
        audioSource.clip = audioClips[num];
        audioSource.Play();
    }

    public void ToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }


    public void ToPickGift()
    {
        SceneManager.LoadScene("PickGift");
    }

    public void ToSetTicket()
    {
        SceneManager.LoadScene("SetTicket");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
