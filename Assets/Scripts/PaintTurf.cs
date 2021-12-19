using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintTurf : MonoBehaviour
{
    public GameObject obj;
    
    public int enemyScore = 0;
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Hexagon") && obj.tag == "Player")
        {
            collision.gameObject.GetComponent<Renderer>().material.color = Color.green;
            source.Play(); //Play a sound effect when a player turfs over a hexagon
        }
        if (collision.gameObject.CompareTag("Hexagon") && obj.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
    }
}
