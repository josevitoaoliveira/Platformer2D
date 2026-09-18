using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyAI : MonoBehaviour
{
    [SerializeField] float EnemySpeed = 4;
    [SerializeField] float VisionRange;
    [SerializeField] Transform Player;
    [SerializeField] Rigidbody2D Rigidbody;
    [SerializeField] Animator animator;
    [SerializeField] float JumpStrength = 3;
    [SerializeField] Transform GroundCheck;
    [SerializeField] LayerMask Ground;

    [SerializeField] bool TakeHit = false;
    [SerializeField] bool Dead = false;
    [SerializeField] int AttackDamage = 5;
    [SerializeField] bool IsAttacking = false;
    [SerializeField] float AttackDuration = 1f;
    [SerializeField] float AttackCooldown = 2f;

    [SerializeField] bool CanAttack = true;

    [SerializeField] Transform AttackPoint;
    [SerializeField] float AttackRange = 0.5f;
    [SerializeField] LayerMask PlayerLayers;
    float Direction = 0f;




    [SerializeField] int MaxLife = 10;
    [SerializeField] Image LifeBar;

    private int ActualLife;

    public void DealDamage()
    {

        float distanceX = Mathf.Abs(AttackPoint.localPosition.x);

        Vector2 AttackCenter = new Vector2(transform.position.x + (distanceX * Direction), AttackPoint.position.y);

        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(AttackCenter, AttackRange, PlayerLayers);

        foreach (Collider2D player in hitPlayer)
        {
            CharacterController playerScript = player.GetComponent<CharacterController>();

            if (playerScript != null)
            {
                playerScript.TakeDamage(AttackDamage);
            }
        }
    }



    public void TakeDamage(int damageAmount)
    {
        ActualLife -= damageAmount;
        LifeBar.fillAmount = (float)ActualLife / MaxLife;

        TakeHit = true;
        Rigidbody.velocity = new Vector2(0f, Rigidbody.velocity.y);
        animator.SetTrigger("TakeHit");

        if(ActualLife <= 0)
        {
            Dead = true;
            Rigidbody.velocity = new Vector2(0f, Rigidbody.velocity.y);
            animator.SetTrigger("Dead");
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        ActualLife = MaxLife;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player == null)
    {
        return; 
    }

        float Distance = Vector2.Distance(transform.position, Player.position);


        if (Distance <= VisionRange)
        {

            bool isEnemyGrounded = CheckGround();

            if (TakeHit)
            {
                return;
            }

            if (Dead)
            {
                return;
            }

            if (Player.position.y > transform.position.y + 1.5f && isEnemyGrounded)
            {
                Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, 0f);

                Rigidbody.AddForce(transform.up * JumpStrength, ForceMode2D.Impulse);
            }


            if (Player.position.x > transform.position.x)
            {
                Direction = 1f;
            }
            else
            {
                Direction = -1f;
            }

            if(CanAttack)
        {
            Rigidbody.velocity = new Vector2(0f, Rigidbody.velocity.y);
            StartCoroutine(Attack());
        }


            Rigidbody.velocity = new Vector2(Direction * EnemySpeed, Rigidbody.velocity.y);
            animator.SetFloat("Speed", Mathf.Abs(Rigidbody.velocity.x));
            animator.SetFloat("Direction", Direction);


        }

        
    }

    private void FixedUpdate()
    {

        if (IsAttacking)
        {

            animator.SetBool("Attack", IsAttacking);

            return;

        }
        animator.SetBool("Attack", IsAttacking);

    }

    bool CheckGround()
    {
    if(Physics2D.OverlapCircle(GroundCheck.position, 0.2f, Ground))
    {
        return true;
    }
    return false;
    }

    private IEnumerator Attack()
    {
        CanAttack = false;
        IsAttacking = true;

        yield return new WaitForSeconds(AttackDuration);

        IsAttacking = false;
        
        yield return new WaitForSeconds(AttackCooldown);

        CanAttack = true;

    }



    public void FinishHit()
    {
        TakeHit = false;
    }

    public void FinishDeath()
    {
        Destroy(gameObject);

        Dead = false;

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            CharacterController playerScript = collision.gameObject.GetComponent<CharacterController>();
            playerScript.TakeDamage(AttackDamage);
        }

    }
}

    
