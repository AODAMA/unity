using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurrentSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public GameObject PauseMenu;
    public GameObject GameOver;
    public VoidEventChannel OnPlayerDeath;
    public PlayerData playerData;


    public bool isGamePaused = false;

    private void Awake() {
        PauseMenu.SetActive(false);
        GameOver.SetActive(false);
    }

    private void OnEnable() {
        OnPlayerDeath.OnEventRaised += Die;
    }

    private void Die()
    {
        GameOver.SetActive(true);
    }

    private void OnDisable() {
        OnPlayerDeath.OnEventRaised -= Die;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)) {
            ReloadScene();
        }

        if(playerData.currentLifePoints > 0 && Input.GetKeyDown(KeyCode.Escape)) {
            TogglePause();
        }
    }

    public void TogglePause() {
        if(isGamePaused) {
            Resume();
        } else {
            Pause();
        }
    }

    public void Pause() {
        isGamePaused = true;
        Time.timeScale = 0;
        PauseMenu.SetActive(isGamePaused);
    }

    public void Resume() {
        isGamePaused = false;
        Time.timeScale = 1;
        PauseMenu.SetActive(isGamePaused);
    }

    public void Quit() {
        Application.Quit();//ne fonctionne que dans un build final du jeu
        Debug.Log("Quit game");
    }

    public void ReloadScene() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
