using TMPro;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public AudioSource buttonClickSound;

    public Sprite[] availableSkins; // Assign in Inspector
    public SpriteRenderer targetRenderer; // The character preview or real player

    public TMP_Text text1;
    public TMP_Text text2;
    public TMP_Text text3;

    public TMP_Text AttemptsText;
    public TMP_Text JumpsText;

    private const string SkinKey = "SelectedSkin";
    private int currentSkinIndex = 0;

    void Start()
    {
        int savedSkinIndex = PlayerPrefs.GetInt(SkinKey, 0);
        SelectSkin(savedSkinIndex, playSound: false);

        int att = PlayerPrefs.GetInt("TotalAttempts", 0);
        int jmp = PlayerPrefs.GetInt("TotalJumps");
        AttemptsText.text = "Attempts: " + att;
        JumpsText.text = "Jumps: " + jmp;
    }

    // 🔁 UI GUMBI KLIČEJO TO METODO
    public void SelectSkin(int index)
    {
        SelectSkin(index, playSound: true);
    }

    // 🔧 Dejanska logika
    private void SelectSkin(int index, bool playSound)
    {
        text1.color = Color.black;
        text2.color = Color.black;
        text3.color = Color.black;

        currentSkinIndex = index;

        if (playSound)
            PlayClickSound();

        if (currentSkinIndex == 0)
            text1.color = Color.white;
        else if (currentSkinIndex == 1)
            text2.color = Color.white;
        else if (currentSkinIndex == 2)
            text3.color = Color.white;

        targetRenderer.sprite = availableSkins[index];
        Debug.Log("Selected skin index (preview only): " + index);
    }

    public void ApplySelectedSkin()
    {
        PlayClickSound();

        PlayerPrefs.SetInt(SkinKey, currentSkinIndex);
        PlayerPrefs.Save();
        Debug.Log("Saved selected skin: " + currentSkinIndex);
    }

    private void PlayClickSound()
    {
        if (buttonClickSound != null)
            buttonClickSound.Play();
        else
            Debug.LogWarning("buttonClickSound ni nastavljen!");
    }
}
