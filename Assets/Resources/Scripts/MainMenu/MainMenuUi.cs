using UnityEngine;
using UnityEngine.UI;

public class MainMenuUi : MonoBehaviour
{
    public string SceneToEnterOnStart = "";

    [Header("Buttons")]
    public Button PlayButton;
    public Button ContinueButton;
    public Button SettingsButton;
    public Button QuitButton;

    void Start()
    {
        PlayButton.onClick.AddListener(OnPlayButtonClicked);
        ContinueButton.onClick.AddListener(OnContinueButtonClicked);
        SettingsButton.onClick.AddListener(OnSettingsButtonClicked);
        QuitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    public void OnPlayButtonClicked()
    {
        if (!string.IsNullOrEmpty(SceneToEnterOnStart))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneToEnterOnStart);
        }
    }

    public void OnContinueButtonClicked()
    {
        Debug.Log("Continue button clicked");
    }

    public void OnSettingsButtonClicked()
    {
        Debug.Log("Settings button clicked");
    }
    
    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}
