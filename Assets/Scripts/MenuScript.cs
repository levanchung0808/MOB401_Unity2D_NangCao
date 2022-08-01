using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuScript : MonoBehaviour
{
    public InputField txtEmail;
    public InputField txtPassword;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public async void Login()
    {
        var username = txtEmail.text;
        var password = txtPassword.text;

        string url = "http://localhost:3000/api/sign-in";
        WWWForm form = new WWWForm(); 
        form.AddField("username", username);
        form.AddField("password", password);

        var httpClient = new HttpPortal(new Serialization());
        var result = await httpClient.Post<Response>(url, form);
        if (result != null)
        {
            Storage storage = Storage.Instance;
            storage.token = result.data.token;
            SceneManager.LoadScene("Level_1");
        }
    }
}
