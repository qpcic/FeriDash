using UnityEngine;
using TMPro;

public class TimerDisplay : MonoBehaviour
{
    public TextMeshProUGUI timerText;  // referenca na UI tekst

    private float startTime;

    void Start()
    {
        startTime = Time.time;  // čas začetka levela
    }

    void Update()
    {
        float t = Time.time - startTime;  // koliko sekund je minilo
        timerText.text = t.ToString("F2") + " s";  // prikaže 2 decimalni mesti
    }
}
