using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : Trap
{
    public float pointLoss = 10;
    public float knockBack = 100;

    protected override void OnTrapHit(CharacterManager character)
    {
        Debug.Log("hit");
        Vector2 direction = (character.transform.position - transform.position);
        character.movement.rigidbody2D.AddForce(direction * knockBack, ForceMode2D.Impulse);
        character.data.points -= pointLoss;
        character.Stun(2);
    }
}
