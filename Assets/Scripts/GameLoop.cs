using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static PlayerMovement;

public class GameLoop : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (!PlayerMovement.gameOver)
        {
            GameObject.Find("GameMaster").GetComponent<ChoosePlatform>().enabled = true;
        }
    }
}
