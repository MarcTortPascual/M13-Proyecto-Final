using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Scripting;

public class Lever : MonoBehaviour
{

    private bool state;
    
    public MonoBehaviour target;
    public List<Lever> linkedLevers;
    public Sprite dis;
    public Sprite ena;
    private bool isplayer;

   
    // Update is called once per frame
    void Update()
    {
        GetComponent<SpriteRenderer>().sprite = state?ena:dis;
        if(isplayer){
            print("ho");
            if (Input.GetKeyDown(KeyCode.E) ){
                state = !state;
                foreach (var lever in linkedLevers){
                    lever.state = state;
                }
                if (target is ISwichable){
                    ISwichable e = target as ISwichable;
                    e.Switch(state);
                }
               
            }
        }
    }
    void OnTriggerEnter2D(Collider2D col){
       if (col.CompareTag("Player")){
            isplayer = true;
       }
        
    }
    void OnTriggerExit2D(Collider2D col){
        if (col.CompareTag("Player")){
            isplayer = false;
        }
    }
}
