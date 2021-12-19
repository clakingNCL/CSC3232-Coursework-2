using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingPlatform : MonoBehaviour
{
    public Rigidbody rb;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<ConstantForce>().enabled = true; //Apply constance force to transport player to the load trigger
        }
    }

}
