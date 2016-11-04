using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public enum State
{
    KillQuest, CollectQuest, CourierQuest
}
public class QuestManager : MonoBehaviour {

    public string[] questText;
    public string[] thankYouText;

    public State[] state;
    public string[] questGiver;

    public static bool[] questStatus = { true, true, true };
    public static bool[] questActive = { false, false, false };
    public static bool onQuest = false;

    public static int[] questGoal = { 5, 1, 1 };
    int[] questTracker = { 0, 0, 0 };

    public GameObject[] exclamationMark;

    public Text questSpeech;

    public Button questAccept;

    int giver;

	// Use this for initialization
	void Start () {
        questSpeech.gameObject.SetActive(false);
        questAccept.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        if (questActive[0]) // Kill quest
        {

        }

        if (questActive[1]) // Collect quest
        {

        }

        if (questActive[2]) // Courier quest
        {

        }

	    if (onQuest)
        {
            if (questTracker[giver] >= questGoal[giver])
            {
                Debug.Log("Quest Complete");
                onQuest = false;

                questSpeech.gameObject.SetActive(true);
                questSpeech.text = "You have completed the quest. \n Congratulations!";

                Invoke("Reset", 3);
            }
        }
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == questGiver[0] || col.gameObject.tag == questGiver[1] || col.gameObject.tag == questGiver[2])
        {
            if (onQuest == false)
            {
                foreach (int i in questTracker)
                {
                    questTracker[i] = 0;
                }

                for (int i = 0; i < questGiver.Length; i++)
                {
                    giver = i;
                    if (questGiver[i] == col.gameObject.tag)
                    {
                        break;
                    }
                }

                Debug.Log(giver);

                if (col.gameObject.tag == questGiver[giver])
                {
                    questSpeech.gameObject.SetActive(true);

                    if (questStatus[giver] == true)
                    {
                        questSpeech.text = questText[giver];
                        questAccept.gameObject.SetActive(true);
                        Cursor.visible = true;
                    }
                    else
                    {
                        questSpeech.text = thankYouText[Random.Range(0, thankYouText.Length)];
                    }
                }
            }

            if (onQuest && col.gameObject.tag == "TalkQuestReq")
            {
                questGoal[giver] += 1;
                col.gameObject.tag = null;
            }
        }     
    }

    void OnTriggerExit()
    {
        questSpeech.gameObject.SetActive(false);
        questAccept.gameObject.SetActive(false);
        Cursor.visible = false;
    }

    public void Quest()
    {
        questActive[giver] = true;
        Cursor.visible = false;
        onQuest = true;

        questSpeech.gameObject.SetActive(false);
        questAccept.gameObject.SetActive(false);

        exclamationMark[giver].SetActive(false);
        Debug.Log("quest " + giver + " accepted.");
    }

    public void Reset()
    {
        questSpeech.gameObject.SetActive(false);
    }
}
