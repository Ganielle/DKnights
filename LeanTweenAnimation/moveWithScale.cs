using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveWithScale : MonoBehaviour
{
    [SerializeField]private Vector3 guiPos, guiScale;
    [SerializeField]private float showTime, showDelay, scaleDelay;
    [SerializeField]private LeanTweenType easeType;
    [SerializeField]private bool withScale;

    LTDescr leantweenLT;


    public void moveGUI()
    {
        leantweenLT = LeanTween.moveX(this.GetComponent<RectTransform>(), guiPos.x, showTime).setEase(easeType);
        if(this.GetComponent<RectTransform>().localPosition.x >= guiPos.x)
        leantweenLT.setOnComplete(stopLeanTween);
        
        if(withScale)
        {
            leantweenLT.setOnComplete(scaleGUI);
        }
    }

    private void scaleGUI()
    {
        leantweenLT = LeanTween.scale(GetComponent<RectTransform>(),guiScale, scaleDelay).setEase(easeType);
        leantweenLT.setOnComplete(stopLeanTween);
    }

    private void stopLeanTween()
    {
        leantweenLT.cancel(this.gameObject);
    }
}
