using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class MenuMainScript : MonoBehaviour
{
    string jsonURLCharacter = "http://localhost:3000/api/get-all-characters";
    // Start is called before the first frame update
    void Start()
    {
        /*StartCoroutine(getData());*/
        GetCharacterss();
    }

    public async void GetCharacterss()
    {
        var httpClient = new HttpPortal(new Serialization());
        var result = await httpClient.Get<Character>(jsonURLCharacter);
        if (result != null)
        {
            
            Debug.Log("GetCharacterss SUCCESS");
        }
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level_1");
    }
}
