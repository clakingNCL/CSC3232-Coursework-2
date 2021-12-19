using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurfParticleEffect : MonoBehaviour
{
    public GameObject obj;
    public ParticleSystem greenEffect;
    public ParticleSystem redEffect;
    // Start is called before the first frame update
    void Start() //Disable all particles
    {
        var emGreen = greenEffect.emission;
        emGreen.enabled = false;
        var emRed = redEffect.emission;
        emRed.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) //Display green effect if the player is on that tile
        {
            var emGreen = greenEffect.emission;
            emGreen.enabled = true;
        }
        if (collision.gameObject.CompareTag("Enemy")) //Display red effect if the enemy is on that tile
        {
            var emRed = redEffect.emission;
            emRed.enabled = true;
        }
        StartCoroutine(stopEffects()); // We only want the effect to play for a very short amount of time
    }
    IEnumerator stopEffects()
    {
        yield return new WaitForSeconds(0.4f);
        var emGreen = greenEffect.emission;
        emGreen.enabled = false;
        var emRed = redEffect.emission;
        emRed.enabled = false;
    }

}
