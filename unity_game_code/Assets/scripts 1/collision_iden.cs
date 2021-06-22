
using UnityEngine;

public class collision_iden : MonoBehaviour
{

    public aurdino moment;

    // Update is called once per frame
    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "obstacle")
        {
            moment.enabled = false;
            FindObjectOfType<game_manager>().EndGame();
        }
        if (collisionInfo.collider.tag == "cylinder")
        {
            moment.enabled = false;
            FindObjectOfType<game_manager>().EndGame();
        }
    }
}
