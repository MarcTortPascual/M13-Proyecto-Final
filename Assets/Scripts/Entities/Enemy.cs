using System;
using System.Threading;
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


   // private float enemy_lerp = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    double CalculateTargetDist(){
        
        return Math.Sqrt((Math.Abs(posPla.x - pos.x) * Math.Abs(posPla.x - pos.x)) + (Math.Abs(posPla.y - pos.y) * Math.Abs(posPla.y - pos.y)));

    }
    double CalculateTargetDistNeg(){
        
        return Math.Sqrt(((posPla.x - pos.x) * (posPla.x - pos.x)) + ((posPla.y - pos.y) * (posPla.y - pos.y)));

    }
    void Start()
    {
        
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
                //bullet.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(5*((pos.x - posPla.x) < 0.0f?1:-1),5*(target.GetComponent<SpriteRenderer>().flipX?-1:1));
                shoot_time = 0;
            }
            
        
        }



        
    }
}

