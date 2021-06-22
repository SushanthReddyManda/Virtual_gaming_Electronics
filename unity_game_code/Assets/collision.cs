using UnityEngine;

public class collision: MonoBehaviour
{

    public aurdino moment;

    // Update is called once per frame
    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "cylinder")
        {
            moment.enabled = false;
            FindObjectOfType<game__manager>().EndGame();
        }
        
    }
}
