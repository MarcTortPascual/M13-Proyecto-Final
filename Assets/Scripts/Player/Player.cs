using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    [Range(0,1000)]
    private float speed;
    [SerializeField]
    private float jumpForce;
    private float WallImpulse = 1.5f;
    private bool canjumpy,jumped,canjumpx,grabed;
  
    [SerializeField]
    private LayerMask groundLayer;
    private float coyote_time;

    public float Speed {
        get{
            return speed;
        }
        set{
            speed = Mathf.Max(value,0);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float direction = Input.GetAxis("Horizontal");
        
        GetComponent<SpriteRenderer>().flipX = direction<0;

        GetComponent<Rigidbody2D>().linearVelocityX = (Speed*Time.deltaTime)*direction;
        //TODO:print
        Debug.DrawRay(transform.position,-transform.up*0.75f,Color.red,0);
        //TODO:print
        Debug.DrawRay(transform.position,new Vector3((float)(Math.Truncate(direction)*0.45f), 0,0),Color.green,0);
        RaycastHit2D hitGround = Physics2D.Raycast(transform.position, -transform.up, 0.75f, groundLayer);
        RaycastHit2D hitwall = Physics2D.Raycast(transform.position, new Vector3((float)Math.Truncate(direction), 0,0), 0.45f, groundLayer);
        canjumpy = hitGround.collider;
        if (hitwall.collider){
            canjumpx = hitwall.collider.gameObject.tag == "Pared";
        }else{
            canjumpx = false;
        }
        if (canjumpx){
            //TODO:print
            Debug.Log("pared");
        }else{
            //TODO:print
            Debug.Log("nopared");
        }
       
     
        if(!canjumpy){
            coyote_time += Time.deltaTime;
        }else{
            coyote_time = 0;
            jumped = false;
        }
        if(Input.GetKeyDown(KeyCode.Space) && coyote_time <= 1 && !jumped && !grabed){
            GetComponent<Rigidbody2D>().linearVelocityY=jumpForce;
            jumped = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && grabed){
                GetComponent<Rigidbody2D>().linearVelocity = new Vector2(jumpForce*direction,jumpForce*WallImpulse);
                grabed = false;
           }
        if(canjumpx){
           grabed = true;
           
        }
        if (grabed){
            GetComponent<Rigidbody2D>().linearVelocityY=0;
            GetComponent<Rigidbody2D>().linearVelocityX=0;
        }

        

    }
}
