using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool canfall;
    private ParameterData data;
    private string file = "";

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


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        LoadConfig();
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
        Vector3 pos = transform.position;
        if (Input.GetKey(data.right))
        {
            pos.x += speed * Time.deltaTime;
        }
        if (Input.GetKey(data.left))
        {
            pos.x -= speed * Time.deltaTime;
        }

        rb.transform.position = pos;
    }
    void BetterJump()
    {
        CheckIfGrounded();
        if (Input.GetKey(data.jump) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
        }
    }

    void CheckIfGrounded()
    {
        Collider2D collider = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer);
        if (collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }





    public void LoadConfig()
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
            Debug.LogWarning("File not found");
        }
        return "";
    }
    private string GetFilePath(string filename)
    {
        return Application.persistentDataPath + "/Config/" + filename;
    }
}
