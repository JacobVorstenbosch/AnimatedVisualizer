﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwapSenes : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SceneManager.LoadScene(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            SceneManager.LoadScene(1);
        else if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
