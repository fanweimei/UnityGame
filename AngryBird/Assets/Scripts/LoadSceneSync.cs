using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneSync : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Screen.SetResolution(960, 600, false);
        Invoke("Load", 2);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Load()
    {
        SceneManager.LoadScene(1);
    }
}
