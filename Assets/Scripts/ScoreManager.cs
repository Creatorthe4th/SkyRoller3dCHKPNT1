using UnityEngine;
using TMPro; // If you use legacy UI Text instead, swap TMP_Text for UnityEngine.UI.Text

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public Transform player;
    public TMP_Text scoreText;

    float maxZ;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (player == null) return;

        if (player.position.z > maxZ)
            maxZ = player.position.z;

        if (scoreText != null)
            scoreText.text = "Distance: " + Mathf.FloorToInt(maxZ) + " m";
    }

    public string GetScoreString()
    {
        return Mathf.FloorToInt(maxZ) + " m";
    }
}
