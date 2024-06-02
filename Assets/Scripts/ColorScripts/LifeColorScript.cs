using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ColourScript : MonoBehaviour
{

private readonly string[,] colorRelations = new string[7,6]
{
    //*complementary
    {"Cyan","Red","Magenta","Green","Yellow","Blue"},
    //*analogous - make this into one
    {"Magenta","Blue","Cyan","Red","Magenta","Red"},
    {"Yellow","Green","Yellow","Blue","Cyan","Green"},
    //*monochrome
    //light
    {"LightRed","LightCyan","LightGreen","LightMageta","LightBlue","LightYellow"},
    {"LighterRed","LighterCyan","LighterGreen","LighterMageta","LighterBlue","LighterYellow"},
    //dark
    {"DarkRed","DarkCyan","DarkGreen","DarkMagenta","DarkBlue","DarkYellow"},
    {"DarkerRed","DarkerCyan","DarkerGreen","DarkerMagenta","DarkerBlue","DarkerYellow"}
    //*splitComplementary
    //*triadic
    //*square
    //*rectangle
};

private readonly List<string> allColors = new()
{
    "Red","Cyan","Green","Magenta","Blue","Yellow"
};

private void OnCollisionEnter2D(Collision2D that)
{
    string thatColor = that.gameObject.tag;
    string thisColor = gameObject.tag;
    
    int idx = allColors.IndexOf(thisColor);
    string complementary = colorRelations[idx, idx];
    string[] analogous = {colorRelations[idx + 1, idx], colorRelations[idx + 2, idx]};
    // string[] lighterThan = {colorRelations[idx + 3, idx], colorRelations[idx + 4, idx]};
    string[] darkerThan = {colorRelations[idx + 5, idx], colorRelations[idx + 6, idx]};


    //make for lifeforms and smoke as well - this is not just for platforms.
    if (analogous.Contains(thatColor))
    {
        //give us a(1) shoe (ie shield from dying) shoe only stays for a while and stops all enemies for a while
        //if shoes complementary colour comes in contact the shoe goes away but we dont
        //if we jump in both analogoues then we get 2 shoes which stays for longer.

        if (thatColor == analogous[1])
        {
            //change colour of left shoe to analogous[1]
        }
        else if (thatColor == analogous[2])
        {
            //change colour of left shoe to analogous[2]
        }
    }
    else if (darkerThan.Contains(thatColor))
    {
        //*no effect -- that.rigidbody.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
    }
    else if (thatColor == complementary)
    {
        if (name == "Bob")
        {
            playerCollider.enabled = false;
            playerRenderer.enabled = false;

            StartCoroutine(RespawnAfterDelay(1f));
        }
    }

    // split complementary gives a shoe which can be killed off by jumping on analogous. the shoe would be worn in reverse
    // that shoe reverses our controls. timer um anagane indakkiya mathi 2 halves, oru half il oru shoe, mattethil matte shoe
    // or gravity shift to whichever side the shoe is on. randu shoe um undenki alternate cheyth kalikkum

    // rectangle -- pothapp karanam pothapp rectangle anallo. 4 points of the rectangle collect cheytha mathi. 

    // triadic -- thoppi or bamboo copter

    // square -- gravity shift 
}

private Vector3 initialPosition;
private Renderer playerRenderer;
private Collider2D playerCollider;

private void Start() 
{

    playerCollider = GetComponent<Collider2D>();
    playerRenderer = GetComponent<Renderer>();
    
    initialPosition = transform.position;
}

private IEnumerator RespawnAfterDelay(float delay)
{
    yield return new WaitForSeconds(delay);

    transform.position = initialPosition;
    playerCollider.enabled = true;
    playerRenderer.enabled = true;

}
}

