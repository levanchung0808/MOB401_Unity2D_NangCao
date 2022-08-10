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
    public InputField txtConfirmPassword;
    public InputField txtFullname;
    public InputField imgAvatar;
    public HttpPortal httpPortal;

    public static string tk;

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
        tk = username.ToString();
        var password = txtPassword.text;

        string url = "https://mob401laptrinhservernodejs.herokuapp.com/api/sign-in";
        WWWForm form = new WWWForm(); 
        form.AddField("username", username);
        form.AddField("password", password);

        var httpClient = new HttpPortal(new Serialization());
        var result = await httpClient.Post<Response>(url, form);
        if (result != null)
        {
            
            Storage storage = Storage.Instance;
            storage.token = result.data.token;
            LoadMenu();
        }
        else
        {
            httpPortal.ShowMessage("Login failed");
        }
    }

    public async void Register()
    {
        var username = txtEmail.text;
        var password = txtPassword.text;
        var fullname = txtFullname.text;
        var image = "https://res.cloudinary.com/dq7xnkfde/image/upload/v1654415375/avatar_bs4grl.png";

        string url = "https://mob401laptrinhservernodejs.herokuapp.com/api/sign-up";
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        form.AddField("fullname", fullname);
        form.AddField("image", image);

        var httpClient = new HttpPortal(new Serialization());
        var result = await httpClient.Post<Response>(url, form);
        if (result != null)
        {
            Debug.Log("REGISTER SUCCESS");
        }
        else
        {
            httpPortal.ShowMessage("Register failed");
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }
}
