using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiamondScript : MonoBehaviour
{
    private TextMeshProUGUI txtDiamond;

    public static int diamond;
    // Start is called before the first frame update
    void Start()
    {
        txtDiamond = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        txtDiamond.text = diamond.ToString();
    }
}
