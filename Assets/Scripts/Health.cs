using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class Health : MonoBehaviour
{
    public int baseHealth = 100;
    public int currenthealth;
    public Slider healthSlider;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColours = new Color(1f, 0f, 0f, 0.1f);
    bool isDead = false;
    bool damaged;
    ThirdPersonCharacter playerMove;
    ThirdPersonUserControl playerControl;
    

	// Use this for initialization
	void Start ()
    {
        playerMove = GetComponent<ThirdPersonCharacter>();
        playerControl = GetComponent<ThirdPersonUserControl>();
        currenthealth = baseHealth;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(damaged)
        {
            damageImage.color = flashColours;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
	}

    public void takeDamage(int damage)
    {
        if (!playerControl.dash)
        {
            damaged = true;
            currenthealth -= damage;
            healthSlider.value = currenthealth;
            if (currenthealth <= 0 && !isDead)
            {
                Death();
            }
        }
    }

    public void Death()
    {
        //play death animation here
        isDead = true;
        playerControl.enabled = false;
        playerMove.enabled = false;

    }
}
