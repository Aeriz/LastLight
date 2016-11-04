using UnityEngine;
using System.Collections;
using VolumetricLines;

public class PerseusManager : MonoBehaviour {

    public GameObject[] puzzles;
    public static bool[] puzzleCheck = { false, false, false, false, false, false, false, false };

	// Use this for initialization
	void Start () {
	
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
    }
}
