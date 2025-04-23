using System.Collections;
using UnityEngine;
using DG.Tweening;

public class TileBasedMover : MonoBehaviour
{
    // Start is called before the first frame update

    float DistancePerMove = 1.0f;
    bool CanMove = true;

    public GameObject[] Objects;
    void Start()
    {
        
    }

    /// <summary>
    /// This update is an exmaple of using the 
    /// </summary>
    void Update()
    {
        int rand = Random.Range(0, Objects.Length-1);
        GameObject Rand = Objects[rand];
        Vector3 moveVector = Vector3.zero;
        if(Input.GetKeyDown(KeyCode.UpArrow) && CanMove)
        {
            CanMove = false;
            transform.DOMoveY(transform.position.y + DistancePerMove, .25f).SetEase(Ease.OutCubic)
                .OnComplete(() => { LocalComplete(Rand); });
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && CanMove)
        {
            CanMove = false;
            transform.DOMoveY(transform.position.y - DistancePerMove, .25f).SetEase(Ease.OutCubic).OnComplete(() => { LocalComplete(Rand); });
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && CanMove)
        {
            CanMove = false;
            transform.DOMoveX(transform.position.x - DistancePerMove, .25f).SetEase(Ease.OutCubic).OnComplete(() => { LocalComplete(Rand); });
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && CanMove)
        {
            CanMove = false;
            transform.DOMoveX(transform.position.x + DistancePerMove, .25f).SetEase(Ease.OutCubic).OnComplete(() => { LocalComplete(Rand); });
        }
        
    }

    void ExmapleEmpty()
    {
        
    }
    void LocalComplete(GameObject toChange)
    {
        CanMove = true;
        toChange.GetComponent<SpriteRenderer>().DOColor(Color.red, .1f).SetLoops(4, LoopType.Yoyo);
    }    
}
