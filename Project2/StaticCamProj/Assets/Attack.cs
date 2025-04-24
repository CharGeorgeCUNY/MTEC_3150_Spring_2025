using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public SphereCollider sc;
    public MeshRenderer mr;

    private void Start()
    {
        sc = GetComponent<SphereCollider>();
        mr = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
        
    
    {

       if(other.gameObject.GetComponent<iDamageable>() != null)
        {
            iDamageable Obj = other.gameObject.GetComponent<iDamageable>();


            Obj.DoDamage();
        }
        
        
        

        //DoDestroyHit();
    }

    void DoDestroyHit()
    {
        Destroy(this.gameObject);
    }
}