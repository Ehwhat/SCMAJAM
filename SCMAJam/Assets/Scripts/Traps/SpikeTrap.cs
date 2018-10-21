using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : Trap
{
    public Animator animator;

    public float pointLoss = 10;
    public float knockBack = 100;

    public float lifeTime = 1.5f;
    public float openDelay = 0.6f;
    public float minDelay = 3f;
    public float maxDelay = 6f;

    private float currentDelay = 3f;
    private float currentTime;

    public void Update()
    {
        if (isArmed)
        {
            if (currentTime > lifeTime)
            {
                isArmed = false;
                currentTime = 0;
            }
            else if (currentTime > lifeTime-openDelay)
            {
                isArmed = true;
                animator.SetBool("IsOpened", false);
            }
        }
        else
        {
            if (currentTime > currentDelay + openDelay)
            {
                isArmed = true;
                currentTime = 0;
                Collider2D[] hits = new Collider2D[4];
                int count = Physics2D.OverlapCollider(GetComponent<Collider2D>(), new ContactFilter2D(), hits);

                for (int i = 0; i < count; i++)
                {
                    if(hits[i].tag == "PlayerCollider")
                    {
                        CharacterManager character = hits[0].transform.parent.GetComponentInParent<CharacterManager>();
                        OnTrapHit(character);
                    }
                }

            }else if (currentTime > currentDelay)
            {
                isArmed = false;
                animator.SetBool("IsOpened", true);
            }
        }
        currentTime += Time.deltaTime;
    }

    protected override void OnTrapHit(CharacterManager character)
    {
        if (isArmed)
        {
            Vector2 direction = (character.transform.position - transform.position).normalized;
            character.GetComponent<Rigidbody2D>().AddForce(direction * knockBack, ForceMode2D.Impulse);
            character.data.points -= pointLoss;
        }
    }
}
