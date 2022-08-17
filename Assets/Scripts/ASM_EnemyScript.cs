using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASM_EnemyScript : MonoBehaviour
{
    private Rigidbody2D r2d;
    public float VanTocConVat = 2;
   
    // Start is called before the first frame update
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 DiChuyen = transform.localPosition;
        DiChuyen.x -= VanTocConVat * Time.deltaTime;
        transform.localPosition = DiChuyen;
    }
}
