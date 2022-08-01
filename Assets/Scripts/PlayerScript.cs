using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Animator HanhDong;
    private Rigidbody2D r2d;
    public static AudioSource AmThanh;

    private float VanToc = 7;
    private bool QuayPhai = true;
    private float TocDo = 0;
    private bool DuoiDat = true;
    private float NhayCao = 550;
    private float RoiXuong = 5;
    GameObject cameraObj;

    public GameObject RuongVang1, RuongVang2, RuongVang3;
    private int SLRuongVang = 0;

    public static bool checkPauseVolume = false;

    // Start is called before the first frame update
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        HanhDong = GetComponent<Animator>();
        AmThanh = GetComponent<AudioSource>();
        cameraObj = GameObject.FindWithTag("MainCamera");
        RuongVang1.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.3f);
        RuongVang2.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.3f);
        RuongVang3.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        HanhDong.SetFloat("TocDo", TocDo);
        HanhDong.SetBool("DuoiDat", DuoiDat);
        NhayLen();
        DiChuyen();

        if (Input.GetKeyDown(KeyCode.P))
        {
            checkPauseVolume = !checkPauseVolume;
            if (checkPauseVolume)
            {
                AmThanh.Pause();
            }
            else
            {
                AmThanh.UnPause();
            }
        }
    }

    private void FixedUpdate()
    {
        
    }

    void DiChuyen()
    {
        float PhimTraiPhai = Input.GetAxis("Horizontal");
        r2d.velocity = new Vector2(VanToc * PhimTraiPhai, r2d.velocity.y);
        TocDo = Mathf.Abs(VanToc * PhimTraiPhai);
        if (PhimTraiPhai > 0 && !QuayPhai)
        {
            cameraObj.transform.position = new Vector3(cameraObj.transform.position.x, cameraObj.transform.position.y, -9.9f);
            QuayPhai = !QuayPhai;
            transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }
        if (PhimTraiPhai < 0 && QuayPhai)
        {
            cameraObj.transform.position = new Vector3(cameraObj.transform.position.x, cameraObj.transform.position.y, -9.9f);
            QuayPhai = !QuayPhai;
            transform.localScale = new Vector3(-0.2f, 0.2f, 0.2f);
        }
    }

    void NhayLen()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (DuoiDat == false) return;
            r2d.AddForce((Vector2.up) * NhayCao);
            TaoAmThanh("nhay");
            DuoiDat = false;
            /*TaoAmThanh("smb_jump-super");*/

        }
        //áp dụng lực hút trái đất để Mario rơi xuống nhanh hơn
        if (r2d.velocity.y < 0)
        {
            r2d.velocity += Vector2.up * Physics2D.gravity.y * (RoiXuong - 1) * Time.deltaTime;
        }
        else if (r2d.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            r2d.velocity += Vector2.up * Physics2D.gravity.y * (5 -1) * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "NenDat")
        {
            DuoiDat = true;
        }
        if(collision.gameObject.tag == "Block_")
        {
            DuoiDat = true;
        }
        if(collision.gameObject.tag =="Enemy_Pig" || collision.gameObject.tag == "Enemy_Chicken")
        {
            if (collision.contacts[0].normal.x > 0 || collision.contacts[0].normal.x < 0)
            {
                HanhDong.SetBool("Chet", true); 
                TaoAmThanh("chet");
            }
            else if (collision.contacts[0].normal.y > 0 || collision.contacts[0].normal.y < 0)
            {
                Destroy(collision.gameObject);
                TaoAmThanh("DapDau");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            ScoreScript.coin += 1;
            TaoAmThanh("AnTien");
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "RuongVang")
        {
            TaoAmThanh("AnRuongVang");
            Destroy(collision.gameObject);
            SLRuongVang += 1;
            if(SLRuongVang == 1)
            {
                RuongVang1.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            }else if(SLRuongVang == 2)
            {
                RuongVang1.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                RuongVang2.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            }else if(SLRuongVang == 3)
            {
                RuongVang1.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                RuongVang2.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                RuongVang3.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            }
        }
    }

    public static void TaoAmThanh(string fileAmThanh)
    {
        //lấy path resources
        AmThanh.PlayOneShot(Resources.Load<AudioClip>("Audio/" + fileAmThanh));
    }
}
