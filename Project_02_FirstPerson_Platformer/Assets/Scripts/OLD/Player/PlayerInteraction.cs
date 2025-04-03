using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace JumpSym.Player
{
    public class Interactions : MonoBehaviour
    {
        [Header("Player Data")]
        public float characterHeight = 2.03f;

        // [Header("Tooltips")]
        // public DisplayButtonPrompt tooltip;

        [Header("Actions")]
        [SerializeField] List<PlayerAction> playerActions;
        public float correctionSpeed = 0.3f;

        // [Header("Action Data")]
        // public GameObject lookingAt;

        [Header("Bools")]
        public bool inAction = false;
        public bool inControl = true;
        public bool cameraControl = true;

        // public void SetUp()
        // {
        //     tooltip = GameManager._GM.GetComponent<DisplayButtonPrompt>();
        // }

        public void DoAction()
        {
            //if player is grounded, is not in an action, in control, and presses "forward"
            if (Controller.locomotion.isGrounded && !inAction && inControl && (Input.GetAxis("Vertical") > 0))
            {
                //scan for obstacles
                var hitData = Controller.worldScanner.ScanWall();

                if (hitData.HitFoundH)
                {
                    foreach (var action in playerActions)
                    {
                        if (action.CheckIfPossible(hitData, gameObject.transform))
                        {
                            StartCoroutine(DoActionCoroutine(action));
                            break;
                        }
                    }
                }

            }
        }

        
        public void CheckRagdoll()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (Controller.animator.enabled)
                {
                    Controller.rememberPoint = transform.position;

                    Controller.animator.enabled = false;

                    SetControl(false);
                }

                else
                {
                    transform.Translate(Controller.rememberPoint);
                    Controller.animator.enabled = true;
                    SetControl(true);
                }
            }
        }


        // public void LookingAt()
        // {
        //     WorldScanner.HitData hit = Controller.worldScanner.ScanCamera();

        //     //if the player isnt already looking at an object
        //     if (!tooltip.playerLooking)
        //     {
        //         tooltip.playerLooking = true;
        //         tooltip.myPoint = hit.HitPoint;
        //         tooltip.promptState = DisplayButtonPrompt.PromptState.Show;
        //         Debug.Log("Showing prompt!");
        //     }

        //     lookingAt = hit.HitObject;
        //     Debug.Log("Looking at a " + lookingAt);
        // }

        public void SetControl(bool playerInControl)
        {
            inControl = playerInControl;
        }

        public void SetCameraControl(bool playerInControl)
        {
            cameraControl = playerInControl;
        }
        

        public IEnumerator DoActionCoroutine(PlayerAction action)
        {
            inAction = true;

            Controller.locomotion.ResetBools();

            if (action.HijackPlayer && inControl == true)
            {
                SetControl(false);
            }

            if (action.HijackCamera && cameraControl == true)
            {
                SetCameraControl(false);
            }

            //transition into new animation
            Controller.animator.SetTrigger(action.ActionName);

            //wait a frame
            yield return null;

            var animState = Controller.animator.GetNextAnimatorStateInfo(0);

            // float t = 0;

            // while (t <= action.AnimTime)
            // {
            //     t += Time.deltaTime;

            //     transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, action.TargetRotation, correctionSpeed);

            //     //remember to wait!!!
            //     yield return new WaitForSeconds(0.1f);


            //     // if (action.RotateToObstacle)
            //     // {
            //     //     
            //     //     yield return null;
            //     // }

            //     // if (action.TargetMatching)
            //     // {
            //     //     if (Controller.animator.IsInTransition(0))
            //     //     {
            //     //         Debug.Log("Animator is in transition!");
            //     //         yield return new WaitForSeconds(0.1f);
            //     //     }
            //     //     //match target at(target position, target rotation, bodypart to match, axis to match, start time, target time)
            //     //     Controller.animator.MatchTarget(action.MatchPos + new Vector3(0, action.OffsetY, 0), transform.rotation, action.MatchBodyPart,
            //     //     new MatchTargetWeightMask(action.WeightMask, 0), action.MatchStartTime, action.MatchTargetTime, false);
            //     //     Debug.Log("Matching at " + action.MatchPos);
            //     //     yield return new WaitForSeconds(0.1f);
            //     // }
            // }

            if (action.HijackCamera && cameraControl == false)
            {
                SetCameraControl(true);
                Controller.locomotion.ZeroCameraVectors();
            }

            if (action.HijackPlayer && inControl == false)
            {
                SetControl(true);
                Controller.locomotion.ZeroVectors();
            }

            inAction = false;
        }

        
    }
}