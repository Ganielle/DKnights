using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardButtons : MonoBehaviour
{
    [Header("TextArea")]
    [SerializeField] private TMP_InputField textArea;
    [SerializeField] private float delayDeleteButtonTime;
    [SerializeField] private float deleteTextTimeRate;

    [Header("Script Reference")]
    [SerializeField] private KeyboardGUIBattle kbGUI;

    bool isDelete;

    public void TextButton(string text)
    {
        textArea.text = textArea.text.Insert(textArea.caretPosition, text);
    }

    private void Update()
    {
        if (isDelete)
            InvokeRepeating("DeleteText", delayDeleteButtonTime, deleteTextTimeRate);
        else
            CancelInvoke("DeleteText");
    }

    public void DeleteText()
    {
        if (kbGUI.caretPos == 0)
            return;

        textArea.text = textArea.text.Remove(kbGUI.caretPos - 1);
        kbGUI.caretPos = textArea.caretPosition;
    }

    public void DeleteButtonDown()
    {
        isDelete = true;
    }

    public void DeleteButtonUp() 
    {
        isDelete = false;
    }
}
