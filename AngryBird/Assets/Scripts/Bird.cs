using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {
    private bool isClick = false;
    public Transform rightPos;
    public float maxDis = 1;

    private void OnMouseDown()
    {
        isClick = true;
    }

    private void OnMouseUp()
    {
        isClick = false;
    }

    // Use this for initialization
    void Start () {
        print(transform.position);
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
}
