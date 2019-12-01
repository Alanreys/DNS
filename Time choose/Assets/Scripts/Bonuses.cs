using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bonuses : MonoBehaviour
{
    public int points;
    public GameObject[] lifes = new GameObject[12];

    void Start()
    {
        points = 3;
    }

    public void Refresh ()
    {
        if (points == 0)
        {
            lifes[0].SetActive(false);
            Invoke("Load_2", 1);
        }

        for (int i = 0; i < points; i++)
        {
            lifes[i].SetActive(true);
            if (i != points - 1) continue;
            else
            {
                for (int k = 11; k > i; k--)
                {
                    lifes[k].SetActive(false);
                }
            }
        }
    }

    void Load_2 ()
    {
        SceneManager.LoadScene("2");
    }
}
