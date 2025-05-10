using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public Sprite[] availableSkins; // Assign in Inspector
    public SpriteRenderer targetRenderer; // The character preview or real player

    private const string SkinKey = "SelectedSkin";
    private int currentSkinIndex = 0;

    void Start()
    {
        int savedSkinIndex = PlayerPrefs.GetInt(SkinKey, 0);
        SelectSkin(savedSkinIndex);

    }

    public void SelectSkin(int index)
    {
        currentSkinIndex = index;
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
