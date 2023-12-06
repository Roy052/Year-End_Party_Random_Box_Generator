using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Unique GameManager
    public static GameManager instance;
    AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;

    void Awake()
    {
        DontDestroyOnLoad(this);
        audioSource = GetComponent<AudioSource>();
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
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
