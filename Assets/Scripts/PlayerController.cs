using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public enum SIDE
    {
        Left, Mid, Right
    }

    public SIDE m_side = SIDE.Mid;
    public float XValue;
    public float JumpPower;
    public float MoveSpeed;
    public GameObject coinMagnet;
    public float magnetTime;
    private float timerMagnet;
    private bool isMagnetActive = false;
    private int coins = 0;

    private CharacterController cc;
    private float NewPosX = 0f;
    private float playerPosX;
    private float x;
    private float y;
    private bool InJump;
    private bool InRoll;
    private float RollCounter;
    private float ColHeight;
    private float ColCenterY;
    private Vector3 scale;

    public Text coinText;
    public Text scoreText;
    private int score;
    private float timeScore = 2f;
    private float scoreTimer;
    public GameObject deathMenu;

    public bool isJetpack = false;
    public float jetpackTime;
    private float jetpackTimer;
    public float maxHeight;
    public float jetpackPower;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        playerPosX = transform.position.x;
        NewPosX = playerPosX;
        x = playerPosX;
        ColHeight = cc.height;
        ColCenterY = cc.center.y;
        scale = transform.localScale;

        scoreText.text = score.ToString();
        coinText.text = coins.ToString();

        deathMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (SwipeManager.swipeLeft)
        {
            if (m_side == SIDE.Mid)
            {
                NewPosX = playerPosX + XValue;
                m_side = SIDE.Left;
            }
            else if (m_side == SIDE.Right)
            {
                NewPosX = playerPosX;
                m_side = SIDE.Mid;
            }
        }

        if (SwipeManager.swipeRight)
        {
            if (m_side == SIDE.Mid)
            {
                NewPosX = playerPosX - XValue;
                m_side = SIDE.Right;
            }
            else if (m_side == SIDE.Left)
            {
                NewPosX = playerPosX;
                m_side = SIDE.Mid;
            }
        }
        x = Mathf.Lerp(transform.position.x, NewPosX, 10f*Time.deltaTime);
        Vector3 moveVector = new Vector3(x-transform.position.x, y*Time.deltaTime, -MoveSpeed*Time.deltaTime);
        cc.Move(moveVector);
        if (!isJetpack)
        {
            Jump();
            Roll();
        }
        else
        {
            Jetpack();
            if (jetpackTimer>0f)
            {
                jetpackTimer -= Time.deltaTime;
            }
            else
            {
                isJetpack = false;
            }
        }
        coinMagnet.transform.position = transform.position;
        if (isMagnetActive)
        {
            timerMagnet -= Time.deltaTime;
            if (timerMagnet<0f)
            {
                isMagnetActive = false;
                coinMagnet.SetActive(false);
            }
        }

        scoreTimer += Time.deltaTime;
        if (scoreTimer>timeScore)
        {
            scoreTimer = 0;
            score++;
            scoreText.text = score.ToString();
        }
    }
    private void Jump()
    {
        if (cc.isGrounded)
        {
            InJump = false;
            if (SwipeManager.swipeUp)
            {
                y = JumpPower;
                InJump = true;
            }
        }
        else
        {
            y -= JumpPower*3f*Time.deltaTime;
        }
    }
    private void Roll()
    {
        RollCounter -= Time.deltaTime;
        if (RollCounter <= 0f)
        {
            RollCounter = 0f;
            cc.center = new Vector3(0,ColCenterY,0);
            cc.height = ColHeight;
            transform.localScale = scale;
            InRoll = false;
        }
        if (SwipeManager.swipeDown)
        {
            RollCounter = 0.5f;
            cc.center = new Vector3(0, ColCenterY/2, 0);
            cc.height = ColHeight/10;
            transform.localScale = new Vector3(scale.x/2.4f, scale.y/2.4f, scale.z/2.4f);
            InRoll = true;
            InJump = false;
        }
    }
    private void Jetpack()
    {
        y = jetpackPower;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Magnet")
        {
            coinMagnet.SetActive(true);
            timerMagnet = magnetTime;
            Destroy(other.gameObject);
            isMagnetActive = true;
        }
        if (other.gameObject.tag == "Coin")
        {
            coins++;
            coinText.text = coins.ToString();
            Destroy(other.gameObject);
            Debug.Log(coins);
        }
        if (other.gameObject.tag == "Obstacles")
        {
            Time.timeScale = 0f;
            deathMenu.SetActive(true);
        }
        if (other.gameObject.tag == "Jetpack")
        {
            Destroy(other.gameObject);
            isJetpack = true;
            jetpackTimer = jetpackTime;
        }
    }
}
