using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {
    private bool isClick = false;
    public float maxDis = 1.5f;
    [HideInInspector]
    public SpringJoint2D sp;
    private Rigidbody2D rb;
    private BirdTrail trail;

    public LineRenderer right;
    public LineRenderer left;
    public Transform rightPos;
    public Transform leftPos;
    public GameObject boom;

    // Use this for initialization
    void Awake() {
        sp = GetComponent<SpringJoint2D>();
        rb = GetComponent<Rigidbody2D>();
        trail = GetComponent<BirdTrail>();
    }
	
	// Update is called once per frame
	void Update () {
		if(isClick)
        {
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
	}

    private void OnMouseDown()
    {
        isClick = true;
        rb.isKinematic = true;

    }

    private void OnMouseUp()
    {
        isClick = false;
        rb.isKinematic = false;
        Invoke("Fly", 0.1f);
        right.enabled = false;
        left.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        trail.TrailStop();
    }

    void Fly()
    {
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

    void Next()
    {
        GameManager.instance.birds.Remove(this);
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
        GameManager.instance.NextBird();
    }
}
