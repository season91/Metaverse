using UnityEngine;
using UnityEngine.SceneManagement;

public class GameZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            if (transform.name == "StackGame")
            {
                GameManager.Instance.isWaveGamePlaying = true;
                GameManager.Instance.StartGame();
            }
            else if (transform.name == "FlappyGame")
            {
                GameManager.Instance.EnterMiniGameZone();
            }
        }
            
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.ExitMiniGameZone();
        }
    }
}
