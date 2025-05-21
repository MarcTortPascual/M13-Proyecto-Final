using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
   
    void OnTriggerEnter2D (Collider2D col){
        if(col.CompareTag("Player")){
            col.GetComponent<Player>().Coins  += 1;
            Destroy(gameObject);
        }
    }
}
