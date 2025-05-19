using UnityEngine;

public class Belt : MonoBehaviour
{
    [Range(-1,1)]
    public int direction = 1;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void OnTriggerStay2D(Collider2D col){
            if (col.CompareTag("Player")){
               bool cm = col.GetComponent<Player>().canmove;
                if (cm){
                    col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2( 750*direction,0));
                    }
            }
        }
}
