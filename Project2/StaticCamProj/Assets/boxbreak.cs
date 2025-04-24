using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxbreak : iDamageable
{
    GameManager gm;
    [SerializeField] GameObject brokenbox;
    private void Start()
    {
        gm = GetComponentInParent<GameManager>();
    }
    public override void DoDamage()
    {
        Instantiate(brokenbox, this.transform.position, Quaternion.identity);
        gm.boxbroke++;
        Destroy(this.gameObject);
    }
}
