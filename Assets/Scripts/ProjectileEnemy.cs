using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemy : MonoBehaviour
{
    public float projectileSpeed = 10;
    public GameObject impactEffect;

    private Rigidbody2D rg;
    /*public EnemyScript enemyScript;*/
    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        rg.velocity = transform.right * projectileSpeed;
        /*enemyScript = GetComponent<EnemyScript>();*/
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "NenDat" || collision.gameObject.tag == "Fireball" || collision.gameObject.tag == "Player")
        {
            EnemyAttack();
        }
    }

    public void EnemyAttack()
    {
        PlayerScript playerScript = gameObject.GetComponent<PlayerScript>();
        playerScript.TakeDame(0.1f);
        Instantiate(impactEffect, transform.position, Quaternion.identity);
        PlayerScript.TaoAmThanh("boomno");
        Destroy(gameObject);
    }

    /* IEnumerator Wait2sKillEnemy(GameObject obj)
     {
         yield return new WaitForSeconds(0.3f);
         Destroy(obj);
     }*/
}
