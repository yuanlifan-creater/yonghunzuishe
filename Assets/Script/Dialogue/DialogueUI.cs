using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance;

    [Header("UI Elements")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public Image npcPortrait;
    public Image playerPortrait;
    public TextMeshProUGUI speakerNameText;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ShowDialogue(bool show)
    {
        dialoguePanel.SetActive(show);
    }

    public void SetDialogue(string text, bool isNPCSpeaking, string speakerName, Sprite npcSprite = null)
    {
        dialogueText.text = text;
        speakerNameText.text = speakerName;

        // ����ͷ��λ��
        npcPortrait.gameObject.SetActive(isNPCSpeaking);
        playerPortrait.gameObject.SetActive(!isNPCSpeaking);

        // ����NPCͷ��
        if (isNPCSpeaking && npcSprite != null)
        {
            npcPortrait.sprite = npcSprite;
        }
    }
}
