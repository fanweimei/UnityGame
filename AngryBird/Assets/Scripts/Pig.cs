using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour {
    public float maxSpeed = 10;
    public float minSpeed = 4;
    private SpriteRenderer sRenderer;
    public Sprite hurtImage;
    public GameObject boom;
    public GameObject pigScore;

    public bool isPig = false;
    // Use this for initialization
    private void Awake()
    {
        sRenderer = GetComponent<SpriteRenderer>();
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.relativeVelocity.magnitude);
        if(collision.relativeVelocity.magnitude > maxSpeed)
        {
            Dead();
        } else if(collision.relativeVelocity.magnitude > minSpeed)
        {
            sRenderer.sprite = hurtImage;
        }
    }

    private void Dead()
    {
        if(isPig)
        {
            GameManager.instance.pigs.Remove(this);
        }
        Destroy(gameObject);
        Instantiate(boom, gameObject.transform.position, Quaternion.identity);
        GameObject score = Instantiate(pigScore, gameObject.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        Destroy(score, 1.5f);
    }
}
