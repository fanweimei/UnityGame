using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour {

    private void Awake()
    {
        print(PlayerPrefs.GetString("nowLevel"));
        Instantiate(Resources.Load(PlayerPrefs.GetString("nowLevel")));
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
