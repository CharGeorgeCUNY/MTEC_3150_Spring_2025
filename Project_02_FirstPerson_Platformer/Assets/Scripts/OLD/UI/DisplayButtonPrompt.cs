using System.Collections;
using JumpSym.Player;
using JumpSym.Utility;
using UnityEngine;

namespace JumpSym.UI
{
    public class DisplayButtonPrompt : MonoBehaviour
    {
        // public enum PromptState
        // {
        //     Hide,
        //     Show,
        //     Active
        // }

        // public PromptState promptState = PromptState.Hide;
        // public GameObject tooltip;
        // public bool playerLooking;
        // [SerializeField] private Sprite inactive;
        // [SerializeField] private Sprite active;
        // [SerializeField] private float tooltipDelay = 0.2f;

        // public Vector3 myPoint;


        // void Update()
        // {
        //     //is the player looking at an object that requires "space"
        //     if (Controller.interactions.lookingAt != null)
        //     {
        //         if (Controller.interactions.lookingAt.GetComponent<Tags>().TooltipTag == TooltipTagType.space)
        //         {
        //             playerLooking = true;
        //         }

        //         else
        //         {
        //             playerLooking = false;
        //             promptState = PromptState.Hide;
        //         }
        //     }


        //     //prompt state machine
        //     switch (promptState)
        //     {
        //         case PromptState.Hide:
        //             tooltip.GetComponent<SpriteRenderer>().sprite = inactive;
        //             tooltip.GetComponent<SpriteRenderer>().enabled = false;
        //             break;

        //         case PromptState.Show:
        //             //move the tooltip to the line of sight
        //             tooltip.transform.position = myPoint;

        //             tooltip.GetComponent<SpriteRenderer>().sprite = inactive;
        //             tooltip.GetComponent<SpriteRenderer>().enabled = true;
        //             break;

        //         case PromptState.Active:
        //             Debug.Log("Prompt active.");

        //             tooltip.GetComponent<SpriteRenderer>().sprite = active;
        //             StartCoroutine(TooltipWait());
        //             break;

        //     }
        // }

        // void LateUpdate()
        // {
        //     transform.LookAt(Controller.playerCamera.transform.position, Vector3.up);
        // }

        // public IEnumerator TooltipWait()
        // {
        //     yield return new WaitForSeconds(tooltipDelay);
        //     if (playerLooking)
        //     {
        //         promptState = PromptState.Show;
        //     }
        // }
    }
}
