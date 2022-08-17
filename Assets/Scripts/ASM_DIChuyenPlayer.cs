using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ASM_DIChuyenPlayer : MonoBehaviour
{


    private Rigidbody2D r2d;
    public static AudioSource AmThanh;

    private float VanToc = 7;
    private bool QuayPhai = true;
    private float TocDo = 0;
    private bool DuoiDat = true;
    private float NhayCao = 550;
    private float RoiXuong = 5;
    GameObject cameraObj;
    public GameObject panel_score;

    public TextMeshProUGUI txtTime;
    public TextMeshProUGUI txtTimePanel;
    int Times = 0;
    bool check = true;
    // Start is called before the first frame update
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        cameraObj = GameObject.FindWithTag("MainCamera");
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(time());
        NhayLen();
        DiChuyen();
        txtTime.text = Times + "";
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
            transform.localScale = new Vector3(3f, 3f, 3f);
        }
        if (PhimTraiPhai < 0 && QuayPhai)
        {
            cameraObj.transform.position = new Vector3(cameraObj.transform.position.x, cameraObj.transform.position.y, -9.9f);
            QuayPhai = !QuayPhai;
            transform.localScale = new Vector3(-3f, 3f, 3f);
        }
    }

    void NhayLen()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (DuoiDat == false) return;
            r2d.AddForce((Vector2.up) * NhayCao);
            DuoiDat = false;
        }
        //áp dụng lực hút trái đất để Mario rơi xuống nhanh hơn
        if (r2d.velocity.y < 0)
        {
            r2d.velocity += Vector2.up * Physics2D.gravity.y * (RoiXuong - 1) * Time.deltaTime;
        }
        else if (r2d.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            r2d.velocity += Vector2.up * Physics2D.gravity.y * (5 - 1) * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "NenDat")
        {
            DuoiDat = true;
        }
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("YOU DIE");

            Time.timeScale = 0;
            panel_score.SetActive(true);
            txtTimePanel.text = txtTime.text;
            check = false;
        }
    }

    IEnumerator time()
    {
        if (check)
        {
            while (true)
            {
                Times += 1;
                yield return new WaitForSeconds(1);
            }
        }
        
    }
}
