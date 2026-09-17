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


    [SerializeField] int MaxLife = 10;
    [SerializeField] Image LifeBar;

    private int ActualLife;


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

            float Direction = 0f;

            if (Player.position.x > transform.position.x)
            {
                Direction = 1f;
            }
            else
            {
                Direction = -1f;
            }

            Rigidbody.velocity = new Vector2(Direction * EnemySpeed, Rigidbody.velocity.y);
            animator.SetFloat("Speed", Mathf.Abs(Rigidbody.velocity.x));
            animator.SetFloat("Direction", Direction);


        }

        
    }

    bool CheckGround()
    {
    if(Physics2D.OverlapCircle(GroundCheck.position, 0.2f, Ground))
    {
        return true;
    }
    return false;
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
}

    
