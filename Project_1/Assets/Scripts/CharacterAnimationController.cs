using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    public Animator animator; //need the animator to animate the animation

    private MovementController movementController; // get the movement script :D

    // Start is called before the first frame update
    void Start()
    {
        //get the movment controller from the same game object

        movementController = GetComponent<MovementController>();

        if (movementController != null)
        {
            //subscribe to the state Change event from the movment script...\

            movementController.OnStateChanged += UpdateAnimation;
        }
        else
        {
            Debug.LogError("Cannot find the Movment controller on this Game Object X_X"); //the fancy debugger
        }
    }

    private void OnDestroy()
    {
        if (movementController != null)
        {
            movementController.OnStateChanged -= UpdateAnimation;
        }
    }

    void UpdateAnimation(PlayerState state)
    {
        //reset animators paramaters
        animator.ResetTrigger("jump");
        animator.SetBool("ground_move", false);
        animator.SetBool("air_move", false);
        animator.SetBool("isGliding", false);

        //update the anims based on state
        switch (state)
        {
            case PlayerState.Idle:
                //you dont need anything to idle
                break;

            case PlayerState.Walking:
                animator.SetBool("ground_move", true);
                break;

            case PlayerState.Jumping:
                animator.SetTrigger("jump");
                break;

            case PlayerState.Gliding:
                animator.SetBool("isGliding", true);
                break;

            case PlayerState.Falling:
                animator.SetBool("air_move", true);
                break;
        }
    }
}
