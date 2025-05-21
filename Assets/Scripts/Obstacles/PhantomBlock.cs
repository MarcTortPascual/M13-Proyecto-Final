using System;
using Unity.VisualScripting;
using UnityEngine;

public class PhantomBlock : MonoBehaviour
{

    public Sprite[] states = new Sprite[5];

    private float time_in;
    private bool player_in = false;
    public float change_time = 0.05f;
    private float change_interval = 0;
    private int state = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
        if (change_interval < change_time){
            change_interval += Time.deltaTime;
        }else{
            state += player_in?1:0;
            
            change_interval = 0;
        }
        GetComponent<SpriteRenderer>().sprite = states[state%5];

        GetComponent<BoxCollider2D>().enabled = !((int)(state%5) == 4);
    }
   void OnTriggerEnter2D (Collider2D col){
    player_in = col.gameObject.CompareTag("Player");
   }
   void OnTriggerExit2D (Collider2D col){
    player_in = !col.gameObject.CompareTag("Player");
    state = 0;
   }
}
