using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuestManager : MonoBehaviour {

    public string[] questText;
    public string[] thankYouText;
    public string[] questGiver;

    public static bool[] questStatus = { true, true, true };
    public static bool[] questActive = { false, false, false };
    public static bool onQuest = false;

    public static int[] questGoal = { 0, 0, 0 };
    int[] questTracker = { 0, 0, 0 };

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
	    if (onQuest)
        {
            if (questTracker[giver] >= questGoal[giver])
            {
                Debug.Log("Quest Complete");
                onQuest = false;
            }
        }
	}

    void OnTriggerEnter(Collider col)
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
    }
}
