using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class KeyboardGUIBattle : MonoBehaviour
{
    [Header("Text area")]
    [SerializeField] public TMP_InputField textArea;
    [SerializeField] public int caretPos;

    private void Start()
    {
        caretPos = 0;
    }

    private void Update()
    {
        showAlwaysCaret();
    }

    private void showAlwaysCaret()
    {
        if (textArea.text == "")
            caretPos = 0;


        if(EventSystem.current.currentSelectedGameObject != textArea.gameObject)
        {
            textArea.caretPosition = caretPos;

            textArea.GetType().GetField("m_AllowInput", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(textArea, true);
            textArea.GetType().InvokeMember("SetCaretActive", BindingFlags.NonPublic | BindingFlags.InvokeMethod | BindingFlags.Instance, null, textArea, null);
        }
        else if(EventSystem.current.currentSelectedGameObject == textArea.gameObject)
        {
            caretPos = textArea.caretPosition;
        }
    }
}
