using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public float startTime = 200;
    public TextMeshProUGUI theText;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        startTime -= Time.deltaTime;
        theText.text = "" + Mathf.Round(startTime);

        if (startTime <= 0)
        {
            Debug.Log("TIME END GAME");
        }
    }
}
