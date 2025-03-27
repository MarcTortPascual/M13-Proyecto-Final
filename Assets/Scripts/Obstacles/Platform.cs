using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Platform : MonoBehaviour
{

    public List<Rail> route;
    private List<Rail> canonicalRoute;
    int actualRail = 0;
    private RailSwicht rs;
    Rail ra;
    public float vel = 0.33f;
    private int directon = 1;
    float pos = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canonicalRoute = new List<Rail>(route);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (pos>=1 || pos<=0){

            actualRail  += directon;
            if (directon == 1){
                pos = 0;
            }else{
                pos = 1;
            }
            
        }
        if (actualRail > route.Count){
            directon = -1;
        }
        if (actualRail < 0){
            directon = 1;
        }
        ra = route[actualRail];
        var posRa= ra.transform.position;
        switch(ra.direction){
            case Directions.RIGHT:
                transform.position = Vector2.Lerp(
                    new Vector2(posRa.x,posRa.y),
                    new Vector2(posRa.x+1f,posRa.y),
                    pos
                );
                break;
            case Directions.UP:
                transform.position = Vector2.Lerp(
                    new Vector2(posRa.x,posRa.y),
                    new Vector2(posRa.x,posRa.y+1f),
                    pos
                );
                break;
            case Directions.UP_RIGHT:
             transform.position = Vector2.Lerp(
                    new Vector2(posRa.x,posRa.y),
                    new Vector2(posRa.x+1f,posRa.y+1f),
                    pos
                );
                break;
            case Directions.UP_LEFT:
             transform.position = Vector2.Lerp(
                    new Vector2(posRa.x,posRa.y),
                    new Vector2(posRa.x-1f,posRa.y+1f),
                    pos
                );
                break;
            case Directions.SWICHT_UP:
                rs = ra as RailSwicht; //si tine esta direcion asuminos que es un descvio
                route = new List<Rail>(canonicalRoute);

                route.AddRange(rs.route_up);
                transform.position = Vector2.Lerp(
                    new Vector2(posRa.x,posRa.y),
                    new Vector2(posRa.x+0.75f,posRa.y+0.5f),
                    pos
                );
                break;
            case Directions.SWICHT_DOWN:
                rs = ra as RailSwicht; //si tine esta direcion asuminos que es un descvio
                route = new List<Rail>(canonicalRoute);

                route.AddRange(rs.route_down);
                transform.position = Vector2.Lerp(
                    new Vector2(posRa.x,posRa.y),
                    new Vector2(posRa.x+0.75f,posRa.y-0.5f),
                    pos
                );
                break;



        }
        
        pos += vel*Time.deltaTime*directon;
      
       
        
       
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
