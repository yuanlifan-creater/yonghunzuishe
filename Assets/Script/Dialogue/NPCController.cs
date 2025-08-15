
using System.Collections.Generic;
using UnityEngine;
using yhzs.Inventory;

public class NPCController : MonoBehaviour
{
    [Header("NPC基本设置")]
    public string npcName;            // NPC名称
    public Sprite npcPortrait;        // NPC头像
    public DialogueData firstMeetingDialogue; // 初次见面对话
    public string[] basicDialogues;    // 基础对话
    public string refuseDialogue = "你难道就没有比和我说话更重要的事做吗？"; // 拒绝对话
    public int maxDialogueCount = 5;   // 最大对话次数

    public GameObject interactionHint; // 交互提示
    
    private bool playerInRange;
    private int dialogueCount;
    private bool hasMet;
    private bool isInDialogue; // 标记是否正在对话中
    [SerializeField] private bool isCanTrade;
    private bool isOpenShop;
   [Header("商人基本数据")]
    public InventoryBag_SO shopData;
    private bool isBasicDialogueActive; // 标记当前是否处于基础对话
    void Start()
    {
        // 加载保存状态
        // hasMet = PlayerPrefs.GetInt(npcName + "_HasMet", 0) == 1;
        // dialogueCount = PlayerPrefs.GetInt(npcName + "_DialogueCount", 0);
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            interactionHint.SetActive(true);
            
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            interactionHint.SetActive(false);          
        }
    }

    void Update()
    {
        // 只有在没有对话中时才允许触发新对话
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !isInDialogue&&!isOpenShop)
        {
           

                DialogueManager.instance.dialoguePanel.SetActive(true);
           
            StartDialogue();
        }
        if (isInDialogue  && isCanTrade && isBasicDialogueActive&&!DialogueManager.instance.isTyping && Input.GetKeyDown(KeyCode.E)&&!isOpenShop)
        {
            isOpenShop = true;

            OpenShop();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isOpenShop = false;
            EventHandler.CallBagCloseEvent(SlotType.NpcBag, shopData);
        }
    }

    void StartDialogue()
    {
        // 标记为正在对话中
        isInDialogue = true;
        isBasicDialogueActive = false;

        dialogueCount++;
        PlayerPrefs.SetInt(npcName + "_DialogueCount", dialogueCount);

        if (dialogueCount > maxDialogueCount)
        {
            DialogueManager.instance.opneShop.SetActive(false);
            // 拒绝对话
            DialogueLine refuseLine = new DialogueLine
            {
                text = refuseDialogue,
                isPlayerSpeaking = false
            };

            DialogueData refuseData = ScriptableObject.CreateInstance<DialogueData>();
            refuseData.lines = new List<DialogueLine> { refuseLine };

            DialogueManager.instance.StartDialogue(refuseData, this);
            return;
        }

        if (!hasMet)
        {
           
            // 初次见面对话
            DialogueManager.instance.StartDialogue(firstMeetingDialogue, this);
            
        }
        else
        {
            isBasicDialogueActive = true;
            // 基础对话
            string randomDialogue = basicDialogues[Random.Range(0, basicDialogues.Length)];

            DialogueLine basicLine = new DialogueLine
            {
                text = randomDialogue,
                isPlayerSpeaking = false
            };

            DialogueData basicData = ScriptableObject.CreateInstance<DialogueData>();
            basicData.lines = new List<DialogueLine> { basicLine };



            
            DialogueManager.instance.StartDialogue(basicData, this);
        }
    }
   

    void OpenShop()
    {
        DialogueManager.instance.CloseDialoguePanel();
        EventHandler.CallBagOpenEvent(SlotType.NpcBag, shopData);
        OnDialogueEnd();
    }
    public void OnDialogueEnd()
    {
        // 标记为已见面
        if (!hasMet)
        {
            hasMet = true;
            PlayerPrefs.SetInt(npcName + "_HasMet", 1);
        }

        // 对话结束，重置状态
        isInDialogue = false;
        isBasicDialogueActive = false;
        DialogueManager.instance.opneShop.SetActive(true);
        // 移除事件监听
       
    }
}