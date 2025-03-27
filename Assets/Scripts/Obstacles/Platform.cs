using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Platform : MonoBehaviour
{

    public List<Rail> route;
    int actualRail = 0;
    Rail ra;
    public float vel = 0.33f;
    private int directon = 1;
    float pos = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (pos>=1){
            actualRail += directon;
            if (actualRail >= route.Count-1){
                directon = -1;
            }else if (actualRail == 0){
                directon = 1;
            }
            pos= 0;
        }
        ra = route[actualRail];
        var posRa= ra.transform.position;
        switch(ra.direction){
            case Directions.RIGHT:
                transform.position = Vector2.Lerp(
                    new Vector2(posRa.x-(0.5f*directon),posRa.y),
                    new Vector2(posRa.x+(0.5f*directon),posRa.y),
                    pos
                );
                break;
            case Directions.UP:
                transform.position = Vector2.Lerp(
                    new Vector2(posRa.x,posRa.y-(0.5f*directon)),
                    new Vector2(posRa.x,posRa.y+(0.5f*directon)),
                    pos
                );
                break;
            case Directions.UP_RIGHT:
             transform.position = Vector2.Lerp(
                    new Vector2(posRa.x-(0.5f*directon),posRa.y-(0.5f*directon)),
                    new Vector2(posRa.x+(0.2f*directon),posRa.y+(0.2f*directon)),
                    pos
                );
                break;
            case Directions.UP_LEFT:
             transform.position = Vector2.Lerp(
                    new Vector2(posRa.x+(0.5f*directon),posRa.y-(0.5f*directon)),
                    new Vector2(posRa.x-(0.2f*directon),posRa.y+(0.2f*directon)),
                    pos
                );
                break;



        }
        pos += vel*Time.deltaTime;
    }
    void OnTriggerEnter2D (Collider2D col){
        if (col.gameObject.CompareTag("Player")){
            col.gameObject.transform.parent = this.transform;
        }
    }
     void OnTriggerExit2D (Collider2D col){
        if (col.gameObject.CompareTag("Player")){
            col.gameObject.transform.parent = null;
        }
    }
}
