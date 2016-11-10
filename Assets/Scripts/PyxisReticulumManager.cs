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
        companionText.text = "";
        StartCoroutine("Companion", 5.0f);
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
        if (PlayerPrefs.GetInt("TutorialDone") == 0)
        {
            companionText.text = "";
            yield return new WaitForSeconds(1.5f);
            companionText.text = "It is finally you... \n The one we have been waiting for.";
            yield return new WaitForSeconds(waitTime);
            companionText.text = "We haven't much time. \n Take this Prism and head down the ramp.";
            yield return new WaitForSeconds(waitTime);
            companionText.text = "";
        }
    }

    IEnumerator CompanionTwo(float waitTime)
    {
        companionText.text = "";
        yield return new WaitForSeconds(waitTime);
        companionText.text = "Only you can use this Prism. \n With it you can manipulate elements of light.";
        yield return new WaitForSeconds(waitTime);
        companionText.text = "Go forth. Test its power. Understand it.";
        yield return new WaitForSeconds(waitTime);
        companionText.text = "Unlock the long-locked gates to Perseus!";
        yield return new WaitForSeconds(waitTime);
        companionText.text = "";
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("PLAYER ENTERED");
            StartCoroutine("CompanionTwo", 5.0f);
        }
    }
}