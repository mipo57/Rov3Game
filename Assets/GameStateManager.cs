using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager {

    public enum State {Start, Game, End };

    private static GameStateManager instance = null;
    private Dictionary<int, Gate> bramki = new Dictionary<int, Gate>();
    private int activeGate = 0;
    private int maxGate = 0;
    private Camera startCamera = null;
    private Camera endCamera = null;
    private GameObject startText = null;
    private GameObject endText = null;
    private TextMeshPro scoreText = null;
    private KeyboardController controlls = null;
    private Camera rovCamera = null;
    private float timeStart = 0;
    private State gameState = State.Start;

    public static GameStateManager getInstance()
    {
        if (instance == null)
            instance = new GameStateManager();

        return instance;
    }

    public void registerROVCamera(Camera rovCamera)
    {
        this.rovCamera = rovCamera;
    }

    internal State getState()
    {
        return gameState;
    }

    public int getActiveGate()
    {
        return activeGate;
    }

    internal void setGameplayState()
    {
        Debug.Log("Set Gameplay state!");

        deactivateStartObjects();
        deactivateStartObjects();
        controlls.active = true;
        rovCamera.enabled = true;

        timeStart = Time.time;
    }

    public void activateStartObjects()
    {
        startCamera.enabled = true;
        startText.SetActive(true);
    }

    public void deactivateStartObjects()
    {
        startCamera.enabled = false;
        startText.SetActive(false);
    }

    public void activateEndObjects()
    {
        endCamera.enabled = true;
        endText.SetActive(true);
        scoreText.gameObject.SetActive(true);
    }

    public void deactivateEndObjects()
    {
        endCamera.enabled = false;
        endText.SetActive(false);
        scoreText.gameObject.SetActive(false);
    }

    public void setStartState()
    {
        Debug.Log("Start State!");
        activateStartObjects();
        deactivateEndObjects();
        rovCamera.enabled = false;
        controlls.active = false;
    }

    public void setEndState()
    {
        deactivateStartObjects();
        activateEndObjects();
        rovCamera.enabled = false;
        controlls.active = false;

        float gameTime = Time.time - timeStart;
        scoreText.text = ((int)gameTime).ToString() + " seconds";
    }

    public void NextGate()
    {
        activeGate += 1;

        if (activeGate > maxGate)
        {
            setEndState();
        }
    }

    public void registerGate(Gate gate)
    {
        if (bramki.ContainsKey(gate.number))
            return;

        if (gate.number > maxGate)
            maxGate = gate.number;

        bramki.Add(gate.number, gate);
    }

    public void registerStartCamera(Camera camera)
    {
        this.startCamera = camera;
    }

    public void registerEndCamera(Camera camera)
    {
        endCamera = camera;
    }

    public void registerStartText(GameObject startText)
    {
        this.startText = startText;
    }

    public void registerEndText(GameObject endText)
    {
        this.endText = endText;
    }

    public void registerScoreText(TextMeshPro scoreText)
    {
        this.scoreText = scoreText;
    }

    public void registerControlls(KeyboardController controller)
    {
        this.controlls = controller;
    }
}
