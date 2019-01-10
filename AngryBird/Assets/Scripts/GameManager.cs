using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public List<Bird> birds;
    public List<Pig> pigs;
    public static GameManager instance;
    private Vector3 originPos;

    public GameObject win;
    public GameObject lose;
    public GameObject[] stars;

    private void Awake()
    {
        instance = this;
        if(birds.Count > 0)
        {
            originPos = birds[0].transform.position;
        }
    }
    // Use this for initialization
    void Start () {
        Initialized();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Initialized()
    {
        for(int i=0; i<birds.Count; i++)
        {
            if(i==0)
            {
                birds[i].transform.position = originPos;
                birds[i].enabled = true;
                birds[i].sp.enabled = true;
            } else
            {
                birds[i].enabled = false;
                birds[i].sp.enabled = false;
            }
        }
    }

    public void NextBird()
    {
        if(pigs.Count > 0)
        {
            if (birds.Count > 0)
            {
                Initialized();
            } else
            {
                lose.SetActive(true);
            }
        } else
        {
            win.SetActive(true);
        }
    }

    public void ShowStars()
    {
        StartCoroutine("show");
    }

    IEnumerator show()
    {
        for (int i = 0; i < birds.Count + 1; i++)
        {
            yield return new WaitForSeconds(0.2f);
            stars[i].SetActive(true);
        }
    }
}
