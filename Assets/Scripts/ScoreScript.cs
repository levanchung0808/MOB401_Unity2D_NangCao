using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    private TextMeshProUGUI txtCoin;

    public static int coin;
    // Start is called before the first frame update
    void Start()
    {
        txtCoin = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        txtCoin.text = coin.ToString();
    }
}
