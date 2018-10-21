using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Trap : MonoBehaviour {

    public bool isArmed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterManager c = collision.GetComponentInParent<CharacterManager>();
        if(c != null)
        {
            OnTrapHit(c);
        }
    }

    protected abstract void OnTrapHit(CharacterManager character);
    public virtual void OnMapStart() { }
    public virtual void OnMapEnd() { }

}
