
using UnityEngine;
using UnityEngine.SceneManagement;




public class level_complete : MonoBehaviour
{
    public void LoadNextLevel()
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
}
