using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class FPSDisplay : MonoBehaviour
{
    public static FPSDisplay Instance;

    public TMP_Text fpsText;
    private float deltaTime;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(transform.root.gameObject);
    }

    private void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = $"FPS: {Mathf.Ceil(fps)}";
    }

    public void SetVisible(bool visible)
    {
        gameObject.SetActive(visible);
    }
}

