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

public class Player : iDamageable

{

    AudioSource sound;

   

    float cooldown = .5f;
    bool cooldownActive;

    Animator ani;

    Transform child;
    public float FiringDistance = 5f;

    public GameObject BulletPrefab;

    public float moveSpeed;
    public float sprintSpeed;
    GameObject cam;

    public Vector2 direction;

    [SerializeField] private float angleOffset = 0f;

    private void Awake()
    {
         sound = GetComponent<AudioSource>();
        ani = GetComponent<Animator>();

        child = GetComponentInChildren<Transform>();
    }
    void Update()
    {



        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

      
        direction = new Vector2(
            mousePos.x - transform.position.x,
            mousePos.y - transform.position.y
        );
        

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;


        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + angleOffset));

        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            
            Vector3 firingPosition = transform.position + transform.up * FiringDistance;
            GameObject bullet = GameObject.Instantiate(BulletPrefab, firingPosition + Vector3.forward * -10, transform.rotation);
            PlayShot();
            LightsOn();


        }
        if (UnityEngine.Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
       

            Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);

        
        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;

       
        transform.position += moveDirection * speed * Time.deltaTime;
        if(moveDirection != Vector3.zero)
        {

            sound.volume = .5f ;
            
        }
        else
        {
            sound.volume = 0;
        }
       
    }
    public override void DoDamage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void LightsOn()
    {
        
        GetComponentInChildren<BoxCollider>().enabled = true;
      
            
            
        LightsOff();
        
    }
    void LightsOff()
    {
        GetComponentInChildren<BoxCollider>().enabled = false;
    }
    void PlayShot()
    {

        this.GetComponentInChildren<shotFX>().shot();

        
    }

}
    


