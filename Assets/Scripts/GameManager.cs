using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Unique GameManager
    private static GameManager gameManagerInstance;
    AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;

    void Awake()
    {
        DontDestroyOnLoad(this);
        audioSource = this.GetComponent<AudioSource>();
        if (gameManagerInstance == null)
        {
            gameManagerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AudioON(int num)
    {
        audioSource.clip = audioClips[num];
        audioSource.Play();
    }

    public void MenuToPick()
    {
        SceneManager.LoadScene("Pick");
    }
    public void MenuToSetting()
    {
        SceneManager.LoadScene("Setting");
    }
    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void QuitThis()
    {
        Application.Quit();
    }
}
