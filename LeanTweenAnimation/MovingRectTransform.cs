using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingRectTransform : MonoBehaviour
{
    
    [SerializeField]public Vector3 guiPos;
    [SerializeField]private float showTime, showDelay;
    [SerializeField]private LeanTweenType easeType;
    LTDescr leantweenLT;

    public void moveRectTransform()
    {
        leantweenLT = LeanTween.moveX(this.GetComponent<RectTransform>(), guiPos.x, showTime).setDelay(showDelay).setEase(easeType);
        leantweenLT.setOnComplete(stopLeanTween);
    }

    private void stopLeanTween()
    {
        leantweenLT.cancel(this.gameObject);
    }

}
