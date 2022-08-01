using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraScript : MonoBehaviour
{
    private Transform Player;
    //lấy vị trí X, Y nhỏ nhất của camera
    private float minX = -2.8f;
    private float maxX = 70;
    private float minY = 0.2499919f;
    private float maxY = 4.272172f;
    private void Start()
    {
        Player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if (Player != null)
        {
            Vector3 vitri = transform.position;
            vitri.x = Player.position.x;
            vitri.y = Player.position.y;
            if (vitri.x < minX) vitri.x = -2.8f;
            else if (vitri.x > maxX) vitri.x = maxX;

            if (vitri.y < minY) vitri.y = 0.2499919f;
            else if (vitri.y > maxY) vitri.y = maxY;
            transform.position = vitri;
        }

        ReloadGame();

    }
    void ReloadGame()
    {
        if (Input.GetKey(KeyCode.R))
        {
            if (!PlayerScript.checkPauseVolume)
            {
                Debug.Log("RELOAD GAME");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

}


