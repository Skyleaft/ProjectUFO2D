using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public InputData inputData;
    public float moveSpeed = 5f;
    public float distanceSpeed;
    public float maxRadius;
    public Text speedText;
    public Text coinText;
    public float coin;

    private float randCoin;

    public Button btnBuy;

    Vector3 currentPos;
    Vector3 clickPos;
    Vector3 releasePos;
    Vector3 direction;
    Vector3 endPos;


    Camera cam;

    private PlayerVFX playerVFX;

    private TrailRenderer tr;

    private Rect windowRect = new Rect(20, 20, 120, 50);

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = FindObjectOfType<Camera>();
        playerVFX = GetComponent<PlayerVFX>();
        tr = GetComponent<TrailRenderer>();

        Button btn = btnBuy.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);


    }

    void TaskOnClick()
    {
        if (coin < 15)
        {
            Debug.Log("Uang Anda Kurang");
        }
        else
        {
            coin = coin - 15;
            moveSpeed = moveSpeed + 5;
        }
    }

    private void OnMouseDown()
    {
        inputData.isPressed = true;
        StopPlayerPos();
        
    }
    private void OnMouseUp()
    {
        inputData.isPressed = false;
        
        releasePos = cam.ScreenToWorldPoint(Input.mousePosition);
        releasePos = new Vector3(releasePos.x, releasePos.y, 0f);
        //Debug.Log(releasePos);

        playerVFX.ChangeDotActiveState(false);
        playerVFX.ResetUFOSize();

        CalculateDirection();
        MovePlayerInDirection();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        speedText.text = rb.velocity.magnitude.ToString();
        coinText.text = coin.ToString();
    }

    void HandleMovement()
    {
        if (inputData.isPressed)
        {
            
            clickPos = rb.position;
            clickPos = new Vector3(clickPos.x, clickPos.y, 0f);

            currentPos = cam.ScreenToWorldPoint(Input.mousePosition);
            CalculateDirection();
            


            playerVFX.SetDotPos(rb.position, endPos);
            playerVFX.MakeUFOPulse();

            playerVFX.ChangeDotActiveState(true);
            
            Debug.Log(clickPos);
        }

        
    }

    void CalculateDirection()
    {
        var difference = currentPos - clickPos;
        direction = difference.normalized;
        var distance = Mathf.Min(maxRadius, difference.magnitude);
        endPos = clickPos + direction * distance;

    }

    void MovePlayerInDirection()
    {
        distanceSpeed = Vector2.Distance(clickPos, endPos);
        rb.velocity = direction * (distanceSpeed + moveSpeed);
        
    }

    void StopPlayerPos()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0;
        releasePos = Vector3.zero;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PickUp")
        {
            randCoin = Random.Range(0.5f, 4f);
            coin = coin + randCoin;
            
        }
    }
}
