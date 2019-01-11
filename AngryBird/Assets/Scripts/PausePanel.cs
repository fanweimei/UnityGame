﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour {
    private Animator anim;
    public GameObject button;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void Home()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }

    public void Pause()
    {
        anim.SetBool("isPause", true);
        button.SetActive(false);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        anim.SetBool("isPause", false);
    }

    public void PauseAnimEnd()
    {
        Time.timeScale = 0;
    }
    public void ResumeAnimEnd()
    {
      //  Time.timeScale = 1;
        button.SetActive(true);
    }
}