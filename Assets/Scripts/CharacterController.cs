using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] float MovementDirection;
    [SerializeField] float Speed = 7;

    [SerializeField] float JumpStrength = 20f;
    [SerializeField] bool DoubleJumped = true;

    [SerializeField] float DashStrength = 12f;
    [SerializeField] float DashDuration = 0.3f;
    [SerializeField] float DashCooldown = 2f;
    [SerializeField] bool IsDashing;
    [SerializeField] bool CanDash = true;

    [SerializeField] bool HasSword = false;
    [SerializeField] bool IsAttacking = false;

    [SerializeField] Transform AttackPoint;
    [SerializeField] float AttackRange = 0.5f;
    [SerializeField] int AttackDamage = 5;
    [SerializeField] LayerMask EnemyLayers;

    [SerializeField] int MaxLife = 20;

    public bool canTakeDamage = true;
    public float invincibilityTime = 1.5f;
    public int ActualLife;




    [SerializeField] Animator animator;


    float FacingDirection = 1f; /*Direita = 1f, Esquerda = -1f*/



    [SerializeField] Rigidbody2D Rigidbody;
    [SerializeField] Transform GroundCheck;
    [SerializeField] LayerMask Ground;

    public void DealDamage()
    {

        float distanceX = Mathf.Abs(AttackPoint.localPosition.x);

        Vector2 AttackCenter = new Vector2(transform.position.x + (distanceX * FacingDirection), AttackPoint.position.y);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackCenter, AttackRange, EnemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyAI enemyScript = enemy.GetComponent<EnemyAI>();

            if (enemyScript != null)
            {
                enemyScript.TakeDamage(AttackDamage);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Sword"))
        {
            HasSword = true;
            Destroy(collision.gameObject);
        }
    }

    public void TakeDamage(int damageAmount)
    {

        if (!canTakeDamage)
        {
            return;
        }
        canTakeDamage = false;
        StartCoroutine(DamageCooldown());
        ActualLife -= damageAmount;

        
        Rigidbody.velocity = new Vector2(0f, Rigidbody.velocity.y);

        if(ActualLife <= 0)
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        ActualLife = MaxLife;
    }

    // Update is called once per frame
    void Update()
    {

        MovementDirection = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(MovementDirection));

        if (MovementDirection > 0)
        {
            FacingDirection = 1f;
        } 
        else if (MovementDirection < 0)
        {
            FacingDirection = -1f;
        }
        animator.SetFloat("FacingDirection", FacingDirection);


        if(Input.GetButtonDown("Jump") && (CheckGround() || DoubleJumped))
        {

            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, 0f);
            Rigidbody.AddForce(transform.up * JumpStrength, ForceMode2D.Impulse);
            DoubleJumped = false;

            if(!CheckGround())
            {
                DoubleJumped = false;
            }

        }

        animator.SetBool("CheckGround", CheckGround());

        if(Input.GetKeyDown(KeyCode.LeftShift) && CanDash)
        {
            Rigidbody.velocity = new Vector2(0f, Rigidbody.velocity.y);
            StartCoroutine(Dash());
        }

        if(Input.GetMouseButtonDown(0) && HasSword && !IsAttacking)
        {
            IsAttacking = true;
            Rigidbody.velocity = new Vector2(0f, Rigidbody.velocity.y);
            animator.SetTrigger("Attack");
            
        }
        
    }

    private void FixedUpdate()
    {

        if (IsAttacking)
        {
            return;
        }

        if (IsDashing)
        {
            animator.SetBool("IsDashing", IsDashing);

            return;
        }
        Rigidbody.velocity = new Vector2(MovementDirection * Speed, Rigidbody.velocity.y);
        animator.SetBool("IsDashing", IsDashing);

    }

    bool CheckGround()
    {
        if(Physics2D.OverlapCircle(GroundCheck.position, .25f, Ground))
        {
            DoubleJumped = true;
            return true;
            
        }
        return false;

    }

    private IEnumerator Dash()
    {
        CanDash = false;
        IsDashing = true;


        float OriginalGravity = Rigidbody.gravityScale;
        Rigidbody.gravityScale = 0f;

        Rigidbody.velocity = new Vector2(DashStrength * FacingDirection, 0f);

        yield return new WaitForSeconds(DashDuration);

        Rigidbody.gravityScale = OriginalGravity;

        float DecelerationTime = 0.25f;
        float Timer = 0f;

        while (Timer < DecelerationTime)
        {
            Timer += Time.deltaTime;

            float PercentComplete = Timer / DecelerationTime;

            float SmoothSpeed = Mathf.Lerp(DashStrength * FacingDirection, 0f, PercentComplete);

            Rigidbody.velocity = new Vector2(SmoothSpeed, Rigidbody.velocity.y);

            yield return null;
        }

        IsDashing = false;
        
        yield return new WaitForSeconds(DashCooldown);

        CanDash = true;
    }

    public void FinishAttack()
    {
        IsAttacking = false;
    }

    private IEnumerator DamageCooldown()
    {
        yield return new WaitForSeconds(invincibilityTime);
        canTakeDamage = true;
    }
}
