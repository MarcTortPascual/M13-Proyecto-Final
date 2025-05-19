using System;

using UnityEngine;


public class Vine : MonoBehaviour
{
    private float vel = 0;
    public float Vel {
        get{
            return vel;
        }
    }

    private Player player;
    private bool is_player = false;
    private Rigidbody2D rg ;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        rg.linearVelocity = Vector2.zero;
       
    }

    // Update is called once per frame
    void Update()
    {
        
        
        
        if (is_player){
            player.gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            var t = transform.position;
            player.transform.position = new Vector3(t.x,t.y,t.z) ;
            if (Input.GetKey("d")){
          
                rg.AddRelativeForceX(10);
                vel += Time.deltaTime;
            }
            if (Input.GetKey("a")){
                rg.AddRelativeForceX(-10);
                vel += Time.deltaTime;
            }
            if (Input.GetKeyDown(KeyCode.Space)){
                is_player = false;
                player.transform.position += transform.right * player.Direction;
                player.GetComponent<Rigidbody2D>().linearVelocity = GetComponent<Rigidbody2D>().linearVelocity;
                player.GetComponent<Rigidbody2D>().linearVelocityX *= vel;
                
            }
            
            
        }else{
            vel = 0;
        }
        
    }
    void OnTriggerEnter2D(Collider2D col){
        
        if (col.CompareTag("Player")){
            rg.linearVelocity = col.GetComponent<Rigidbody2D>().linearVelocity;
            col.transform.parent = transform;
            is_player = true;
            player = col.GetComponent<Player>();
            
        }
    }

     void OnTriggerExit2D(Collider2D col){
        if (col.CompareTag("Player")){
             col.transform.parent = null;
             col.transform.rotation = Quaternion.identity;
            is_player = false;
            player = null;
        }
    }
}
