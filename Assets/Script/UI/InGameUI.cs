using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private GameObject b;
    private Slider slider;
    private bool a=false;
    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
        
    }
    void Start()
    {

        if (playerStats != null)
            playerStats.onHealthChanged += UpdateHealtuUI;
    }

    // Update is called once per frame
    void Update()
    {

       
        
            if (Input.GetKeyDown(KeyCode.V))
            {
                b.SetActive(false);
                a = true;

            }
        
        
        
          
    }

    private void UpdateHealtuUI()
    {
        slider.maxValue = playerStats.GetMaxHealthValue();
        slider.value = playerStats.currentHealthy;
    }

    private void CloseHealth()
    {
        
       
    }



}
