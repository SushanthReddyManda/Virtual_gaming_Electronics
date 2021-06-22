
using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public

    // Update is called once per frame
    void Update()
    {


        transform.position = player.position + offset;
        
    }
    
}
