using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform firePosition;
    public GameObject projectile;
    private bool canShoot = false;
    private float cooldownTime = 10f;
    GameObject obj;
    Animator ani;


    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        StartCoroutine(isShooting());
        shoot();
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot)
        {
            StartCoroutine(shoot());
        }
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

    public IEnumerator isShooting()
    {
        yield return new WaitForSeconds(10);
        canShoot = true;
    }

    void shootLogic()
    {
        obj = Instantiate(projectile, firePosition.position, firePosition.rotation);
        if (transform.localScale.x == -0.2f)
        {
            /*obj.transform.localScale = new Vector3(-1f, 1f, 1f);*/
            obj.transform.eulerAngles = new Vector3(0.2f, 0.2f, 0.2f);
        }
        else
        {
            /*obj.transform.localScale = new Vector3(1f, 1f, 1f);*/
            obj.transform.eulerAngles = new Vector3(0.2f, 0.2f, 0.2f);
        }

        PlayerScript.TaoAmThanh("tiengsung");
    }
}
