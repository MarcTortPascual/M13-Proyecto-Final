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
    private int direction = 1;
    float pos = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canonicalRoute = new List<Rail>(route);
    }
    void update_track(){
        
        route = new List<Rail>(canonicalRoute);
        List<Rail> rt = route;
        while (rt[rt.Count-1] is RailSwicht)
        {
            RailSwicht sw = rt[rt.Count-1] as RailSwicht;
            rt = new List<Rail>(sw.direction == Directions.SWICHT_UP?sw.route_up:sw.route_down);
            route.AddRange(rt);
        }
    }
    
    void Update()
    {
        List<Rail> back;
        if (pos>=1 || pos<=0){

            actualRail  += direction;
            if (direction == 1){
                pos = 0;
            }else{
                pos = 1;
            }
            
        }
        if (actualRail >= route.Count){
            
            direction = -1;
        }
        if (actualRail < 0){
            
            direction = 1;
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
            case Directions.DOWN:
                transform.position = Vector2.Lerp(
                    new Vector2(posRa.x,posRa.y),
                    new Vector2(posRa.x,posRa.y-1f),
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
            case Directions.DOWN_RIGHT:
             transform.position = Vector2.Lerp(
                    new Vector2(posRa.x,posRa.y),
                    new Vector2(posRa.x+1f,posRa.y-1f),
                    pos
                );
                break;
            case Directions.DOWN_LEFT:
                transform.position = Vector2.Lerp(
                    new Vector2(posRa.x,posRa.y),
                    new Vector2(posRa.x-1f,posRa.y-1f),
                    pos
                );
                break;
            case Directions.SWICHT_UP:
                back = route;
                update_track();
                if (!route.Contains(ra)){
                    route = back;
                }
                transform.position = Vector2.Lerp(
                    new Vector2(posRa.x,posRa.y),
                    new Vector2(posRa.x+0.75f,posRa.y+0.5f),
                    pos
                );
                break;
            case Directions.SWICHT_DOWN:
                back = route;
                update_track();
                if (!route.Contains(ra)){
                    route = back;
                }
              
                transform.position = Vector2.Lerp(
                    new Vector2(posRa.x,posRa.y),
                    new Vector2(posRa.x+0.75f,posRa.y-0.5f),
                    pos
                );
                break;



        }
        
        pos += vel*Time.deltaTime*direction;
      
       
        
       
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
