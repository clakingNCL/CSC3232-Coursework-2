using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosePlatform : MonoBehaviour
{
    private int platformToStay;
    private int setGravity;
    GameObject gameObj;
    private Transform[] pellets;

    void OnEnable()
    {
        platformToStay = Random.Range(0, 7);
        setGravity = Random.Range(0, 3);
        StartCoroutine(RenderAndMoveHexagons(platformToStay));
    }

    IEnumerator RenderAndMoveHexagons(int platformToStay) //Select hexagon to render on the screen and remain in position whilst moving the others
    {
        switch (platformToStay)
        {
            case (0):
                RenderObj("RedHexScreen");
                EnablePellet("RedHexPellet");
                for (int i=2; i <= 3; i++)
                {
                    yield return new WaitForSeconds(i);
                    MoveHex("BlueHex");
                    MoveHex("PurpleHex");
                    MoveHex("PinkHex");
                    MoveHex("GreenHex");
                    MoveHex("OrangeHex");
                    MoveHex("WhiteHex");
                }
                break;
            case (1):
                RenderObj("BlueHexScreen");
                EnablePellet("BlueHexPellet");
                for (int i = 2; i <= 3; i++)
                {
                    yield return new WaitForSeconds(i);
                    MoveHex("RedHex");
                    MoveHex("PurpleHex");
                    MoveHex("PinkHex");
                    MoveHex("GreenHex");
                    MoveHex("OrangeHex");
                    MoveHex("WhiteHex");
                }
                break;
            case (2):
                RenderObj("PurpleHexScreen");
                EnablePellet("PurpleHexPellet");
                for (int i = 2; i <= 3; i++)
                {
                    yield return new WaitForSeconds(i);
                    MoveHex("BlueHex");
                    MoveHex("RedHex");
                    MoveHex("PinkHex");
                    MoveHex("GreenHex");
                    MoveHex("OrangeHex");
                    MoveHex("WhiteHex");
                }
                break;
            case (3):
                RenderObj("PinkHexScreen");
                EnablePellet("PinkHexPellet");
                for (int i = 2; i <= 3; i++)
                {
                    yield return new WaitForSeconds(i);
                    MoveHex("BlueHex");
                    MoveHex("PurpleHex");
                    MoveHex("RedHex");
                    MoveHex("GreenHex");
                    MoveHex("OrangeHex");
                    MoveHex("WhiteHex");
                }
                break;
            case (4):
                RenderObj("GreenHexScreen");
                EnablePellet("GreenHexPellet");
                for (int i = 2; i <= 3; i++)
                {
                    yield return new WaitForSeconds(i);
                    MoveHex("BlueHex");
                    MoveHex("PurpleHex");
                    MoveHex("PinkHex");
                    MoveHex("RedHex");
                    MoveHex("OrangeHex");
                    MoveHex("WhiteHex");
                }
                break;
            case (5):
                RenderObj("OrangeHexScreen");
                EnablePellet("OrangeHexPellet");
                for (int i = 2; i <= 3; i++)
                {
                    yield return new WaitForSeconds(2);
                    MoveHex("BlueHex");
                    MoveHex("PurpleHex");
                    MoveHex("PinkHex");
                    MoveHex("GreenHex");
                    MoveHex("RedHex");
                    MoveHex("WhiteHex");
                }
                break;
            case (6):
                RenderObj("WhiteHexScreen");
                EnablePellet("WhiteHexPellet");
                for (int i = 2; i <= 3; i++)
                {
                    yield return new WaitForSeconds(i);
                    MoveHex("BlueHex");
                    MoveHex("PurpleHex");
                    MoveHex("PinkHex");
                    MoveHex("GreenHex");
                    MoveHex("OrangeHex");
                    MoveHex("RedHex");
                }
                break;
        }
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("DisplayHex"))
        {
            o.GetComponent<MeshRenderer>().enabled = false;
        }
        this.enabled = false;
    }

     void RenderObj(string objToRender)
    {
        gameObj = GameObject.Find(objToRender);
        gameObj.GetComponent<MeshRenderer>().enabled = true;
    }

    void EnablePellet(string pellet) //Enable revelant pellet
    {
        gameObj = GameObject.Find("Pellets");
        pellets = gameObj.transform.GetComponentsInChildren<Transform>(true);

        foreach (Transform pelletTransform in pellets)
        {
            if (pelletTransform.gameObject.name == pellet)
            {
                pelletTransform.gameObject.SetActive(true);
            }
        }
    }

    void MoveHex(string hexToMove) //Enable moving platform script on the Hexagon set to move
    {
        gameObj = GameObject.Find(hexToMove);
        gameObj.GetComponent<MovingPlatform>().enabled = true;
    }
}

