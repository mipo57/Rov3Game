using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Register : MonoBehaviour {

    public Camera startCamera;
    public Camera endCamera;
    public GameObject startText;
    public GameObject endText;
    public TextMeshPro scoreText;
    public KeyboardController keyboardControler;
    public Camera rovCamera;

	// Use this for initialization
	void Start () {
        GameStateManager manager = GameStateManager.getInstance();

        manager.registerStartCamera(startCamera);
        manager.registerEndCamera(endCamera);
        manager.registerScoreText(scoreText);
        manager.registerStartText(startText);
        manager.registerEndText(endText);
        manager.registerControlls(keyboardControler);
        manager.registerROVCamera(rovCamera);

        manager.setStartState();
	}
	
	// Update is called once per frame
	void Update () { 
        if (GameStateManager.getInstance().getState() == GameStateManager.State.Start)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                GameStateManager.getInstance().setGameplayState();
            }
        }

        if (GameStateManager.getInstance().getState() == GameStateManager.State.End)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
