using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public List<Bird> birds;
    public List<Pig> pigs;
    public static GameManager instance;
    private Vector3 originPos;
    private int starsNum = 0;
    private int totalNum = 10;

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
                birds[i].canClick = true;
            } else
            {
                birds[i].enabled = false;
                birds[i].sp.enabled = false;
                birds[i].canClick = false;
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
        for (; starsNum < birds.Count + 1; starsNum++)
        {
            if (starsNum>=stars.Length)
            {
                break ;
            }
            yield return new WaitForSeconds(0.2f);
            stars[starsNum].SetActive(true);
        }
        print(starsNum);
    }

    public void Replay()
    {
        SaveData();
        SceneManager.LoadScene(2);
    }

    public void Home()
    {
        SaveData();
        SceneManager.LoadScene(1);
    }

    public void SaveData()
    {
        if(starsNum > PlayerPrefs.GetInt(PlayerPrefs.GetString("nowLevel"), 0))
        {
            PlayerPrefs.SetInt(PlayerPrefs.GetString("nowLevel"), starsNum);
        }
        ReCountTotalNum();
    }

    public void ReCountTotalNum()
    {
        int sum = 0;
        for (int i = 0; i <= totalNum; i++)
        {
            sum += PlayerPrefs.GetInt("level" + i.ToString());
        }
        PlayerPrefs.SetInt("totalNum", sum);
    }
}
