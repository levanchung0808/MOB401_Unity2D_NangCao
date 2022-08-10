using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class EnemyScript : MonoBehaviour
{
    private Rigidbody2D r2d;
    public float VanTocConVat = 2;
    public bool DiChuyenTrai = true;
    public float minXPositionEnemey = 0;
    public float maxXPositionEnemey = 0;

    public MyEnum SelectEnemy = new MyEnum();

    private Transform player;

    public int maxHealthEnemy = 100;
    public int currentHealthEnemy;
    public HealthBar healthBarEnemy;
    public float attackSpeed = 0.1f;
    public float canAttack;

    // Start is called before the first frame update
    void Start()
    {
       
        r2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        currentHealthEnemy = maxHealthEnemy;
        healthBarEnemy.SetMaxHealthEnemy(maxHealthEnemy);

    }

    // Update is called once per frame
    void Update()
    {
        /*Vector2 DiChuyen = transform.localPosition;
        DiChuyen.x += VanTocConVat * Time.deltaTime * (-1);
        transform.localPosition = DiChuyen;*/

        DiChuyenTrongKhoang();
    }

    private void FixedUpdate()
    {
        Vector2 DiChuyen = transform.localPosition;
        if (DiChuyenTrai) DiChuyen.x -= VanTocConVat * Time.deltaTime;
        else DiChuyen.x += VanTocConVat * Time.deltaTime;
        transform.localPosition = DiChuyen;
    }

    void DiChuyenTrongKhoang()
    {
        if (minXPositionEnemey != 0 && maxXPositionEnemey != 0)
        {
            if (player.transform.localPosition.x > minXPositionEnemey && player.transform.localPosition.x < maxXPositionEnemey)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, VanTocConVat * Time.deltaTime);
                if (transform.localPosition.x < player.localPosition.x)
                {
                    if (SelectEnemy.ToString() == "Pig")
                    {
                        transform.localScale = new Vector3(-0.7f, 0.7f, 0.7f);
                    }
                    else if (SelectEnemy.ToString() == "Chicken")
                    {
                        transform.localScale = new Vector3(-5f, 5f, 5f);
                    }
                    Vector2 DiChuyen = transform.localPosition;
                    DiChuyen.x += VanTocConVat * Time.deltaTime;
                    transform.localPosition = DiChuyen;
                }
                else
                {
                    if (SelectEnemy.ToString() == "Pig")
                    {
                        transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                    }
                    else if (SelectEnemy.ToString() == "Chicken")
                    {
                        transform.localScale = new Vector3(5f, 5f, 5f);
                    }
                    Vector2 DiChuyen = transform.localPosition;
                    DiChuyen.x -= VanTocConVat * Time.deltaTime;
                    transform.localPosition = DiChuyen;
                }
            }



            //di chuyển trong khoảng
            if (gameObject.transform.localPosition.x <= minXPositionEnemey)
            {
                //quay mặt
                DiChuyenTrai = !DiChuyenTrai;
                if (SelectEnemy.ToString() == ("Pig"))
                {
                    transform.localScale = new Vector3(-0.7f, 0.7f, 0.7f);
                }
                else if (SelectEnemy.ToString() == ("Chicken"))
                {
                    transform.localScale = new Vector3(-5f, 5f, 5f);

                }


            }
            else if (gameObject.transform.localPosition.x >= maxXPositionEnemey)
            {
                DiChuyenTrai = !DiChuyenTrai;
                if (SelectEnemy.ToString()== "Pig")
                {
                    transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                }
                else if (SelectEnemy.ToString() == "Chicken")
                {
                    transform.localScale = new Vector3(5f, 5f, 5f);
                }
            }
        }

    }

    public void TakeDame(int dame)
    {
        currentHealthEnemy -= dame;
        healthBarEnemy.SetHealthEnemy(currentHealthEnemy);
    }
}


public enum MyEnum
{
    Pig,
    Chicken,
};