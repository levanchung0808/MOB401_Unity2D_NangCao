using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using EasyUI.Toast;

public class HttpPortal
{
    private readonly ISerialization _serialization;

    public HttpPortal(ISerialization serialization)
    {
        _serialization = serialization;
    }

    public async Task<TResultType> Get<TResultType>(string url)
    {
        try
        {
            using var www = UnityWebRequest.Get(url);
            www.SetRequestHeader("Content-Type", _serialization.ContentType);
            var operation = www.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Failed: {www.error}");
                return default;
            }

            var result = _serialization.Deserialize<TResultType>(www.downloadHandler.text);
            return result;
        }
        catch (Exception ex)
        {
            Debug.LogError($"{nameof(Get)} failed: {ex.Message}");
            return default;
        }
    }

    public async Task<TResultType> Post<TResultType>(string url, WWWForm form)
    {
        try
        {
            using var www = UnityWebRequest.Post(url, form);
            /*www.SetRequestHeader("Content-Type", _serialization.ContentType);*/
            var operation = www.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            if (www.result != UnityWebRequest.Result.Success)
            {
                ShowMessage("Email or Password is incorrect!");
                Debug.LogError($"Login Failed: {www.error}");
                return default;
            }
            var result = _serialization.Deserialize<TResultType>(www.downloadHandler.text);
            return result;
        }
        catch (Exception ex)
        {
            Debug.LogError($"{nameof(Post)} failed: {ex.Message}");
            return default;
        }
    }

    public void ShowMessage(string str)
    {
        Toast.Show(str, 4f, new Color(255f, 0f, 0f));
    }
}
