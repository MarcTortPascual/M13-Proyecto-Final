using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D (Collider2D col){
        if(col.CompareTag("Player")){
            col.GetComponent<Player>().Coins  += 1;
            Destroy(gameObject);
        }
    }
}
