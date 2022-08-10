using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
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
        if (collision.gameObject.tag == "NenDat" || collision.gameObject.tag == "Fireball")
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
            PlayerScript.TaoAmThanh("boomno");
            Destroy(gameObject);

        }
        else if (collision.gameObject.tag == "Enemy_Pig" || collision.gameObject.tag == "Enemy_Chicken")
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
            PlayerScript.TaoAmThanh("boomno");
            if (collision.gameObject.tag == "Enemy_Pig")
            {
                PlayerScript.TaoAmThanh("heochet");
                /*if (enemyScript.currentHealthEnemy == 0)
                {
                    Debug.Log("Heo đã chết");
                }*/
                /*enemyScript.TakeDame(20);*/
            }
            else if (collision.gameObject.tag == "Enemy_Chicken") PlayerScript.TaoAmThanh("gachet");
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "BOSS")
        {
            EnemyScript enemyScript = collision.gameObject.GetComponent<EnemyScript>();
            PlayerScript playerScript = collision.gameObject.GetComponent<PlayerScript>();
            if (enemyScript.currentHealthEnemy <= 0)
            {
                Debug.Log("ENEMY DIE");
                playerScript.WinGame();
                playerScript.SaveScore(ScoreScript.coin + 100);
            }
            enemyScript.TakeDame(10);
            Instantiate(impactEffect, transform.position, Quaternion.identity);
            PlayerScript.TaoAmThanh("boomno");
            Destroy(gameObject);
            
        }
    }

    /* IEnumerator Wait2sKillEnemy(GameObject obj)
     {
         yield return new WaitForSeconds(0.3f);
         Destroy(obj);
     }*/
}
