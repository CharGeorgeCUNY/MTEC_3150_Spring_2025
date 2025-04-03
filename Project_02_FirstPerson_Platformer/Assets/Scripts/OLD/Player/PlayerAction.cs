using JumpSym.Utility;
using Unity.Entities;
using Unity.Entities.UniversalDelegates;
using UnityEngine;

namespace JumpSym.Player
{
    [CreateAssetMenu(menuName = "Custom/New Player Action")]
    public class PlayerAction : ScriptableObject
    {
        [Header("Animation Data")]
        [SerializeField] string actionName;
        [SerializeField] string animationName;
        [SerializeField] ObstacleFlagType obstacleFlag;
        [SerializeField] float animationLength;
        [SerializeField] float transitionTime = 0.2f;

        [SerializeField] float minHeight;
        [SerializeField] float maxHeight;


        [Header("Target Matching")]
        [SerializeField] bool targetMatching;
        [SerializeField] AvatarTarget matchBodyPart;
        [SerializeField] float matchStartTime;
        [SerializeField] float matchTargetTime;

        [SerializeField] Vector3 weightMask = new Vector3(0, 1, 0);
        [SerializeField] float offsetY;


        [Header("Player Controls")]
        [SerializeField] bool hijackPlayer;
        [SerializeField] bool hijackCamera;

        [Header("Climbing?")]
        [SerializeField] bool climbing;

        public Quaternion TargetRotation { get; set; }
        public Vector3 MatchPos { get; set; }
        
        public bool CheckIfPossible(WorldScanner.HitData hitData, Transform player)
        {
            //make sure we're not already in an action
            if (Controller.interactions.inAction) return false;

            //subtract player y position from hit y position
            float height = hitData.HitV.point.y - player.position.y;

            //get the obstacle gameobject
            GameObject obstacle = hitData.HitObject;

            //if height of obstacle is between minimum and maximum height
            //also make sure the tag matches
            if (obstacleFlag == obstacle.GetComponent<Flags>().ObstacleFlag)
            {
                //for wall type obstacles
                if (obstacleFlag == ObstacleFlagType.Wall)
                {
                    //if the wall is the correct height for the action
                    if (height > minHeight && height < maxHeight)
                    {
                        Debug.Log("Detected an " + obstacle + " at height " + height);

                        TargetRotation = Quaternion.LookRotation(-hitData.HitH.normal);

                        //match at the point where the vertical ray hits the top of the obstacle
                        MatchPos = hitData.HitV.point;

                        return true;
                    }
                }

                if (obstacleFlag == ObstacleFlagType.Beam)
                {
                    return true;
                }
            }

            return false;
        }

        public string ActionName => actionName;
        public string AnimName => animationName;
        public ObstacleFlagType ObstacleFlag => obstacleFlag;
        public float AnimTime => animationLength;
        public float TransTime => transitionTime;

        public float MinHeight => minHeight;
        public float MaxHeight => maxHeight;

        public bool TargetMatching => targetMatching;
        public AvatarTarget MatchBodyPart => matchBodyPart;
        public float MatchStartTime => matchStartTime;
        public float MatchTargetTime => matchTargetTime;
        public Vector3 WeightMask => weightMask;
        public float OffsetY => offsetY;

        public bool HijackPlayer => hijackPlayer;
        public bool HijackCamera => hijackCamera;

        public bool Climbing => climbing;
    }
}