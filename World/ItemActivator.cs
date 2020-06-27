using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemActivator : MonoBehaviour
{
    [SerializeField]private float distanceFromPlayer;
    [SerializeField]private GameObject player;
    [HideInInspector]public List<ActivatorItem> activatorItems;
    private IEnumerator m_courotine = null;
    void Awake(){
        activatorItems = new List<ActivatorItem>();
    }

    void Update(){

        if(m_courotine != null) return;

        m_courotine = CheckActivation();
        StartCoroutine(m_courotine);
    }

    public void Register(ActivatorItem item){
        activatorItems.Add(item);
    }

    public void deRegister(ActivatorItem item){
        activatorItems.Remove(item);
    }

    private IEnumerator CheckActivation(){

        List<ActivatorItem>removeList = new List<ActivatorItem>();

        if(activatorItems.Count > 0f){
            
            foreach(ActivatorItem item in activatorItems){

                if(Vector3.Distance(player.transform.position, item.itemPos) > distanceFromPlayer){
                    
                    if(item.item == null){

                        removeList.Add(item);
                    }
                    else{
                        
                        item.item.SetActive(false);
                    }
                }
                else{
                    if(item.item == null){

                        removeList.Add(item);
                    }
                    else{

                        item.item.SetActive(true);
                    }
                }
            }
        }

        yield return new WaitForSeconds(0.01f);

        if(removeList.Count > 0f){

            foreach(ActivatorItem item in removeList){
                activatorItems.Remove(item);
            }
        }

        yield return new WaitForSeconds(0.01f);

        m_courotine = null;
    }
}

public class ActivatorItem{

    public GameObject item;
    public Vector3 itemPos;
}