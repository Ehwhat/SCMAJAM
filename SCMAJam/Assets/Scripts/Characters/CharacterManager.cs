using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {

    public bool stunned;

    public CharacterData data;
    public TestCharacterMovement movement;
    public Animator animator;

    public void Update()
    {

    }

    public void Stun(float time)
    {
        StartCoroutine(StunEnumerator(time));
    }

    private IEnumerator StunEnumerator(float time)
    {
        stunned = true;
        movement.enabled = false;
        animator.SetBool("IsStunned", true);
        yield return new WaitForSeconds(time);
        stunned = false;
        movement.enabled = true;
        animator.SetBool("IsStunned", false);
    }


}
