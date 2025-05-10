using UnityEngine;

public class PlayerSkinLoader : MonoBehaviour
{
    public Sprite[] skins; // Same array as in Profile
    public SpriteRenderer playerRenderer;

    void Start()
    {

        int selectedSkin = PlayerPrefs.GetInt("SelectedSkin", 0);
        Debug.Log("Loading skin index: " + selectedSkin);

        if (selectedSkin >= 0 && selectedSkin < skins.Length)
        {
            playerRenderer.sprite = skins[selectedSkin];
        }
        else
        {
            Debug.LogWarning("Skin index out of range!");
        }
    }
}
