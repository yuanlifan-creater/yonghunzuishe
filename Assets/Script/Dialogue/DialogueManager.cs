// DialogueManager.cs (����UI����)
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    // UI���
    public GameObject dialoguePanel;
    public Image npcPortrait;        // NPCͷ��
    public Image playerPortrait;     // ���ͷ��
    public TMP_Text npcNameText;     // NPC�����ı�
    public TMP_Text playerNameText;  // ��������ı�
    public GameObject npcNameBox;    // NPC���ֿ�
    public GameObject playerNameBox; // ������ֿ�
    public TMP_Text dialogueText;   // �Ի��ı�
    public GameObject opneShop;
    private Queue<DialogueLine> dialogueQueue = new Queue<DialogueLine>();
    private NPCController currentNPC;
    private bool isDialogueActive;
    public bool isTyping { get; private set; }
    public bool isOpenShop;
    private Coroutine typingCoroutine;
    private string currentSentence;
    
    public event Action OnDialogueLineComplete;
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void CloseDialoguePanel()
    {
        dialoguePanel.SetActive(false);
        isDialogueActive = false;

        // ���õ�ǰNPC����
        if (currentNPC != null)
        {
            currentNPC.OnDialogueEnd();
            currentNPC = null;
        }

        // ����¼�����
        OnDialogueLineComplete = null;
    }

    public void StartDialogue(DialogueData data, NPCController npc)
    {
        // ��ֹ�ظ���ʼ�Ի�
        if (isDialogueActive) return;

        currentNPC = npc;
        dialogueQueue.Clear();

        foreach (var line in data.lines)
        {
            dialogueQueue.Enqueue(line);
        }

        // ����NPCͷ��
        if (currentNPC.npcPortrait != null)
        {
            npcPortrait.sprite = currentNPC.npcPortrait;
        }

        dialoguePanel.SetActive(true);
        isDialogueActive = true;
        DisplayNextLine();
    }

    void DisplayNextLine()
    {
        // ������ڴ��֣���ֹͣ
        if (isTyping && typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            isTyping = false;
            
        }

        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine line = dialogueQueue.Dequeue();

        // �����������ֿ��ͷ��
        npcNameBox.SetActive(false);
        playerNameBox.SetActive(false);
        npcPortrait.gameObject.SetActive(false);
        playerPortrait.gameObject.SetActive(false);

        // ����˵����UI
        if (line.isPlayerSpeaking)
        {
            // ���˵��
            playerPortrait.gameObject.SetActive(true);
            playerNameBox.SetActive(true);
        }
        else
        {
            // NPC˵��
            npcPortrait.gameObject.SetActive(true);
            npcNameBox.SetActive(true);

            // ����NPC����
            if (currentNPC != null)
            {
                npcNameText.text = currentNPC.npcName;
            }
        }

        // ��ʼ���ִ�ӡ
        typingCoroutine = StartCoroutine(TypeText(line.text));
    }

    IEnumerator TypeText(string text)
    {
        
        isTyping = true;
        dialogueText.text = "";
        currentSentence = text;

        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.02f); // ������ӡ�ٶ�
        }

        isTyping = false;
        
        OnDialogueLineComplete?.Invoke();
    }

    public void SkipTyping()
    {
        if (isTyping && typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = currentSentence;
            isTyping = false;
            OnDialogueLineComplete?.Invoke();
        }
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        isDialogueActive = false;

        if (currentNPC != null)
        {
            currentNPC.OnDialogueEnd();
            currentNPC = null;
        }
        OnDialogueLineComplete?.Invoke();
    }

    void Update()
    {
        if (!isDialogueActive) return;

        
            
       

        // ������һ��
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // ������ڴ��֣�����ɵ�ǰ��
            if (isTyping)
            {
                SkipTyping();
            }
            else
            {
                DisplayNextLine();
            }
        }
    }
}