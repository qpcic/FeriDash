using UnityEngine;
using TMPro;

public class LevelPause : MonoBehaviour {
    public TMP_Text jumpsText;
    // public TMP_Text attemptsText;
    public TMP_Text timeText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jumpsText.text = $"Jumps: {PlayerPrefs.GetInt("Jumps", 1)}";
        timeText.text = $"Time: {(int)(PlayerPrefs.GetFloat("Time", 1))}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
