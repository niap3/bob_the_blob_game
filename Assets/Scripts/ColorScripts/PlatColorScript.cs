using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatColorScript : MonoBehaviour
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

// public float bounceForce = 10f;

private void OnCollisionEnter2D(Collision2D that)
{
    //should i kill just the lifeforms or the platform as well?
    string thatColor = that.gameObject.tag;
    string thisColor = gameObject.tag;
    
    int idx = allColors.IndexOf(thatColor);
    // string complementary = colorRelations[idx, idx];
    // string[] analogous = {colorRelations[idx + 1, idx], colorRelations[idx + 2, idx]};
    string[] lighterThan = {colorRelations[idx + 3, idx], colorRelations[idx + 4, idx]};
    // string[] darkerThan = {colorRelations[idx + 5, idx], colorRelations[idx + 6, idx]};

    if (lighterThan.Contains(thisColor))
    {
        //*sinks off like goop after like a second window
        //what about bomb platforms though - can have them you just slightly bounce up from the bomb platforms.
        //the smoke after explotion will be of the composites.if you get hit by complementary smoke you die 

        StartCoroutine(DisappearAndReappear());
    }

}

private Renderer platformRenderer;
private Collider2D platformCollider;

private void Awake()
{
    platformRenderer = GetComponent<Renderer>();
    platformCollider = GetComponent<Collider2D>();
}

private IEnumerator DisappearAndReappear()
{
    yield return new WaitForSeconds(1f);

    platformRenderer.enabled = false;
    platformCollider.enabled = false;

    yield return new WaitForSeconds(4f);

    platformRenderer.enabled = true;
    platformCollider.enabled = true;

}

}

