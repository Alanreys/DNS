using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public GameObject minute;
    public GameObject hour;
    public float velocity;
    public AudioSource sounds_game;
    public AudioClip right_clock, wrong_clock, miss_clock;

    private float angle_minute;
    private float angle_hour;
    private int spawn_time;
    [SerializeField]
    private bool true_time;
    private bool done;

    public GameObject broke1;
    public GameObject broke2;
    public GameObject broke3;
    public GameObject broke4;

    private bool true_broke = false;

    private void Start()
    {
        done = false;
        velocity = GameObject.Find("Main Camera").GetComponent<GameController>().velosity;
        spawn_time = Random.Range(1, 4);
        if (spawn_time == 3)
        {
            true_time = true;
            Angle(GameObject.Find("Main Camera").GetComponent<GameController>().true_time_minute, GameObject.Find("Main Camera").GetComponent<GameController>().true_time_hour);
        }
        else
        {
            true_time = false;
            Angle(Random.Range(1, 61), Random.Range(1, 24));
        }
    }

    public void Angle(float time_minute, float time_hour)
    {
        time_hour += time_minute / 60;
        angle_minute = -6 * time_minute + 360;
        angle_hour = -30 * time_hour;
        Return_time(angle_minute, angle_hour);
    }

    public void Return_time (float angle_minute, float angle_hour)
    {
        minute.transform.eulerAngles = new Vector3(0, 0, angle_minute);
        hour.transform.eulerAngles = new Vector3(0, 0, angle_hour);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.down * velocity);

        if (true_broke)
        {
            broke1.transform.Translate(Vector2.left * 0.5f, Space.World);
            broke1.transform.Rotate(Vector3.forward * 50f);
            broke4.transform.Translate(Vector2.left * 0.5f, Space.World);
            broke4.transform.Rotate(Vector3.forward * 50f);
            broke2.transform.Translate(Vector2.right * 0.5f, Space.World);
            broke2.transform.Rotate(-Vector3.forward * 50f);
            broke3.transform.Translate(Vector2.right * 0.5f, Space.World);
            broke3.transform.Rotate(-Vector3.forward * 50f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (true_time)
        {
            if (collision.gameObject.tag == "Destroyer")
            {
                sounds_game.PlayOneShot(miss_clock);
                true_broke = true;
                minute.SetActive(false);
                hour.SetActive(false);
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                if (GameObject.Find("Clock_bonus").GetComponent<Bonuses>().points > 0)
                {
                    GameObject.Find("Clock_bonus").GetComponent<Bonuses>().points -= 1;
                    GameObject.Find("Clock_bonus").GetComponent<Bonuses>().Refresh();
                }
                Destroy(this.gameObject, 0.5f);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!true_time)
        {
            if (collision.gameObject.tag == "Destroyer")
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnMouseUpAsButton()
    {
        if (!done)
        {
            done = true;
            if (true_time)
            {
                GameObject.Find("Main Camera").GetComponent<GameController>().velosity += 0.001f;
                GameObject.Find("Main Camera").GetComponent<GameController>().time -= 0.008f;
                sounds_game.PlayOneShot(right_clock);
                if (GameObject.Find("Clock_bonus").GetComponent<Bonuses>().points < 12)
                {
                    GameObject.Find("Clock_bonus").GetComponent<Bonuses>().points += 1;
                    GameObject.Find("Clock_bonus").GetComponent<Bonuses>().Refresh();
                }
                Destroy(this.gameObject, 0.5f);
            }
            else
            {
                GameObject.Find("Main Camera").GetComponent<GameController>().velosity += 0.001f;
                GameObject.Find("Main Camera").GetComponent<GameController>().time -= 0.008f;
                sounds_game.PlayOneShot(wrong_clock);
                if (GameObject.Find("Clock_bonus").GetComponent<Bonuses>().points > 0)
                {
                    GameObject.Find("Clock_bonus").GetComponent<Bonuses>().points -= 1;
                    GameObject.Find("Clock_bonus").GetComponent<Bonuses>().Refresh();
                }
                Destroy(this.gameObject, 0.5f);
            }
        }
    }
}
