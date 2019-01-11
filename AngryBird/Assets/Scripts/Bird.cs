using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {
    private bool isClick = false;
    public float maxDis = 1.5f;
    [HideInInspector]
    public SpringJoint2D sp;
    protected Rigidbody2D rb;
    protected BirdTrail trail;
    private bool canClick = true;
    private bool isFly = false;
    protected SpriteRenderer sRenderer;
    public Sprite hurtImage;

    public LineRenderer right;
    public LineRenderer left;
    public Transform rightPos;
    public Transform leftPos;
    public GameObject boom;
    public float cameraFollowSpeeed = 3;

    public AudioClip select;
    public AudioClip fly;
    //public AudioClip dead;

    // Use this for initialization
    void Awake() {
        sp = GetComponent<SpringJoint2D>();
        rb = GetComponent<Rigidbody2D>();
        trail = GetComponent<BirdTrail>();
        sRenderer = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		if(isClick)
        {
            // 更新小鸟位置
            Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            v.z = 0;
            transform.position = v;
            if (Vector3.Distance(transform.position, rightPos.position) > maxDis)
            {
                Vector3 pos = (transform.position - rightPos.position).normalized * maxDis;
                transform.position = pos + rightPos.position;
            }
            Line();
        }

        // 相机跟随
        float posX = transform.position.x;
        Vector3 initPos = Camera.main.transform.position;
        Camera.main.transform.position = Vector3.Lerp(initPos, new Vector3(Mathf.Clamp(posX, 0, 15), initPos.y, initPos.z), cameraFollowSpeeed * Time.deltaTime);
        
        if(isFly)
        {
            if(Input.GetMouseButtonDown(0))
            {
                ShowSkill();
            }
        }
    }

    private void OnMouseDown()
    {
        if(canClick)
        {
            AudioPlay(select);
            isClick = true;
            rb.isKinematic = true;
        }

    }

    private void OnMouseUp()
    {
        if(canClick)
        {
            isClick = false;
            rb.isKinematic = false;
            Invoke("Fly", 0.1f);
            right.enabled = false;
            left.enabled = false;
            canClick = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isFly = false;
        trail.TrailStop();
    }

    void Fly()
    {
        isFly = true;
        AudioPlay(fly);
        trail.TrailStart();
        sp.enabled = false;
        Invoke("Next", 5);
    }

    void Line()
    {
        right.enabled = true;
        left.enabled = true;

        right.SetPosition(0, rightPos.position);
        right.SetPosition(1, transform.position);

        left.SetPosition(0, leftPos.position);
        left.SetPosition(1, transform.position);
    }

    protected virtual void Next()
    {
        GameManager.instance.birds.Remove(this);
        Destroy(gameObject);
       // AudioPlay(dead);
        Instantiate(boom, transform.position, Quaternion.identity);
        GameManager.instance.NextBird();
    }

    public void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }

    public virtual void ShowSkill()
    {
        isFly = false;
    }

    public void Hurt()
    {
        sRenderer.sprite = hurtImage;
    }
}
