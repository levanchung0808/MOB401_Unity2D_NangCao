using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using System.Net.Http;
using System.Net.Http.Headers;

public class LoadAPI : MonoBehaviour
{
    public Text txtFullname;
    public TextMeshProUGUI txtCoin;
    public TextMeshProUGUI txtDiamond;
    public Image profileImage;
    // Start is called before the first frame update
    void Start()
    {
        GetInfo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async void GetInfo()
    {
        
        if(Storage.Instance.token != null)
        {
            string url = "https://mob401laptrinhservernodejs.herokuapp.com/api/user-info";
            var httpClient = new HttpPortal(new Serialization());
            var result = await httpClient.GetAPI<User>(url);
            if (result != null)
            {
                txtFullname.text = result.fullname;
                DiamondScript.diamond = result.coin;
                ScoreScript.coin = result.score;
                Debug.Log("GET SCORE SUCCESS");
            }
        }
        else
        {
            Debug.Log("ERROR TOKEN");
        }
        
    }

    public void yourMethod()
    {
        StartCoroutine(setImage("http://url/image.jpg")); //balanced parens CAS
    }

    IEnumerator setImage(string url)
    {
        Texture2D texture = profileImage.canvasRenderer.GetMaterial().mainTexture as Texture2D;

        WWW www = new WWW(url);
        yield return www;

        // calling this function with StartCoroutine solves the problem
        Debug.Log("Why on earh is this never called?");

        www.LoadImageIntoTexture(texture);
        www.Dispose();
        www = null;
    }

    
}
