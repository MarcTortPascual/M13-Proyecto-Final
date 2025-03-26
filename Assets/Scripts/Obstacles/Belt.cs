using UnityEngine;

public class Belt : MonoBehaviour
{
    [Range(-1,1)]
    public int direction = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    void OnTriggerStay2D(Collider2D col){
            if (col.CompareTag("Player")){
                
                print(direction * 750);
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2( 750*direction,0.01f));

                
            }
        }
}
