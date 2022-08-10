using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using UnityEngine.Networking;
using System.Net.Http;
using System.Net.Http.Headers;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    Animator HanhDong;
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

    public float maxHealth = 1f;
    public float currentHealth;
    public HealthBar healthBar;

    public float attackSpeed = 0.5f;
    public float canAttack;

    public GameObject objGameOverScreen;
    public TextMeshProUGUI txtTextTitle, txtScore;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        r2d = GetComponent<Rigidbody2D>();
        HanhDong = GetComponent<Animator>();
        AmThanh = GetComponent<AudioSource>();
        cameraObj = GameObject.FindWithTag("MainCamera");
        RuongVang1.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.3f);
        RuongVang2.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.3f);
        RuongVang3.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.3f);

        currentHealth = maxHealth;
        healthBar.SetHealth(maxHealth);
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

        //save sate
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveState("Level 2", transform.localPosition.x.ToString(), transform.localPosition.y.ToString());
        }

        //save score
        if (Input.GetKeyDown(KeyCode.G))
        {
            SaveScore(100);
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

        if (collision.gameObject.tag == "Wingame")
        {
            Time.timeScale = 0;
            TaoAmThanh("wingame");
            SaveScore(ScoreScript.coin);
            SceneManager.LoadScene("Level_2");
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

    public void TakeDame(float dame)
    {
        if (currentHealth == 0 || currentHealth < 0)
        {
            LoseGame();
            HanhDong.SetBool("Chet", true);
            TaoAmThanh("chet");
            currentHealth = 0;
        }
        currentHealth -= dame;
        healthBar.SetHealth(currentHealth);
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "BOSS")
        {
            if (attackSpeed <= canAttack)
            {
                if (currentHealth <= 0)
                {
                    LoseGame();
                    HanhDong.SetBool("Chet", true);
                    TaoAmThanh("chet");
                    currentHealth = 0;
                }
                TakeDame(0.1f);
                canAttack = 0f;
            }
            else
            {
                canAttack += Time.deltaTime;
            }
        }
    }

    public async void SaveState(string name, string posX, string posY)
    {
        string url = "https://mob401laptrinhservernodejs.herokuapp.com/api/save-state";
        WWWForm form = new WWWForm();
        form.AddField("name", name);
        form.AddField("posX", posX);
        form.AddField("posY", posY);

        var httpClient = new HttpPortal(new Serialization());
        var result = await httpClient.Post<Response>(url, form);
        if (result != null)
        {
            Debug.Log("SAVE STATE SUCCESS");
        }
    }

    public async void SaveScore(int score)
    {
        string url = "http://localhost:3000/api/save-score";
        WWWForm form = new WWWForm();
        form.AddField("username", MenuScript.tk);
        form.AddField("score", score);
        Debug.Log("SCORE: "+score + " tk:"+ MenuScript.tk);

        var httpClient = new HttpPortal(new Serialization());
        var result = await httpClient.Post<Result>(url, form);
        Debug.Log(result);
        if (result != null)
        {
            Debug.Log("SCORE PLAYER" + result.user);
            Debug.Log("SAVE SCORE SUCCESS");
        }
    }

    public void LoseGame()
    {
        txtTextTitle.text = "GAME OVERRRRRR";
        txtScore.text = ScoreScript.coin.ToString();
        objGameOverScreen.SetActive(true);
        Time.timeScale = 0;
        Debug.Log("FINAL COIN END GAME: "+DiamondScript.diamond);
        Debug.Log("FINAL score END GAME" + ScoreScript.coin);
        SaveScore(ScoreScript.coin);
    }

    public void WinGame()
    {
        txtTextTitle.text = "YOU WIN";
        txtScore.text = ScoreScript.coin.ToString();
        PlayerScript.TaoAmThanh("wingame");
        objGameOverScreen.SetActive(true);
        Time.timeScale = 0;
        SaveScore(ScoreScript.coin);
    }
}
