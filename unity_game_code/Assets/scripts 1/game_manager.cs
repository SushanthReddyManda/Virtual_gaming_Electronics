
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class game_manager : MonoBehaviour
{
    public float restartdelay = 3f;
    public GameObject completelevelUI;

    public void Completelevel()
    {
        completelevelUI.SetActive(true);
    }

    bool gamehasended = false;
    public void EndGame()
    {
        if (gamehasended == false)
        {
            gamehasended = true;
            Debug.Log("GAME OVER");
            Invoke("Restart", restartdelay);
        }

        
    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
