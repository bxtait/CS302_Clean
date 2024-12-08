using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument _document;
    private Button _startButton;
    private Button _quitButton;
    private Button _controlsButton;
    private Button _closeControlButton; // Close button
    private List<Button> _menuButtons = new List<Button>();
    private AudioSource _audioSource;
    private VisualElement _controlsPopUp;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _document = GetComponent<UIDocument>();

        // Find the Start, Quit, and Controls buttons
        _startButton = _document.rootVisualElement.Q("StartGameButton") as Button;
        _quitButton = _document.rootVisualElement.Q("QuitButton") as Button; 
        _controlsButton = _document.rootVisualElement.Q("ControlsButton") as Button;

        // Find the Controls PopUp and Close Button
        _controlsPopUp = _document.rootVisualElement.Q("ControlsPopUp") as VisualElement;
        _closeControlButton = _document.rootVisualElement.Q("CloseControlButton") as Button;

        // Debugging: Check if buttons are found
        if (_controlsPopUp != null)
        {
            Debug.Log("ControlsPopUp found!");
            _controlsPopUp.style.display = DisplayStyle.None; // Ensure it is hidden initially
        }
        else
        {
            Debug.LogError("ControlsPopUp not found!");
        }

        if (_closeControlButton != null)
        {
            Debug.Log("CloseControlButton found!");
            _closeControlButton.RegisterCallback<ClickEvent>(OnCloseControlsClick);
        }
        else
        {
            Debug.LogError("CloseControlButton not found!");
        }

        // Register click callbacks for Start and Quit buttons
        _startButton.RegisterCallback<ClickEvent>(OnPlayGameClick);
        _quitButton.RegisterCallback<ClickEvent>(OnQuitGameClick); 
        _controlsButton.RegisterCallback<ClickEvent>(OnControlsGameClick);

        _menuButtons = _document.rootVisualElement.Query<Button>().ToList();
        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnDisable()
    {
        _startButton.UnregisterCallback<ClickEvent>(OnPlayGameClick);
        _quitButton.UnregisterCallback<ClickEvent>(OnQuitGameClick);
        _controlsButton.UnregisterCallback<ClickEvent>(OnControlsGameClick);
        if (_closeControlButton != null)
        {
            _closeControlButton.UnregisterCallback<ClickEvent>(OnCloseControlsClick);
        }

        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnPlayGameClick(ClickEvent evt)
    {
        Debug.Log("You Pressed the Start Button");
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameplayScene"); // Replace with your scene name
    }

    private void OnQuitGameClick(ClickEvent evt)
    {
        Debug.Log("You Pressed the Quit Button");

        // Quit the application
        Application.Quit();

        // For Play Mode in the Editor (only works in Unity Editor)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void OnControlsGameClick(ClickEvent evt)
    {
        Debug.Log("You pressed the Controls Button");

        if (_controlsPopUp != null)
        {
            _controlsPopUp.style.display = DisplayStyle.Flex; // Show the Controls PopUp
            _controlsPopUp.MarkDirtyRepaint(); // Force UI to refresh
        }
    }

    private void OnCloseControlsClick(ClickEvent evt)
    {
        Debug.Log("CloseControlButton clicked!");

        if (_controlsPopUp != null)
        {
            _controlsPopUp.style.display = DisplayStyle.None; // Hide the Controls PopUp
            _controlsPopUp.MarkDirtyRepaint(); // Force UI to refresh
            Debug.Log("ControlsPopUp hidden!");
        }
    }

    private void OnAllButtonsClick(ClickEvent evt)
    {
        _audioSource.Play();
    }
}
