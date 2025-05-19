using System;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] [Range(0,1000)] private float speed;
    [SerializeField] private float jumpForce;
    public TMP_Text coinsTxt,timeTxt;
    public Sprite stunned, normal;
    private float wallImpulse = 1.5f;
    private float direction;
    public float Direction {get{
        return direction;
    }}
    public bool canjumpy,jumped,canjumpx,grabed,inVine,impuling,canmove,jumping;
    public Transform viewPoint, spawner;
    [SerializeField] private LayerMask groundLayer;
    private float flyTime;
    public double stuned_time = 1.5;
    private double stuned_time_counter = 0;
    private float level_time = 0;
    public float Level_time
    {
        get
        {
            return level_time;
        }
    }
    public float Speed {
        get{
            return speed;
        }
        set{
            speed = Mathf.Max(value,0);
            speed = Mathf.Min(value,1000);
        }
    }
    private int coins = 0;
    public int Coins {
        get{
            return coins;
        }
        set{
            this.coins = value;
        }
    }
    void Start()
    {
      transform.position = spawner.position;
     
      canmove = true;
    }
    void move(){
        GetComponent<SpriteRenderer>().flipX = direction<0;
        GetComponent<Rigidbody2D>().linearVelocityX = (Speed*Time.deltaTime)*direction;
       
    }
    // Update is called once per frame
    void Update()
    {
        level_time += Time.deltaTime;
        timeTxt.SetText(level_time.ToString("N2"));
        GetComponent<SpriteRenderer>().sprite = canmove?normal:stunned;
        coinsTxt.text = coins.ToString();
        if(transform.parent){
            GetComponent<Rigidbody2D>().gravityScale = transform.parent.CompareTag("Vine") ? 0:1;
            direction = transform.parent.CompareTag("Vine") ? 0:Input.GetAxis("Horizontal");
        }else{
            GetComponent<Rigidbody2D>().gravityScale = 1; 
            direction = Input.GetAxis("Horizontal");
        }
        Camera.main.transform.position = viewPoint.position;
        if (canmove){
            move();
        }else{
            stuned_time_counter += Time.deltaTime;
            if (stuned_time_counter>stuned_time){
                stuned_time_counter  = 0;
                canmove = true;
            }
        }
        RaycastHit2D hitGround = Physics2D.Raycast(transform.position, -transform.up, 0.75f, groundLayer);
        RaycastHit2D hitwall = Physics2D.Raycast(transform.position, new Vector3((float)Math.Truncate(direction), 0,0), 0.45f, groundLayer);
        canjumpy = hitGround.collider;
        if (hitwall.collider){
            canjumpx = hitwall.collider.gameObject.tag == "Pared";
            inVine = hitwall.collider.gameObject.tag == "Vine";
        }else{
            canjumpx = false;
        }
        if(!canjumpy){
            flyTime += Time.deltaTime;
        }else{
            flyTime = 0;
            jumped = false;
        }
        if(Input.GetKeyDown(KeyCode.Space) && flyTime <= 0.3 && !jumped && !grabed){
            GetComponent<Rigidbody2D>().linearVelocityY=jumpForce;
            jumping = true;
            jumped = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && grabed){
                jumping = true;
                hitwall = Physics2D.Raycast(transform.position, new Vector2((GetComponent<SpriteRenderer>().flipX?-1f:1f), 0f), 0.45f, groundLayer);
                if (!hitwall){
                    GetComponent<Rigidbody2D>().linearVelocity = new Vector2(jumpForce*direction,jumpForce*wallImpulse);
                    
                }
                grabed = false;
        }
        if(transform.parent && Input.GetKeyDown(KeyCode.Space) ){
           if (transform.parent.CompareTag("Vine")){
                var _vel =transform.parent.GetComponent<Vine>().Vel;
               
                GetComponent<Rigidbody2D>().AddForce(new Vector2(_vel*6,0),ForceMode2D.Impulse);
                transform.position += new Vector3(transform.GetComponent<BoxCollider2D>().size.x* _vel>0?1:-1,0,0);
                transform.parent = null;
            }
        }
        if(canjumpx){
           grabed = true;
           
        }
        if (grabed ){
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }
    }
}
