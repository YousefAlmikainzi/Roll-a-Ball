using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

//this script is for the player's movement and controls and other things
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 0f;
    [SerializeField] private float growth = 0.1f;
    public TextMeshProUGUI countText;
    private int points;
    public Text WallText;
    private int wallCount = 0;
    public Text losingText;
    public Text pointText;

    private Rigidbody playersRigidBody;
    private float movementX;
    private float movementY;
    void Start()
    {
        playersRigidBody = GetComponent<Rigidbody>();
        points = 0;
        WallText.text = "";
        losingText.text = "";
        pointText.text = "";
        SetCountText();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Points: " + points.ToString();
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);

        playersRigidBody.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            Renderer collectableRenderer = other.gameObject.GetComponent<Renderer>();
            collectableRenderer.material.color = Random.ColorHSV(0f,1f,1f,1f,0.5f,1f);
            transform.localScale = transform.localScale + new Vector3 (growth, growth, growth);
            points += 1;

            SetCountText();
        }
        else if(other.gameObject.CompareTag("Collectable2"))
        {
            other.gameObject.SetActive(false);
            points += 10;
            pointText.text = "You got 10 more points";
            Invoke("ClearMessage", 2.0f);
            SetCountText();
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            if (collision.gameObject.GetComponent<Renderer>().material.color != Color.red)
            {
                collision.gameObject.GetComponent<Renderer>().material.color = Color.red;
                wallCount++;

                if (wallCount < 4)
                {
                    WallText.text = $"Careful! You hit {wallCount} wall(s). If you hit all four you will lose!";
                    Invoke("ClearWallHitMessage", 2.0f);
                }
                else
                {
                    losingText.text = "YOU LOST";
                }
            }
        }
    }

    void ClearMessage()
    {
        pointText.text = "";
    }

    void ClearWallHitMessage()
    {
        WallText.text = "";
    }
}
