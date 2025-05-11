using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class LevelProgress : MonoBehaviour
{
    public Transform player;
    public Transform levelEnd;
    public Slider progressBar;
    public TextMeshProUGUI progressText;
    public bool levelComplete = false;

    private float totalDistance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        totalDistance = Vector3.Distance(player.position, levelEnd.position);
    }

    // Update is called once per frame

    void Update()
    {
        if (levelComplete)
        {
            progressBar.value = 1f;
            progressText.text = "100%";
            return;
        }
        float distancePassed = Vector3.Distance(player.position, levelEnd.position);
        float progress = 1f - (distancePassed / totalDistance);
        progressBar.value = Mathf.Clamp01(progress);
        int percent = Mathf.RoundToInt(progress * 100f);
        progressText.text = percent + "%";
        PlayerPrefs.SetFloat("LevelProgress", progress);
    }

    public void CompleteLevel()
    {
        levelComplete = true;
        
    }
}
