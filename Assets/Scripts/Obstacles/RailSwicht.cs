using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RailSwicht : Rail,ISwichable
{
    public Sprite dis;
    public Sprite ena;

    public List<Rail> route_up;
    public List<Rail> route_down;
    void Start (){
        direction = Directions.SWICHT_DOWN;
    }
    public void Switch(bool state)
    {
        GetComponent<SpriteRenderer>().sprite = state?ena:dis;
        direction = state?Directions.SWICHT_UP:Directions.SWICHT_DOWN;
    }
}
