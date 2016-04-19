using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class CoinsShow : MonoBehaviour {

    Text text;

    void Awake()
    {
        text = GetComponent<Text>();
    }

    void Update ()
    {
        if (PlayerPrefs.GetInt("CoinsCount", -1) != -1)
        {
            if (PlayerPrefs.GetInt("CoinsCount", -1) != Int32.Parse(text.text))
            {
                GetComponent<Text>().text = PlayerPrefs.GetInt("CoinsCount", -1).ToString();
            }
        }
        else
        {
            PlayerPrefs.SetInt("CoinsCount", 1000);
            Update();
        }
	}
}
