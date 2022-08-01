using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage
{
    public string token { get; set; } = null;
    Storage() { }
    private static readonly object _lock = new object();
    private static Storage instance = null;
    public static Storage Instance
    {
        get
        {
            if(instance == null)
            {
                lock (_lock)
                {
                    if(instance == null)
                    {
                        instance = new Storage();
                    }
                }
            }
            return instance;
        }
    }
}
