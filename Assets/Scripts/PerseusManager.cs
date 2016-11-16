using UnityEngine;
using System.Collections;
using VolumetricLines;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.UI;

public class PerseusManager : MonoBehaviour {

    public GameObject[] puzzles;
    public static bool[] puzzleCheck = { false, false, false, false, false, false, false, false, false };

    public GameObject[] rocksToExplode;

    public Text companionText;

    static bool glideTextDone = false;
    bool storyElementText = false;

    public string[] flavourText;
    bool canFlavourText = false;

    public GameObject puzzleComplete;
    bool newText;

	// Use this for initialization
	void Start () {
        puzzleComplete.SetActive(false);

        companionText.text = "";

        GameObject.Find("PuzzleYellow").GetComponent<Puzzle>().enabled = false;

        foreach (GameObject i in rocksToExplode)
        {
            i.GetComponent<Rigidbody>().isKinematic = true;
        }

        StartCoroutine("FlavourText", 5.0f);
    }
	
	// Update is called once per frame
	void Update () {

        for (int i = 0; i < puzzles.Length; i++)
        {
            if(puzzles[i].GetComponent<Puzzle>().puzzleComplete == true)
            {
                puzzleCheck[i] = true;
                if (newText)
                {
                    newText = false;
                    Debug.Log("True");
                    //puzzleComplete.SetActive(true);
                    Invoke("ResetText", 3f);
                }
            }
        }

        if (puzzleCheck[0])
        {
            newText = true;
        }

        if (puzzleCheck[1])
        {
            newText = true;

            ThirdPersonUserControl tempControl = GameObject.Find("ThirdPersonController").GetComponent<ThirdPersonUserControl>();
            tempControl.canFloat = true;

            if (glideTextDone == false)
            {
                StartCoroutine("GlideUnlocked", 5.0f);
            }
        }

        if (puzzleCheck[2])
        {
            newText = true;
        }

        if (puzzleCheck[3])
        {
            newText = true;
        }

        if (puzzleCheck[4])
        {
            newText = true;

            GameObject.Find("PuzzleYellow").GetComponent<Puzzle>().enabled = true;
        }

        if (puzzleCheck[5])
        {
            newText = true;
        }

        if (puzzleCheck[6])
        {
            newText = true;

            foreach (GameObject i in rocksToExplode)
            {
                i.GetComponent<Rigidbody>().isKinematic = false;
            }
        }

        if (puzzleCheck[7])
        {
            newText = true;
        }

        if (puzzleCheck[8])
        {
            newText = true;
        }

        if (canFlavourText)
        {
            StartCoroutine("FlavourText", 60.0f);
        }
    }

    IEnumerator GlideUnlocked(float waitTime)
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
    }

    IEnumerator FlavourText(float resetTime)
    {
        if (storyElementText == false)
        {
            canFlavourText = false;
            companionText.text = flavourText[Random.Range(0, flavourText.Length)];
            yield return new WaitForSeconds(5);
            companionText.text = "";
            yield return new WaitForSeconds(resetTime);

            canFlavourText = true;
        }
    }

    public void ResetText()
    {
        puzzleComplete.SetActive(false);
    }
}
