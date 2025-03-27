using System;
using Unity.VisualScripting;
using UnityEngine;

public class PhantomBlock : MonoBehaviour
{

    public Sprite[] states = new Sprite[5];

    private float time_in;
    private bool player_in = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
        time_in += player_in?Time.deltaTime:Time.deltaTime*-1;
        time_in = Math.Max(time_in,0);
        GetComponent<SpriteRenderer>().sprite = states[(int)time_in%5];

        GetComponent<BoxCollider2D>().enabled = !((int)time_in == 4);
    }
   void OnTriggerEnter2D (Collider2D col){
    player_in = col.gameObject.CompareTag("Player");
   }
   void OnTriggerExit2D (Collider2D col){
    player_in = !col.gameObject.CompareTag("Player");
   }
}
