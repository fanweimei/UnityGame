using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelect : MonoBehaviour {
    public int starsNum = 0;
    private bool isSelect = false;
    public GameObject lockObj;
    public GameObject starObj;
    public GameObject panel;
    public Text starText;
    public int startPass;
    public int endPass;
	// Use this for initialization
	void Start () {
        //PlayerPrefs.DeleteAll();
        if(PlayerPrefs.GetInt("totalNum", 0) >= starsNum)
        {
            isSelect = true;
        }
        if (isSelect)
        {
            lockObj.SetActive(false);
            starObj.SetActive(true);
        }
        int sum = 0;
        for(int i=startPass; i<=endPass; i++)
        {
            sum += PlayerPrefs.GetInt("level" + i);
        }
        starText.text = sum.ToString() + "/9";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Selected()
    {
        if(isSelect)
        {
            panel.SetActive(true);
            transform.parent.gameObject.SetActive(false);
        }
    }

    public void Back()
    {
        panel.SetActive(false);
        transform.parent.gameObject.SetActive(true);
    }
}
