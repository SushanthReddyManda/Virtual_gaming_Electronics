
using UnityEngine;

public class player_movement : MonoBehaviour
{
    public Rigidbody rb;

    public float forwardforce = 2000f;
    public float sidewaysforce = 50f;

    // Update is called once per frame
    void FixedUpdate ()
    {
        rb.AddForce(0, 0, forwardforce * Time.deltaTime);
        if(Input.GetKey("d"))
        {
            rb.AddForce(sidewaysforce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
        if (Input.GetKey("a"))
        {
            rb.AddForce(-sidewaysforce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
        if(rb.position.y<-1)
        {
            FindObjectOfType<game_manager>().EndGame();
        }
    }
}
