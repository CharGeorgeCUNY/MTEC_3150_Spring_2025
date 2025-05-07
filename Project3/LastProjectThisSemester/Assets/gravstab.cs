using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravstab : MonoBehaviour
{
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 norm = new Vector3(0f, 0f, 0f);
        this.rb.transform.Rotate(norm); // = new  
    }
    public void FollowTargetWitouthRotation(Transform target, float distanceToStop, float speed)
    {
        var direction = Vector3.zero;
        if (Vector2.Distance(transform.position, target.position) > distanceToStop)
        {
            direction = target.position - transform.position;

            this.rb.AddRelativeForce(direction.normalized * speed, ForceMode2D.Force);
            this.transform.parent.position = this.transform.position;
        }
    }
}
