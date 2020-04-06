using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Variables
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animations;

    public Transform FeetPos;
    public LayerMask WhatIsGround;
    public Text PointsText;
    public Text MorePointsText;
    public GameObject Panel;
    public GameObject WorldText;
    public Button NextLevel;
    public string SceneToLoad;

    public float HorizontalAxis;
    public float Speed = 5f;
    public float JumpRadius = 10f;
    public float JumpForce = 5f;
    public float JumpTimeCounter;
    public float OutOfBoundsY = -4.3f;
    public float JumpTime = 0.35f;

    public int Points;
    public int MaxPointsNeeded;

    public bool IsGrounded = true;
    public bool Jump;
    public bool JumpDown;
    public bool JumpUp;
    public bool IsJumping;

    void Start()
    {
        //Instatiating variables
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animations = GetComponent<Animator>();
        PointsText.text = $"{Points}/{MaxPointsNeeded}";
    }

    void Update()
    {
        //Handling Input
        HorizontalAxis = Input.GetAxisRaw("Horizontal");
        Jump = Input.GetButton("Jump");
        JumpDown = Input.GetButtonDown("Jump");
        JumpUp = Input.GetButtonUp("Jump");

        //Jump
        IsGrounded = Physics2D.OverlapCircle(FeetPos.position, JumpRadius, WhatIsGround);

        if (IsGrounded && JumpDown)
        {
            IsJumping = true;
            JumpTimeCounter = JumpTime;
            rb.velocity = Vector2.up * JumpForce;
        }

        if (Jump && IsJumping)
        {
            if (JumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * JumpForce;
                JumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                IsJumping = false;
            }
        }

        if (JumpUp)
            IsJumping = false;

        //Animations and flipping---------------------------------------

        //Flip
        if (HorizontalAxis == 1)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else if (HorizontalAxis == -1)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }

        //Animate
        if (HorizontalAxis != 0 && !IsGrounded)
        {
            animations.SetBool("IsMoving", false);
            animations.SetBool("IsJumping", true);
        }
        else if (HorizontalAxis != 0)
        {
            animations.SetBool("IsMoving", true);
            animations.SetBool("IsJumping", false);
        }
        else if (!IsGrounded)
        {
            animations.SetBool("IsJumping", true);
            animations.SetBool("IsMoving", false);
        }
        else
        {
            animations.SetBool("IsJumping", false);
            animations.SetBool("IsMoving", false);
        }

        //Out of Bounds
        if (transform.position.y <= OutOfBoundsY)
            GameOver();
    }

    void FixedUpdate()
    {
        //Movement
        rb.velocity = new Vector2(HorizontalAxis * Speed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Point")
        {
            this.Points += 1;
            PointsText.text = $"{Points}/{MaxPointsNeeded}";
        }
        else if (collision.gameObject.tag == "Finish" && Points >= MaxPointsNeeded)
            GameWon();
        else if (collision.gameObject.tag == "Finish" && Points < MaxPointsNeeded)
            AlertNeedMorePoints();
    }

    void GameOver()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    void GameWon()
    {
        rb.gravityScale = 0;
        rb.mass = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        Panel.SetActive(true);
        NextLevel.onClick.AddListener(LoadNextLevel);
        Invoke("StartWorldTextSlapAnimation", 2f);
    }

    void StartWorldTextSlapAnimation()
    {
        WorldText.GetComponent<Text>().text = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        WorldText.SetActive(true);
    }

    void AlertNeedMorePoints()
    {
        MorePointsText.text = $"You need {MaxPointsNeeded - Points} more points to finish this level!";
        MorePointsText.enabled = true;
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneToLoad);
    }
}
