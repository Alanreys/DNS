using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject text_hours;
    public GameObject text_minutes;
    public GameObject clock;
    public Text text_hour, text_minute, text_hour_st, text_minute_st;
    public float true_time_minute;
    public float true_time_hour;
    public float time;
    public float velosity;

    private float pos_x;
    private float rand_hour1_more;
    private float rand_hour2_more;
    private float rand_minute1_more;
    private float rand_minute2_more;
    private float rand_hour1_less;
    private float rand_hour2_less;
    private float rand_minute1_less;
    private float rand_minute2_less;
    private int math_sigh;

    void Start()
    {
        text_hour = text_hours.GetComponent<Text>();
        text_minute = text_minutes.GetComponent<Text>();
        time = 3f;
        velosity = 0.025f;
        StartCoroutine(Clock_spawn());
        if (PlayerPrefs.GetInt("level") == 1)
        {
            StartCoroutine(Time_level1());
        }
        else if (PlayerPrefs.GetInt("level") == 2)
        {
            StartCoroutine(Time_level2());
        }
    }

    public void true_set_time(float time_minute, float time_hour)
    {
        true_time_minute = time_minute;
        true_time_hour = time_hour;
    }


    IEnumerator Clock_spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            pos_x = Random.Range(-4.6f, 4.6f);
            Instantiate(clock, new Vector2(pos_x, 5.7f), Quaternion.identity);
        }
    }

    IEnumerator Time_level1()
    {
        while (true)
        {
            math_sigh = Random.Range(0, 2);
            if (math_sigh == 0)
            {
                rand_hour1_more = Random.Range(0, 12);
                rand_hour2_more = Random.Range(0, 13);
                rand_minute1_more = Random.Range(0, 31);
                rand_minute2_more = Random.Range(0, 31);

                text_hour.text = rand_hour1_more.ToString() + "\n" + "+" + "\n" + rand_hour2_more.ToString();
                text_minute.text = rand_minute1_more.ToString() + "\n" + "+" + "\n" + rand_minute2_more.ToString();

                true_set_time(rand_minute1_more + rand_minute2_more, rand_hour1_more + rand_hour2_more);
            }
            else
            {
                rand_hour1_less = Random.Range(12, 24);
                rand_hour2_less = Random.Range(0, 13);
                rand_minute1_less = Random.Range(30, 61);
                rand_minute2_less = Random.Range(0, 31);

                text_hour.text = rand_hour1_less.ToString() + "\n" + "-" + "\n" + rand_hour2_less.ToString();
                text_minute.text = rand_minute1_less.ToString() + "\n" + "-" + "\n" + rand_minute2_less.ToString();

                true_set_time(rand_minute1_less - rand_minute2_less, rand_hour1_less - rand_hour2_less);
            }
            yield return new WaitForSeconds(8f);
        }
    }

    IEnumerator Time_level2()
    {
        while (true)
        {
            text_hour_st.text = "";
            text_minute_st.text = "";

            math_sigh = Random.Range(0, 2);
            if (math_sigh == 0)
            {
                rand_hour1_more = Random.Range(0, 6);
                rand_hour2_more = Random.Range(0, 6);
                if (rand_hour1_more == 5 && rand_hour2_more == 5) rand_hour2_more -= 0.4f;
                rand_minute1_more = Random.Range(0, 9);
                rand_minute2_more = Random.Range(0, 9);
                if (rand_minute1_more == 8 && rand_minute2_more == 8) rand_minute2_more -= 0.5f;

                text_hour.text = rand_hour1_more.ToString() + "\n" + "*" + "\n" + rand_hour2_more.ToString();
                text_minute.text = rand_minute1_more.ToString() + "\n" + "*" + "\n" + rand_minute2_more.ToString();

                true_set_time(rand_minute1_more * rand_minute2_more, rand_hour1_more * rand_hour2_more);
                yield return new WaitForSeconds(13f);
            }
            else
            {
                rand_hour1_more = Random.Range(0, 5);
                rand_hour2_more = 2;
                rand_minute1_more = Random.Range(0, 8);
                rand_minute2_more = 2;

                text_hour.text = rand_hour1_more.ToString();
                text_hour_st.text = rand_hour2_more.ToString();
                text_minute.text = rand_minute1_more.ToString();
                text_minute_st.text = rand_minute2_more.ToString();

                true_set_time(Mathf.Pow(rand_minute1_more, 2), Mathf.Pow(rand_hour1_more, 2));
            }
            yield return new WaitForSeconds(10f);
        }
    }
}
