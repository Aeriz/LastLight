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
    public ThirdPersonUserControl player;

    // Use this for initialization
    void Start ()
    {
        player = GetComponent<ThirdPersonUserControl>();
        currentStamina = baseStamina;
        currentMana = baseMana;
    }
	
	// Update is called once per frame
	void LateUpdate ()
    {
        staminaSlider.value = currentStamina;
        manaSlider.value = currentMana;
        if (currentStamina < baseStamina)
        {
            currentStamina += Time.deltaTime * 4;
        }
        else if(currentStamina > baseStamina)
        {
            currentStamina = baseStamina;
        }

        if (currentMana < baseMana)
        {
            currentMana += Time.deltaTime * 2;
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

        if(currentStamina < 30)
        {
            player.canDash = false;
        }
        else if(currentStamina >= 30)
        {
            player.canDash = true;
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
