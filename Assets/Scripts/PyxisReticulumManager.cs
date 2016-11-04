using UnityEngine;
using System.Collections;
using VolumetricLines;

public class PyxisReticulumManager : MonoBehaviour
{
    public GameObject gateCol;
    public Animator anim;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Puzzle").GetComponent<Puzzle>().puzzleComplete == true)
        {
            gateCol.SetActive(false);
            anim.SetBool("GateRight", true);
        }
    }
}