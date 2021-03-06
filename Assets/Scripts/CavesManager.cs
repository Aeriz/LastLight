﻿using UnityEngine;
using System.Collections;
using VolumetricLines;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.UI;

public class CavesManager : MonoBehaviour {

    public GameObject[] puzzles;
    public static bool[] puzzleCheck = { false, false, false };

    public string[] flavourText;
    bool canFlavourText = false;
    bool storyElementText = false;
    bool lightsOnText = false;

    public Text companionText;
    public GameObject puzzleCompleteText;

    public Light blueLight;

    public Vector3 spawnLocOne;
    public Vector3 spawnLocTwo;

    public GameObject player;

    public SaveGame load;
    // Use this for initialization
    void Start()
    {
        if (PlayerPrefs.GetInt("CaveSpawn") == 1)
        {
            player.transform.position = spawnLocOne;
        }
        else if (PlayerPrefs.GetInt("CaveSpawn") == 2)
        {
            player.transform.position = spawnLocTwo;
        }

        if (PlayerPrefs.GetInt("CanGlide") == 1)
        {
            ThirdPersonUserControl tempControl = GameObject.Find("ThirdPersonController").GetComponent<ThirdPersonUserControl>();
            tempControl.canFloat = true;
        }

        GameObject.Find("PuzzleSeven").GetComponent<Puzzle>().enabled = false;
        GameObject.Find("PuzzleEight").GetComponent<Puzzle>().enabled = false;

        puzzleCompleteText.SetActive(false);

        companionText.text = "";

        StartCoroutine("FlavourText", 60.0f);

        load.GameSave();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(storyElementText);

        for (int i = 0; i < puzzles.Length; i++)
        {
            if (puzzles[i].GetComponent<Puzzle>().puzzleCompleteText == true)
            {
                puzzleCompleteText.SetActive(true);
                Invoke("ResetText", 3f);
            }

            if (puzzles[i].GetComponent<Puzzle>().puzzleComplete == true)
            {
                puzzleCheck[i] = true;
            }
        }

        if (puzzleCheck[0])
        {
            if (blueLight.intensity < 0.5)
            {
                blueLight.intensity += 0.01f;
            }

            if (PlayerPrefs.GetInt("LightsOnTextDone") == 0)
            {
                StartCoroutine("LightsOn", 5f);
            }

            GameObject.Find("PuzzleSeven").GetComponent<Puzzle>().enabled = true;
            GameObject.Find("PuzzleEight").GetComponent<Puzzle>().enabled = true;
        }

        if (puzzleCheck[1])
        {

        }

        if (puzzleCheck[2])
        {
        }

        if (canFlavourText)
        {
            StartCoroutine("FlavourText", 60.0f);
        }
    }

    /*IEnumerator GlideUnlocked(float waitTime)
    {
        storyElementText = true;
        glideTextDone = true;

        companionText.text = "";
        companionText.text = "Fantastic! With the completion of this puzzle \n you have been granted wings of flight.";
        yield return new WaitForSeconds(waitTime);
        companionText.text = "You can now glide by holding your \n jump for a period of time.";
        yield return new WaitForSeconds(waitTime);
        companionText.text = "Go on. Give it a try!";
        yield return new WaitForSeconds(waitTime);
        companionText.text = "";

        storyElementText = false;
    }*/

    IEnumerator LightsOn(float waitTime)
    {
        PlayerPrefs.SetInt("LightsOnTextDone", 1);
        storyElementText = true;

        companionText.text = "";
        companionText.text = "It seems completion of this puzzle has \n restored some power to these ancient grounds.";
        yield return new WaitForSeconds(waitTime);

        storyElementText = false;
    }

    IEnumerator FlavourText(float resetTime)
    {
        if (storyElementText == false)
        {
            canFlavourText = false;
            companionText.text = flavourText[Random.Range(0, flavourText.Length)];
            yield return new WaitForSeconds(5);
            if (storyElementText == false)
            {
                companionText.text = "";
            }
            
            yield return new WaitForSeconds(resetTime);

            canFlavourText = true;
        }
    }

    void ResetText()
    {
        puzzleCompleteText.SetActive(false);
    }
}
