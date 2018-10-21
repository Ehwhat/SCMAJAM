using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTrap : Trap {

    public static PortalTrap[] links;

    public Animator animator;
    public LayerMask mask;
    public float delayTimeMin = 4;
    public float delayTimeMax = 8;
    public float lifetimeMin = 2;
    public float lifetimeMax = 6;

    private float currentTime = 0;
    private float currentLifetimeLimit = 0;
    private float currentDelayLimit = 0;

    private float characterFudgeDelay = 1f;
    private float lastPortalUse = 0;
    private CharacterManager lastCharacter;

    public override void OnMapStart()
    {
        if(links == null)
        {
            links = GameObject.FindObjectsOfType<PortalTrap>();
        }
        
    }

    public override void OnMapEnd()
    {
        links = null;
    }

    private void Start()
    {
        SetNextDelayAndTime();
        currentTime = Random.Range(0, currentDelayLimit);

    }

    private void Update()
    {
        if (isArmed)
        {
            if(currentTime > currentLifetimeLimit)
            {
                isArmed = false;
                animator.SetBool("IsOpened", false);
                currentTime = 0;
            }
        }
        else
        {
            if(currentTime > currentDelayLimit)
            {
                isArmed = true;
                animator.SetBool("IsOpened", true);
                currentTime = 0;
            }
        }
        currentTime += Time.deltaTime;
    }

    private void SetNextDelayAndTime()
    {
        currentLifetimeLimit = Random.Range(lifetimeMin, lifetimeMax);
        currentDelayLimit = Random.Range(delayTimeMin, delayTimeMax);
    }

    private PortalTrap PickRandomPortal()
    {
        if(links.Length == 0)
        {
            return null;
        }
        PortalTrap other = links[Random.Range(0, links.Length - 1)];
        return other;
    }

    public void MatchPortal(float time, float lifetime)
    {
        currentTime = time;
        currentLifetimeLimit = lifetime;
        isArmed = true;
    }

    protected override void OnTrapHit(CharacterManager character)
    {
        if(character == lastCharacter && lastPortalUse > Time.time - characterFudgeDelay)
        {
            return;
        }
        if (isArmed)
        {
            Vector2 differnce = transform.position - character.transform.position;
            for (int i = 0; i < links.Length; i++)
            {
                PortalTrap nextPortal = links[i];
                if (nextPortal == this)
                    continue;
                if (nextPortal.isArmed)
                {
                    lastPortalUse = Time.time;
                    nextPortal.lastPortalUse = Time.time;
                    lastCharacter = character;
                    character.transform.position = (Vector2)nextPortal.transform.position + differnce * 1.2f;
                }
            }
            
        }
    }
}
