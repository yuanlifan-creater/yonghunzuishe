using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillToolTip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI skillText;
    [SerializeField] private TextMeshProUGUI skillName;
    public void ShowToolTip(string _skillDescription,string _skillName)
    {
        skillText.text = _skillDescription;
        skillName.text = _skillName;
        gameObject.SetActive(true);
    }

    public void HideToolTip()
    {
        
        gameObject.SetActive(false);
    }
}
