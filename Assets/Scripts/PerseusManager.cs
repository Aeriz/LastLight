using UnityEngine;
using System.Collections;
using VolumetricLines;

public class PerseusManager : MonoBehaviour {

    public GameObject[] puzzles;
    public static bool[] puzzleCheck = { false, false, false, false, false, false, false, false };

    public static bool glideUnlock = false;

    public GameObject[] rocksToExplode;

	// Use this for initialization
	void Start () {
        GameObject.Find("PuzzleYellow").GetComponent<Puzzle>().enabled = false;

        foreach (GameObject i in rocksToExplode)
        {
            i.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
	
	// Update is called once per frame
	void Update () {

        for (int i = 0; i < puzzles.Length; i++)
        {
            if(puzzles[i].GetComponent<Puzzle>().puzzleComplete == true)
            {
                puzzleCheck[i] = true;
            }
        }

        if (puzzleCheck[1])
        {
            glideUnlock = true;
        }

        if (puzzleCheck[4])
        {
            GameObject.Find("PuzzleYellow").GetComponent<Puzzle>().enabled = true;
        }

        if (puzzleCheck[6])
        {
            foreach (GameObject i in rocksToExplode)
            {
                i.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }
}
