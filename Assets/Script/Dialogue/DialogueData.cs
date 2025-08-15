using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "�Ի�", menuName = "�Ի�/NPC�ı�")]
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