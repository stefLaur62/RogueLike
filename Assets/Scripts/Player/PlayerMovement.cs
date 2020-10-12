using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool canfall;
    private ParameterData data;
    private string file = "config.txt";

    public float speed = 5;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider2d;


    public float jumpVelocity = 5f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private bool isGrounded = false;
    public Transform isGroundedChecker;
    public float checkGroundRadius;
    public LayerMask groundLayer;

    private Animator anim;
    private bool faceRight = true;

    public ConfigManager config;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider2d = GetComponent<BoxCollider2D>();

        data = config.data;

    }

    void Update()
    {
        Move();
        BetterJump();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collide");
    }

    private void Move()
    {
        bool isWalking = false;
        Vector3 pos = transform.position;
        if (Input.GetKey(data.right))
        {
            isWalking = true;
            SetWalkingAnimation(true);
            pos.x += speed * Time.deltaTime;
            FlipSpriteRight();
        }
        if (Input.GetKey(data.left))
        {
            isWalking = true;
            SetWalkingAnimation(true);
            pos.x -= speed * Time.deltaTime;
            FlipSpriteLeft();
        }
        rb.transform.position = pos;
        if (!isWalking)
        {
            SetWalkingAnimation(false);
        }
    }

    private void FlipSpriteRight()
    {
        if (!faceRight)
        {
            transform.localScale = new Vector3(2.5f, 2.5f, 1);
        }
        faceRight = true;
    }

    private void FlipSpriteLeft()
    {
        if (faceRight)
        {
            transform.localScale = new Vector3(-2.5f, 2.5f, 1);
        }
        faceRight = false;
    }

    void BetterJump()
    {
        CheckIfGrounded();
        if (Input.GetKey(data.jump) && isGrounded)
        {
            anim.SetBool("isJumping", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
        }
    }

    void CheckIfGrounded()
    {
        Collider2D collider = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer);
        if (collider != null)
        {
            isGrounded = true;
            anim.SetBool("isJumping", false);

        }
        else
        {
            isGrounded = false;
        }
    }
    void SetWalkingAnimation(bool isWalking)
    {
        if (anim != null)
        {
            if (isWalking)
            {
                anim.SetBool("isWalking", true);
            }
            else
            {
                anim.SetBool("isWalking", false);
            }
        } else
        {
            Debug.LogError("No animation loaded");
        }
    }





    /*public void LoadConfig()
    {
        data = new ParameterData();
        string json = ReadFromFile(file);
        JsonUtility.FromJsonOverwrite(json, data);

    }

    private string ReadFromFile(string filename)
    {
        string path = GetFilePath(filename);
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                return json;
            }
        }
        else
        {
            Debug.Log(GetFilePath(filename));
        }
        return "";
    }
    private string GetFilePath(string filename)
    {
        return Application.persistentDataPath + "/Config/" + filename;
    }*/
}
