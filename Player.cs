using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Rigidbody2D rigi;
    public float speedRun = 3f;
    public float forceJump = 1.0f;
    private bool isOnground;
    public Text txtScore;
    public int score;
    public int highscore;
    public int HP = 100;
    public bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        rigi=gameObject.GetComponent<Rigidbody2D>();
        score = 0;
        highscore = PlayerPrefs.GetInt("hightscore", 0);
        
    }


    // Update is called once per frame
   private void Update()
    {
        if (isDead) return;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rigi.velocity = new Vector2(speedRun, rigi.velocity.y);
           // rigi.AddForce(new Vector2(speedRun, 0), ForceMode2D.Impulse);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigi.velocity = new Vector2(-speedRun, rigi.velocity.y);
           // rigi.AddForce(new Vector2(-speedRun, 0));
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow)||Input.GetKeyDown(KeyCode.RightArrow))
        {
            GameHelper.instance.SoundController.PlaySound(SoundName.Walk1, true);

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            GameHelper.instance.SoundController.PlaySound(SoundName.Walk1);

        }

        if (Input.GetKeyDown(KeyCode.UpArrow)&& isOnground)
        {
            rigi.AddForce(Vector2.up*forceJump, ForceMode2D.Impulse);
            GameHelper.instance.SoundController.PlaySound(SoundName.jump, false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fruits"))//mat qua tao
        {
            collision.gameObject.SetActive(false);
            score++;
            if(score>highscore)
            {
                highscore = score;
                PlayerPrefs.SetInt("highscore", highscore);
            }
            GameHelper.instance.SoundController.PlaySound(SoundName.Collect, false);
            txtScore.text = "Score : " + score;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Khi bat dau va cham se goi 1 lan
        if(collision.gameObject.CompareTag("Ground"))
        {
            isOnground = true;
            GameHelper.instance.SoundController.PlaySound(SoundName.LandOnGround, false);
        }
        

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        //Khi co tiep xuc se goi lien tuc
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnground = true;
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //Khi vua thoat khoi va cham se goi 1 lan
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnground = false;
        }
    }

}

