using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{

    
    public GameObject[] gameUI;
    public SkillToolTip skillToolTip;
    public TextMeshProUGUI money;
    private bool newState;
    // Start is called before the first frame update
    void Update()
    {
        OpenUI();
        money.text = PlayerManager.instance.player.playerMoney.ToString();
        
    }

    
    public void TogglePanel(int targetIndex)
    {
        if (targetIndex < 0 || targetIndex >= gameUI.Length) return;

       
        bool newState = !gameUI[targetIndex].activeSelf;
        gameUI[targetIndex].SetActive(newState);

      
        if (newState)
        {
            CloseOtherPanels(targetIndex);
        }
    }

    
    private void CloseOtherPanels(int exceptionIndex)
    {
        for (int i = 0; i < gameUI.Length; i++)
        {
            if (i != exceptionIndex && gameUI[i].activeSelf)
            {
                gameUI[i].SetActive(false);
            }
        }
    }

    private void OpenUI()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {

            TogglePanel(1);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {

            TogglePanel(0);
        }

    }

    public void OpenSkillTree()
    {
        

            TogglePanel(1);
        
    }

    public void OpenBag()
    {
        

            TogglePanel(0);
        
    }

}
