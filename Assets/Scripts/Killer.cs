using Unity.VisualScripting;
using UnityEngine;

public class Killer : MonoBehaviour
{
    public Player target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col){
        if (col.CompareTag("Player")){
            col.GetComponent<Player>().canmove = true;
            col.GetComponent<Player>().transform.position = col.GetComponent<Player>().spawner.position;
        }
    }
}
