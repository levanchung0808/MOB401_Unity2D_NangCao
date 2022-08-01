using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform firePosition;
    public GameObject projectile;
    private bool canShoot = true;
    private float cooldownTime = 0.5f;
    GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (!PlayerScript.checkPauseVolume)
            {
                if (canShoot)
                {
                    StartCoroutine(shoot());
                }
            }
        }

        //limit distance a bullet
        /*if (GameObject.Find("Fireball(Clone)") != null)
        {
            if (obj.transform.localPosition.x >= limit)
            {
                Debug.Log("Limit");
            }
        }*/
    }

    public IEnumerator shoot()
    {
        //Instantiate your projectile
        shootLogic();
        canShoot = false;
        //wait for some time
        yield return new WaitForSeconds(cooldownTime);
        canShoot = true;
    }

    void shootLogic()
    {
        obj = Instantiate(projectile, firePosition.position, firePosition.rotation);
        if (transform.localScale.x == -0.2f)
        {
            /*obj.transform.localScale = new Vector3(-1f, 1f, 1f);*/
            obj.transform.eulerAngles = new Vector3(0f, 0f, 180f);
        }
        else
        {
            /*obj.transform.localScale = new Vector3(1f, 1f, 1f);*/
            obj.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }

        PlayerScript.TaoAmThanh("tiengsung");
    }
}
