using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // swap for UnityEngine.UI.Text if you use legacy UI

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("References")]
    public Transform player;
    public GameObject gameOverPanel;
    public TMP_Text finalScoreText;

    [Header("Lose Condition")]
    public float fallThreshold = -5f; // player loses if they drop below this Y

    [Header("Scenes")]
    public string menuSceneName = "MainMenu";

    bool isGameOver;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (isGameOver || player == null) return;

        if (player.position.y < fallThreshold)
            GameOver();
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StopMusic();
            AudioManager.Instance.PlayDeath();
        }

        PlayerMovement pm = player != null ? player.GetComponent<PlayerMovement>() : null;
        if (pm != null) pm.StopAndDisable();

        if (finalScoreText != null && ScoreManager.Instance != null)
            finalScoreText.text = "You traveled " + ScoreManager.Instance.GetScoreString();

        if (gameOverPanel != null) gameOverPanel.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(menuSceneName);
    }
}