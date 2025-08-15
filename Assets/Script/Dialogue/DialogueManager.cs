// DialogueManager.cs (放在UI场景)
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    // UI组件
    public GameObject dialoguePanel;
    public Image npcPortrait;        // NPC头像
    public Image playerPortrait;     // 玩家头像
    public TMP_Text npcNameText;     // NPC名字文本
    public TMP_Text playerNameText;  // 玩家名字文本
    public GameObject npcNameBox;    // NPC名字框
    public GameObject playerNameBox; // 玩家名字框
    public TMP_Text dialogueText;   // 对话文本
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

        // 重置当前NPC引用
        if (currentNPC != null)
        {
            currentNPC.OnDialogueEnd();
            currentNPC = null;
        }

        // 清除事件监听
        OnDialogueLineComplete = null;
    }

    public void StartDialogue(DialogueData data, NPCController npc)
    {
        // 防止重复开始对话
        if (isDialogueActive) return;

        currentNPC = npc;
        dialogueQueue.Clear();

        foreach (var line in data.lines)
        {
            dialogueQueue.Enqueue(line);
        }

        // 设置NPC头像
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
        // 如果正在打字，先停止
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

        // 隐藏所有名字框和头像
        npcNameBox.SetActive(false);
        playerNameBox.SetActive(false);
        npcPortrait.gameObject.SetActive(false);
        playerPortrait.gameObject.SetActive(false);

        // 设置说话者UI
        if (line.isPlayerSpeaking)
        {
            // 玩家说话
            playerPortrait.gameObject.SetActive(true);
            playerNameBox.SetActive(true);
        }
        else
        {
            // NPC说话
            npcPortrait.gameObject.SetActive(true);
            npcNameBox.SetActive(true);

            // 设置NPC名字
            if (currentNPC != null)
            {
                npcNameText.text = currentNPC.npcName;
            }
        }

        // 开始逐字打印
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
            yield return new WaitForSeconds(0.02f); // 调整打印速度
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

        
            
       

        // 进入下一句
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 如果正在打字，先完成当前句
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