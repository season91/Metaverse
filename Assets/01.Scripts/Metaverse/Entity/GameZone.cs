using UnityEngine;
using UnityEngine.SceneManagement;

public class GameZone : MonoBehaviour
{
    public GameObject popupUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            popupUI.SetActive(true);
        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            popupUI.SetActive(false);
            int score = PlayerPrefs.GetInt("FlappyGameScore", 0);
            Debug.Log($"미니게임 점수: {score}");

        }
    }
}
