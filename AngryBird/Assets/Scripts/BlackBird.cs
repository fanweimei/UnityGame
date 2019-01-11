using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBird : Bird {

    public List<Pig> blocks = new List<Pig>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            blocks.Add(collision.gameObject.GetComponent<Pig>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            blocks.Remove(collision.gameObject.GetComponent<Pig>());
        }
    }

    public override void ShowSkill()
    {
        base.ShowSkill();
        print("black:" + blocks.Count);
        if(blocks.Count > 0)
        {
            for(int i=0; i<blocks.Count; i++)
            {
                blocks[i].Dead();
            }
        }
        OnClear();
    }

    void OnClear()
    {
        rb.velocity = Vector3.zero;
        Instantiate(boom, transform.position, Quaternion.identity);
        sRenderer.enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        trail.TrailStop();
    }

    protected override void Next()
    {
        GameManager.instance.birds.Remove(this);
        Destroy(gameObject);
        GameManager.instance.NextBird();
    }

}
