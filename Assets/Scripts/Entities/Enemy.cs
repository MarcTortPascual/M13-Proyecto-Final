using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int distRange;
    public Player target;
    private Vector2 posPla,pos;
    public GameObject bull;
    private bool moving = false;
    public double shoot_interval;
    private double shoot_time = 0;
    double CalculateTargetDist(){
        return Math.Sqrt((Math.Abs(posPla.x - pos.x) * Math.Abs(posPla.x - pos.x)) + (Math.Abs(posPla.y - pos.y) * Math.Abs(posPla.y - pos.y)));
    }
    // Update is called once per frame
    void Update()
    {
        shoot_time += Time.deltaTime;
        posPla = target.transform.position;
        pos = transform.position;
        GetComponent<SpriteRenderer>().flipX = (pos.x - posPla.x) < 0.0f? false : true;
        moving = CalculateTargetDist() < distRange;
        if (moving){
            if (shoot_time>shoot_interval){
                var bullet = Instantiate(bull,transform.position+new Vector3(1*((pos.x - posPla.x) < 0.0f?1:-1),0,0),transform.rotation);
                bullet.GetComponent<Bullet>().target = target;
                shoot_time = 0;
            }
        
        }
    }
}

