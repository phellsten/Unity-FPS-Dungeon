using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class controlMenuMeta : MonoBehaviour {
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


    // Use this for initialization
    void Start () {
		backButton.onClick.AddListener (Back);

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
	

	void Back() {
		SceneManager.LoadScene ("UI");

	}

}
