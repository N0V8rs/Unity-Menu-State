using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator anim;
    Vector2 movement;
    public GameManager gameManager;
    public LevelManager _levelManager;
    public string sceneName;
    public static PlayerController Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        _levelManager = FindObjectOfType<LevelManager>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        HandleMove();
        HandleAnim();

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("UnlockedDoor"))
        {
            GameObject.FindObjectOfType<LevelManager>().LoadScene(sceneName);
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            gameManager.GameWin();
            GameObject.FindObjectOfType<LevelManager>().LoadScene("WinScene");

        }

        if (other.gameObject.CompareTag("LoseTrigger"))
        {
            gameManager.GameOver();
            gameManager.gameState = GameManager.GameState.GameOver;
        }
    }

    public void HandleMove()
    {
        Vector2 normalizedMovement = movement.normalized;

        rb.velocity = normalizedMovement * moveSpeed;
    }

    public void HandleAnim()
    {
        if (movement != Vector2.zero)
        {
            anim.SetFloat("XInput", movement.x);
            anim.SetFloat("YInput", movement.y);
            anim.SetBool("Moving", true);
        }
        else
        {
            anim.SetBool("Moving", false);
        }
    }
}
