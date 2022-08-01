using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Response
{
    public bool error { get; set; }
    public string responseTimestamp { get; set; }
    public int statusCode { get; set; }
    public Data data { get; set; }

    override
    public string ToString()
    {
        return statusCode.ToString();
    }

}
