using Unity.VisualScripting;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform point;
    public Sprite check,uncheck;
    private bool bcheck = false;    

    // Update is called once per frame
    void Update()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = bcheck?check:uncheck;
    }
    void OnTriggerEnter2D (Collider2D col){
        if (col.CompareTag("Player")){
            if(!bcheck){
                bcheck = true;
                point.position = transform.position;

            }
        }
    }
 }
