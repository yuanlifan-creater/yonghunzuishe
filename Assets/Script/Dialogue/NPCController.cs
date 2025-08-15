
using System.Collections.Generic;
using UnityEngine;
using yhzs.Inventory;

public class NPCController : MonoBehaviour
{
    [Header("NPC��������")]
    public string npcName;            // NPC����
    public Sprite npcPortrait;        // NPCͷ��
    public DialogueData firstMeetingDialogue; // ���μ���Ի�
    public string[] basicDialogues;    // �����Ի�
    public string refuseDialogue = "���ѵ���û�бȺ���˵������Ҫ��������"; // �ܾ��Ի�
    public int maxDialogueCount = 5;   // ���Ի�����

    public GameObject interactionHint; // ������ʾ
    
    private bool playerInRange;
    private int dialogueCount;
    private bool hasMet;
    private bool isInDialogue; // ����Ƿ����ڶԻ���
    [SerializeField] private bool isCanTrade;
    private bool isOpenShop;
   [Header("���˻�������")]
    public InventoryBag_SO shopData;
    private bool isBasicDialogueActive; // ��ǵ�ǰ�Ƿ��ڻ����Ի�
    void Start()
    {
        // ���ر���״̬
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
        // ֻ����û�жԻ���ʱ���������¶Ի�
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
        // ���Ϊ���ڶԻ���
        isInDialogue = true;
        isBasicDialogueActive = false;

        dialogueCount++;
        PlayerPrefs.SetInt(npcName + "_DialogueCount", dialogueCount);

        if (dialogueCount > maxDialogueCount)
        {
            DialogueManager.instance.opneShop.SetActive(false);
            // �ܾ��Ի�
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
           
            // ���μ���Ի�
            DialogueManager.instance.StartDialogue(firstMeetingDialogue, this);
            
        }
        else
        {
            isBasicDialogueActive = true;
            // �����Ի�
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
        // ���Ϊ�Ѽ���
        if (!hasMet)
        {
            hasMet = true;
            PlayerPrefs.SetInt(npcName + "_HasMet", 1);
        }

        // �Ի�����������״̬
        isInDialogue = false;
        isBasicDialogueActive = false;
        DialogueManager.instance.opneShop.SetActive(true);
        // �Ƴ��¼�����
       
    }
}