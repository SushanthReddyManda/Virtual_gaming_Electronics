
using UnityEngine;

public class end_trigger : MonoBehaviour
{
    public game__manager gamemanager;

    public void OnTriggerEnter(Collider other)
    
        
           

            {
                Debug.Log("works1");
                gamemanager.Completelevel();
            }

        
    

}
