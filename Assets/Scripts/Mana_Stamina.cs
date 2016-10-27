using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;


public class Mana_Stamina : MonoBehaviour
{
    public float baseStamina = 100;
    public float currentStamina;
    public Slider staminaSlider;
    public float baseMana = 100;
    public float currentMana;
    public Slider manaSlider;
    ThirdPersonUserControl player;

    // Use this for initialization
    void Start ()
    {
        player = GetComponent<ThirdPersonUserControl>();
        currentStamina = baseStamina;
        currentMana = baseMana;
    }
	
	// Update is called once per frame
	void Update ()
    {
        staminaSlider.value = currentStamina;
        manaSlider.value = currentMana;
        if (currentStamina < baseStamina)
        {
            currentStamina += Time.deltaTime;
        }
        else if(currentStamina > baseStamina)
        {
            currentStamina = baseStamina;
        }

        if (currentMana < baseMana)
        {
            currentMana += Time.deltaTime / 2;
        }
        else if (currentMana > baseMana)
        {
            currentMana = baseMana;
        }

        if(player.dashStamina)
        {
            useStamina(30);
            player.dashStamina = false;
        }

    }
    
    public void useMana(float cost)
    {
        currentMana -= cost;
    }

    public void useStamina(float cost)
    {
        currentStamina -= cost;
    }
}
