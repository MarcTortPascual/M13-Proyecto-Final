using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Player target;
    private Vector2 dest,ori;
    float lerpPos = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dest = target.transform.position;
        ori = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (lerpPos >= 1){
            Destroy(gameObject);
        }else{
            lerpPos += 1f*Time.deltaTime;
            transform.position = Vector2.Lerp(ori,dest,lerpPos);
        }
    }
    void OnTriggerEnter2D (Collider2D col){
        if (col.CompareTag("Player")){
            col.GetComponent<Player>().canmove = false;
            col.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-10,6);
            Destroy(gameObject);
          
        }
         
    }
}
