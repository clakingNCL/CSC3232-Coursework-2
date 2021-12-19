using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using static Score;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    private bool jump = false;
    private bool isGrounded = false;
    public static bool gameOver = false;
    public Score score;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hexagon"))
        {
            isGrounded = true; //Used to only allow a player to jump when grounded
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "HexGame1")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //Load the scene containing the first game
        }
        if (other.gameObject.tag == "HexGame2")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2); //Load the scene containing the second game
        }
        if (other.gameObject.tag == "End")
        {
            gameOver = true;
            GameObject.Find("Game Over").GetComponent<Text>().enabled = true; //End the game and display "Game Over" text
        }
        if (other.gameObject.tag == "Pellet")
        {
            rb.mass += 0.1f; //Increase the weight of the player whenever they collet a pellet
            score.AddScore();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!jump && Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        if (!gameOver)
        {
            if (Input.GetKey("w"))
            {
                rb.AddForce(500 * Time.deltaTime, 0, 0);
            }
            if (Input.GetKey("s"))
            {
                rb.AddForce(-500 * Time.deltaTime, 0, 0);
            }
            if (Input.GetKey("a"))
            {
                rb.AddForce(0, 0, +500 * Time.deltaTime);
            }
            if (Input.GetKey("d"))
            {
                rb.AddForce(0, 0, -500 * Time.deltaTime);
            }
            if (jump)
            {
                jump = false;
                rb.AddForce(0, 250, 0);
                isGrounded = false;
            }
        }
    }
}
