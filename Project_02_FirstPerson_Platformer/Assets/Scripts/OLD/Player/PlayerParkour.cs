using Unity.Entities;
using UnityEngine;

namespace JumpSym.Player
{
    public class Parkour : MonoBehaviour
    {
        private WorldScanner scanner;

        void Awake()
        {
            scanner = gameObject.GetComponent<WorldScanner>();
        }

        void Update()
        {
            var hitData = scanner.ScanWall();
            if (hitData.HitFoundH)
            {
                //do something
            }
        }
    }
}