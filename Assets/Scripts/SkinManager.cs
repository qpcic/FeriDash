using TMPro;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public Sprite[] availableSkins; // Assign in Inspector
    public SpriteRenderer targetRenderer; // The character preview or real player

    public TMP_Text text1;
    public TMP_Text text2;
    public TMP_Text text3;
    
    private const string SkinKey = "SelectedSkin";
    private int currentSkinIndex = 0;

    void Start()
    {
        int savedSkinIndex = PlayerPrefs.GetInt(SkinKey, 0);
        SelectSkin(savedSkinIndex);

    }

    public void SelectSkin(int index)
    {
        text1.color = Color.black;
        text2.color = Color.black;
        text3.color = Color.black;
        currentSkinIndex = index;
        if (currentSkinIndex == 0)
        {
            text1.color = Color.white; 
        }
        if (currentSkinIndex == 1)
        {
            text2.color = Color.white;
        }
        if (currentSkinIndex == 2)
        {
            text3.color = Color.white;
        }
        targetRenderer.sprite = availableSkins[index];
        Debug.Log("Selected skin index (preview only): " + index);
    }

    public void ApplySelectedSkin()
    {
        PlayerPrefs.SetInt(SkinKey, currentSkinIndex);
        PlayerPrefs.Save();
        Debug.Log("Saved selected skin: " + currentSkinIndex);
    }
}
