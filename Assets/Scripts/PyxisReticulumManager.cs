using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using VolumetricLines;

public class PyxisReticulumManager : MonoBehaviour
{
    public GameObject gateCol;
    public Animator anim;
    public Text companionText;

    // Use this for initialization
    void Start()
    {
        StartCoroutine("Companion", 10.0f);
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

    IEnumerator Companion(float waitTime)
    {
        companionText.text = "test test test";
        yield return new WaitForSeconds(waitTime);
        companionText.text = "test2 test2 test2";
    }
}