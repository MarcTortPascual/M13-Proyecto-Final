using Unity.VisualScripting;
using UnityEngine;

public class BeltReversible : Belt, ISwichable
{
    public Sprite dis;
    public Sprite ena;
    
    
    void Start()
    {
        direction = -1;
    }
    
    public void Switch(bool state)
    {
        GetComponent<SpriteRenderer>().sprite = state?ena:dis;
        direction = state?1:-1;
    }
}
