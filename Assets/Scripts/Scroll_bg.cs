using UnityEngine;

public class Scroll_bg : MonoBehaviour
{
    public Player target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
