using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {
    private bool isClick = false;
    public Transform rightPos;
    public float maxDis = 1.3f;
    private SpringJoint2D sp;
    private Rigidbody2D rb;

    // Use this for initialization
    void Start () {
        sp = GetComponent<SpringJoint2D>();
        rb = GetComponent<Rigidbody2D>();
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
    }

    void Fly()
    {
        sp.enabled = false;
    }
}
