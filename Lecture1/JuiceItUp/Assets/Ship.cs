using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using UnityEngine;
public class iDamageable : MonoBehaviour
{
    public virtual void DoDamage()
    {

    }

}

public class Ship : iDamageable
{
    // Start is called before the first frame update
    
    public float AccelValue = 10f;
    public float RotationVal = 25f;
    public float FiringDistance = 1.2f;
    //float xMove;
    //Rigidbody2D rb;

    public GameObject BulletPrefab;
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(new Vector3(0f, 0f, 1f), RotationVal * Time.deltaTime);
        }
        if (UnityEngine.Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(new Vector3(0f, 0f, 1f), -RotationVal * Time.deltaTime);
        }
        if (UnityEngine.Input.GetKey(KeyCode.UpArrow))
        {
            //new Vector2(Mathf.Cos(Mathf.Deg2Rad * transform.up), Mathf.Sin(Mathf.Deg2Rad * transform.up));
            Vector2 ForceDirection = transform.up;
            ForceDirection.Normalize();
            GetComponent<Rigidbody2D>().AddForce(ForceDirection * AccelValue);
        }
        if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 firingPosition = transform.position + transform.up * FiringDistance;
            GameObject bullet = GameObject.Instantiate(BulletPrefab, firingPosition, transform.rotation);
            
        }

    }

    public override void DoDamage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
