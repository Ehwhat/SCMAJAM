using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class heroScript : MonoBehaviour {

  
    public PlayerIndex player;
    Rigidbody2D rb;

    float stunDuration = 1;
    float speed = 3f;
    float speedModifier; //how much faster or slower the hero moves when under the effects of a buff/debuff

    float interactRadius = 1f;

    private GamePadState lastState;

    string pickedBuff;
    string currentItem = "none";

    public CircleCollider2D ownCollider;
    public CircleCollider2D bigCollider;
    
    bool canAct = true;
    bool canDash = true;
    public Vector2 whereToAutoMove;

    float baseDashCooldown = 1f;
    float dashCooldown; //cooldown of the dash, in seconds.
    float dashCooldownModif = 0.5f; //ratio of how much smaller or bigger the cooldown is.

    float baseDashPower = 15f;
    float dashPower;
    float dashPowerModif = 0.5f; //ratio of how weaker or stronger the dash should be.

    public Animator charAnimator;

	// Use this for initialization
	void Start () {
        lastState = GamePad.GetState(player);

        rb = GetComponent<Rigidbody2D>();

        speedModifier = speed / 2; //set the modifier in relation to the speed

        dashPower = baseDashPower;
        dashCooldown = baseDashCooldown;

        equipItem("quickDash");

        //StartCoroutine(Automove(new Vector2(-5,3), 3.0f));
        //StartCoroutine(AutomoveBySpeed(whereToAutoMove, 3f, 3f));
	}
	
	// Update is called once per frame
	void Update () {

        GamePadState state = GamePad.GetState(player);
        Vector2 input = new Vector2(state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y);

        bool aPressed = state.Buttons.A == ButtonState.Pressed && lastState.Buttons.A == ButtonState.Released;
        bool dashPressed = state.Buttons.X == ButtonState.Pressed && lastState.Buttons.X == ButtonState.Released;

        if (canAct)
        {
            rb.AddForce(input * speed, ForceMode2D.Impulse);
            charAnimator.SetBool("IsRunning", input.sqrMagnitude > 0);
            if(input.x > 0f)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    
        if (aPressed && canAct)
        {
            Collider2D[] circleInfo = Physics2D.OverlapCircleAll(transform.position, interactRadius);
            for (int i = 0; i < circleInfo.Length; i++)
            {
                if (circleInfo[i].tag == "chest")
                {
                    Debug.Log("Player " + player + " got a chest");
                    chestScript scriptOfChest = circleInfo[i].gameObject.GetComponent<chestScript>();
                    pickedBuff = scriptOfChest.getBuff();
                    Debug.Log(pickedBuff);

                    if(pickedBuff == "fast")
                    {
                        StartCoroutine( modifySpeed(1f));

                    }else if(pickedBuff == "slow")
                    {
                        StartCoroutine(modifySpeed(-1f));

                    }
                    else if(pickedBuff == "coins")
                    {
                        Debug.Log("got some coins");
                    }
                    //Destroy(circleInfo[i].gameObject);

                }else if(circleInfo[i].tag == "item")
                {
                    itemScript scriptOfItem = circleInfo[i].gameObject.GetComponent<itemScript>();
                    string givenItem = scriptOfItem.equipThis();
                    equipItem(givenItem);
                    Debug.Log("Equipped " + givenItem.ToString());

                }
            }
        }

        if (dashPressed && canAct && canDash && input != new Vector2(0f,0f))
        {

            StartCoroutine(dashTransition(input));
            Debug.Log("gets past input if");
        }


        
        lastState = state;
    }

    IEnumerator modifySpeed(float modifier) //modifier is -1 or 1
    {
        speed += speedModifier * modifier;
        yield return new WaitForSeconds(3);
        speed -= speedModifier * modifier;
    }

    IEnumerator dashTransition(Vector2 direction)
    {
        float currentTime = 0.0f;
        canDash = false;
        charAnimator.SetBool("IsDash", true);
        Vector2 oldPosition = transform.position;
        while (currentTime < 0.2f)
        {
            ownCollider.enabled = false;
            bigCollider.enabled = false;
            RaycastHit2D[] hits = new RaycastHit2D[100];

            ContactFilter2D filter = new ContactFilter2D();
            filter.useTriggers = false;

            
            int count = Physics2D.CircleCast(transform.position, 0.5f, direction, filter, hits, dashPower * Time.deltaTime);
            float distanceDashed = Vector2.Distance(oldPosition, transform.position);
            ownCollider.enabled = true;
            bigCollider.enabled = true;
            Debug.Log("hit something!");
            //Debug.Log(distanceDashed);
            if (count > 0)
            {
                for (int i = 0; i < 100; i++)
                {
                    
                    heroScript other = hits[0].collider.transform.parent.GetComponentInParent<heroScript>();
                    if (other != null)
                    {
                        other.StartCoroutine(other.wasBumped(stunDuration, direction * distanceDashed));
                    }
                    
                }
                StartCoroutine(DashCooldown());
                yield break; //returns

            }
            rb.AddForce(direction * dashPower, ForceMode2D.Impulse);
            currentTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(DashCooldown());
    }

    public void bump (float time, Vector2 direction)
    {
        StartCoroutine(wasBumped(time, direction));
    }

    public IEnumerator wasBumped (float time , Vector2 direction)
    {
        if (canAct)
        {
            charAnimator.SetBool("IsStunned", true);
            canAct = false;
            float currentTime = 0.0f;
            while (currentTime < 0.2f)
            {
                rb.AddForce(direction * dashPower, ForceMode2D.Impulse);
                currentTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            Debug.Log("got bumped");
            yield return new WaitForSeconds(time);
            canAct = true;
            charAnimator.SetBool("IsStunned", false);
        }

    }

    public IEnumerator Automove(Vector2 targetPos, float moveTime)
    {
        canAct = false;
        Vector2 startPos = transform.position;
        float t = 0f; //ratio for the lerp

        while(t < 1f)
        {
            t += Time.deltaTime / moveTime;
            transform.position = Vector2.Lerp(startPos, targetPos, t);
            yield return null;
        }
        canAct = true;
    }

    public IEnumerator AutomoveBySpeed(Vector2 targetPos, float speed, float moveTime)
    {
        canAct = false;
        Vector3 targetPosv3 = targetPos;
        while (transform.position != targetPosv3)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosv3, step);
            yield return null;
        }
        canAct = true;
    }

    IEnumerator DashCooldown()
    {
        charAnimator.SetBool("IsDash", false);
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;

    }

    void equipItem(string item)
    {
        if(item == "bigDash")
        {
            dashPower = baseDashPower + baseDashPower * dashPowerModif;
            dashCooldown = baseDashCooldown + baseDashCooldown * dashCooldownModif;

        }else if(item == "quickDash")
        {
            dashPower = baseDashPower - baseDashPower * dashPowerModif;
            dashCooldown = baseDashCooldown - baseDashCooldown * dashCooldownModif;
        }
    }
}
