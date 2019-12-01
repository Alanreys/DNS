using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public GameObject Menu;
    public GameObject SoundOff;
    public GameObject Rules;
    public GameObject Levels;
    public GameObject Play_on, Rules_on, Hard_on, Easy_on;
    public GameObject Home;
    public GameObject replay;

    private void Start()
    {
        if (gameObject.name == "M_sound_on")
        {
            if (PlayerPrefs.GetString("Sound") == "no")
            {
                SoundOff.SetActive(true);
                GameObject.Find("AudioClick").GetComponent<AudioSource>().mute = true;
            }
        }
    }

    private void OnMouseDown()
    {
        switch (gameObject.name)
        {
            case "M_play_s":
                Play_on.SetActive(true);
                break;

            case "M_rules_s":
                Rules_on.SetActive(true);
                break;

            case "Easy_static":
                Easy_on.SetActive(true);
                break;

            case "Hard_static":
                Hard_on.SetActive(true);
                break;

            case "Home":
                transform.localScale = new Vector2(1.25f, 1.25f);
                break;

            case "Game_home":
                transform.localScale = new Vector2(1.25f, 1.25f);
                break;

            case "End game":
                replay.SetActive(true);
                break;

            case "Home_1":
                transform.localScale = new Vector2(1.25f, 1.25f);
                break;
        }
    }

    private void OnMouseUp()
    {
        switch (gameObject.name)
        {
            case "M_play_s":
                Play_on.SetActive(false);
                break;

            case "M_rules_s":
                Rules_on.SetActive(false);
                break;

            case "Easy_static":
                Easy_on.SetActive(false);
                break;

            case "Hard_static":
                Hard_on.SetActive(false);
                break;

            case "Game_home":
                transform.localScale = new Vector2(1, 1);
                break;

            case "End game":
                replay.SetActive(false);
                break;

            case "Home_1":
                transform.localScale = new Vector2(1, 1);
                break;
        }
    }

    private void OnMouseUpAsButton()
    {
        if (PlayerPrefs.GetString("Sound") != "no")
            GameObject.Find("AudioClick").GetComponent<AudioSource>().Play();

        switch (gameObject.name)
        {
            case "M_sound_on":
                if (PlayerPrefs.GetString("Sound") != "no")
                {
                    PlayerPrefs.SetString("Sound", "no");
                    SoundOff.SetActive(true);
                    GameObject.Find("AudioClick").GetComponent<AudioSource>().mute = true;
                }
                else
                {
                    PlayerPrefs.SetString("Sound", "yes");
                    SoundOff.SetActive(false);
                    GameObject.Find("AudioClick").GetComponent<AudioSource>().mute = false;
                }
                break;

            case "M_play_s":
                Levels.SetActive(true);
                Menu.SetActive(false);
                Home.GetComponent<Transform>().localScale = new Vector2(1, 1);
                break;

            case "M_rules_s":
                Rules.SetActive(true);
                Menu.SetActive(false);
                break;

            case "M_Exit":
                Application.Quit();
                break;

            case "Home_r":
                Menu.SetActive(true);
                Rules_on.SetActive(false);
                Rules.SetActive(false);
                break;

            case "Home":
                Levels.SetActive(false);
                Menu.SetActive(true);
                Play_on.SetActive(false);
                break;

            case "Easy_static":
                PlayerPrefs.SetInt("level", 1);
                Invoke("SceneLoad", 0.3f);
                break;

            case "Hard_static":
                PlayerPrefs.SetInt("level", 2);
                Invoke("SceneLoad", 0.3f);
                break;

            case "Game_home":
                Invoke("Exit_menu", 0.3f);
                break;

            case "End game":
                Invoke("SceneLoad", 0.3f);
                break;

            case "Home_1":
                Invoke("Exit_menu", 0.3f);
                break;
        }
    }


    void SceneLoad()
    {
        SceneManager.LoadScene("1");
    }

    void Exit_menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
