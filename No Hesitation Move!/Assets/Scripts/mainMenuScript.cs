﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class mainMenuScript : MonoBehaviour {
	public string newGameScene;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void StartGame() {
    	SceneManager.LoadScene(newGameScene);
    }
}
