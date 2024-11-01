using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreathSystem : MonoBehaviour
{

    
    public static BreathSystem instance;

    public GameObject character;
    public GameObject breathUI;
    public Slider breathSlider;

    public float maxBreathCapacity;
    public float currentBreathCapacity;
    public float breathCapacityMultiplier; //passive skill utk menahan laju penurunan napas + mempercepat pengambilan napas
    
    

    public int breathAddition; //rate penambahan napas
    public int breathSubtraction; //rate penurunan napas

    public bool hadAccessToAir; //cek apakah sedang bisa bernapas

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentBreathCapacity = maxBreathCapacity;
        breathSlider.maxValue = maxBreathCapacity;
        breathSlider.value = currentBreathCapacity;
    }

    public void OnTriggerStay2D(Collider2D other)
    {

      

        if(other.tag == "Water")
        {
            if(hadAccessToAir != true)
            {
                Debug.Log("Didalam Air");
                BreathDecrease(breathSubtraction);

                
            }

        }

        if (other.tag == "Air")
        {
            Debug.Log("Diudara");
            hadAccessToAir = true;

          BreathIncrease(breathAddition);
        }
    }


    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Air")
        {
            Debug.Log("Menyelam");
            hadAccessToAir = false;

           
        }
    }

        
    public void BreathDecrease(int amount)
    {

        if (breathUI.activeSelf != true)
        {
            breathUI.SetActive(true);
        }

        if (currentBreathCapacity > 0)
        {
            currentBreathCapacity -= amount * Time.deltaTime / breathCapacityMultiplier;
            breathSlider.value = currentBreathCapacity;
        }

        else
        {
            Debug.Log("Start Respawn Mechanism");
        }
    }

    public void BreathIncrease(int amount)
    {
        if (currentBreathCapacity < maxBreathCapacity)
        {
            currentBreathCapacity += amount * Time.deltaTime * breathCapacityMultiplier;
            breathSlider.value = currentBreathCapacity;
        }

        else
        {
            breathUI.SetActive(false);
        }
    }
}
