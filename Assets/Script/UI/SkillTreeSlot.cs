using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class SkillTreeSlot : MonoBehaviour,IPointerClickHandler,IPointerExitHandler,IPointerEnterHandler
{
    public bool unlocked;
    private UI ui;
    public UnityEvent OnUnlocked;
    [SerializeField] private SkillTreeSlot[] shouldlUnLocked;
    [SerializeField] private SkillTreeSlot[] shouldLocked;
    [SerializeField] private Image skillImage;
    [SerializeField] private Color lockedSkillColor;
    [SerializeField] private int costPlayerAbility;

    [SerializeField] private string skillName;
    [TextArea]
    [SerializeField] private string skillDescription;






    private void OnValidate()
    {
        gameObject.name = "SkillTree" + skillName; 
    }

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => UnlockSkillSlot());
        ui = GetComponentInParent<UI>();
        skillImage = GetComponent<Image>();
        skillImage.color = Color.red;
       

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void UnlockSkillSlot()
    {
        if (PlayerManager.instance.HaveEnoughMoney(costPlayerAbility) == false)
            return;
       
        for (int i = 0; i <shouldlUnLocked.Length; i++)
        {
            if (shouldlUnLocked[i].unlocked == false)
            {
                return;
            }
        }

        for (int i = 0; i < shouldLocked.Length; i++)
        {
            if (shouldLocked[i].unlocked == true)
            {
                return;
            }
           
        }
        
        unlocked = true;
        OnUnlocked.Invoke();
        skillImage.color = Color.white;



    }

    public void OnPointerClick(PointerEventData eventData)
    {
       
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.skillToolTip.HideToolTip();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.skillToolTip.ShowToolTip(skillDescription, skillName);
        Vector2 mousePosition = Input.mousePosition;

        ui.skillToolTip.transform.position = new Vector2(mousePosition.x, mousePosition.y + 50);
    }
}
