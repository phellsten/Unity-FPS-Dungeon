using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlsMenuMeta : MonoBehaviour {
	public Button backButton;
    public Button shootButton;
    public Button aimButton;
    public Button meleeButton;
    public Button reloadButton;
    public Button forwardButton;
    public Button backwardButton;
    public Button leftButton;
    public Button rightButton;
    public Button jumpButton;
    public Button crouchButton;
    public Button sprintButton;

    private bool bindingKey = false;
    private string keyToBind;


    // Use this for initialization
    void Start () {
		backButton.onClick.AddListener (Back);

        shootButton.onClick.AddListener(   delegate { ChangeKeyBinding("shoot"); });
        aimButton.onClick.AddListener(     delegate { ChangeKeyBinding("aim"); });
        meleeButton.onClick.AddListener(   delegate { ChangeKeyBinding("melee"); });
        reloadButton.onClick.AddListener(  delegate { ChangeKeyBinding("reload"); });
        forwardButton.onClick.AddListener( delegate { ChangeKeyBinding("forward"); });
        backwardButton.onClick.AddListener(delegate { ChangeKeyBinding("backward"); });
        leftButton.onClick.AddListener(    delegate { ChangeKeyBinding("left"); });
        rightButton.onClick.AddListener(   delegate { ChangeKeyBinding("right"); });
        jumpButton.onClick.AddListener(    delegate { ChangeKeyBinding("jump"); });
        crouchButton.onClick.AddListener(  delegate { ChangeKeyBinding("crouch"); });
        sprintButton.onClick.AddListener(  delegate { ChangeKeyBinding("sprint"); });

        shootButton.GetComponentInChildren<Text>().text    = KeyBindings.ShootKey.ToString();
        aimButton.GetComponentInChildren<Text>().text      = KeyBindings.FocusAimKey.ToString();
        meleeButton.GetComponentInChildren<Text>().text    = KeyBindings.MeleeKey.ToString();
        reloadButton.GetComponentInChildren<Text>().text   = KeyBindings.ReloadKey.ToString();
        forwardButton.GetComponentInChildren<Text>().text  = KeyBindings.ForwardKey.ToString();
        backwardButton.GetComponentInChildren<Text>().text = KeyBindings.BackwardKey.ToString();
        leftButton.GetComponentInChildren<Text>().text     = KeyBindings.LeftKey.ToString();
        rightButton.GetComponentInChildren<Text>().text    = KeyBindings.RightKey.ToString();
        jumpButton.GetComponentInChildren<Text>().text     = KeyBindings.JumpKey.ToString();
        crouchButton.GetComponentInChildren<Text>().text   = KeyBindings.CrouchKey.ToString();
        sprintButton.GetComponentInChildren<Text>().text   = KeyBindings.SprintKey.ToString();

    }

    void Update()
    {
        if (bindingKey)
        {
            // Detect any key press
            foreach (KeyCode kCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(kCode))
                {
                    bindingKey = false;
                    switch (keyToBind)
                    {
                        case "shoot":
                            KeyBindings.ShootKey = kCode;
                            shootButton.GetComponentInChildren<Text>().text = KeyBindings.ShootKey.ToString();
                            break;
                        case "aim":
                            KeyBindings.FocusAimKey = kCode;
                            aimButton.GetComponentInChildren<Text>().text = KeyBindings.FocusAimKey.ToString();
                            break;
                        case "melee":
                            KeyBindings.MeleeKey = kCode;
                            meleeButton.GetComponentInChildren<Text>().text = KeyBindings.MeleeKey.ToString();
                            break;
                        case "reload":
                            KeyBindings.ReloadKey = kCode;
                            reloadButton.GetComponentInChildren<Text>().text = KeyBindings.ReloadKey.ToString();
                            break;
                        case "forward":
                            KeyBindings.ForwardKey = kCode;
                            forwardButton.GetComponentInChildren<Text>().text = KeyBindings.ForwardKey.ToString();
                            break;
                        case "backward":
                            KeyBindings.BackwardKey = kCode;
                            backwardButton.GetComponentInChildren<Text>().text = KeyBindings.BackwardKey.ToString();
                            break;
                        case "left":
                            KeyBindings.LeftKey = kCode;
                            leftButton.GetComponentInChildren<Text>().text = KeyBindings.LeftKey.ToString();
                            break;
                        case "right":
                            KeyBindings.RightKey = kCode;
                            rightButton.GetComponentInChildren<Text>().text = KeyBindings.RightKey.ToString();
                            break;
                        case "jump":
                            KeyBindings.JumpKey = kCode;
                            jumpButton.GetComponentInChildren<Text>().text = KeyBindings.JumpKey.ToString();
                            break;
                        case "crouch":
                            KeyBindings.CrouchKey = kCode;
                            crouchButton.GetComponentInChildren<Text>().text = KeyBindings.CrouchKey.ToString();
                            break;
                        case "sprint":
                            KeyBindings.SprintKey = kCode;
                            sprintButton.GetComponentInChildren<Text>().text = KeyBindings.SprintKey.ToString();
                            break;

                    }

                    break;
                }
            }
        }
    }

    private void ChangeKeyBinding(string key)
    {
        bindingKey = true;
        keyToBind = key;
    }

    void Back() {
		SceneManager.LoadScene ("MainMenu");
	}

}
