using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "对话", menuName = "对话/NPC文本")]
public class DialogueData : ScriptableObject
{
    public List<DialogueLine> lines = new List<DialogueLine>();
}

// DialogueLine.cs
[System.Serializable]
public class DialogueLine
{
    [TextArea(3, 10)]
    public string text;
    public bool isPlayerSpeaking;
}