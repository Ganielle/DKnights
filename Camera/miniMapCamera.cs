using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class miniMapCamera : MonoBehaviour
{
    public Transform player;
    [SerializeField]private bool isRotate = false;
    [SerializeField]private bool revertFogState;

    private void Awake(){
        revertFogState = RenderSettings.fog;
    }

    void LateUpdate(){
        Vector3 newPos = player.position;
        newPos.y = transform.position.y;
        transform.position = newPos;
        if(isRotate){
            transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
        }
    }

    private void OnPreRender() {
        RenderSettings.fog = false;
    }

    private void OnPostRender() {
        RenderSettings.fog = revertFogState;
    }

}
