using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class LevelPause : MonoBehaviour {
    public TMP_Text jumpsText;
    public TMP_Text attemptsText;
    public TMP_Text timeText;
    public Slider progressBar;
    public TextMeshProUGUI progressText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jumpsText.text = $"Jumps: {PlayerPrefs.GetInt("Jumps", 1)}";
        attemptsText.text = $"Attempts: {PlayerPrefs.GetInt("Attempts", 1)}";
        timeText.text = $"Time: {(int)(PlayerPrefs.GetFloat("Time", 1))}";
        float progress = PlayerPrefs.GetFloat("LevelProgress", 1);
        int percent = Mathf.RoundToInt(progress * 100f);
        progressBar.value = Mathf.Clamp01(progress);
        progressText.text = percent + "%";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
