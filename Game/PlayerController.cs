using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GunController gunController;
    public PowerUpSpawner powerUpSpawner;

    [Header("GAMEPLAY")]
    [SerializeField] private float slowMotionpowerUpDuration;
    [SerializeField] private float slowMotionTimeScale;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    private float xInput;
    private bool isGrounded;
    private bool facingRight = true;
    public bool isSlowMotion = false;
    private float originalTimeScale = 1f;

    private Rigidbody2D playerRb;
    public Animator playerAnim;
    public ParticleSystem dustParticle;
    public ParticleSystem freezingParticle;
    public GameObject freezingPowerUpText;
    private float freezingPowerUpTextDelay = 1f;

    [Header("COLLISION CHECKS")]
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;

    [Header("AUDIO")]
    public AudioSource jumpAudioSource;
    public AudioSource pickUpAudioSource;
    public AudioClip jumpAudioClip;
    public AudioClip pickUpAudioClip;


    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        powerUpSpawner = GetComponent<PowerUpSpawner>();
        originalTimeScale = Time.timeScale;
        isSlowMotion = false;
        freezingPowerUpText.SetActive(false);
    }

    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        if (RoundManager.instance.currentState == GameState.playing)
        {
            AnimationControllers();
            CollisionChecks();
            Movement();
            FlipController();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
    }

    private void AnimationControllers()
    {
        playerAnim.SetFloat("xVelocity", playerRb.velocity.x);
        playerAnim.SetFloat("yVelocity", playerRb.velocity.y);
        playerAnim.SetBool("isGrounded", isGrounded);
    }

    public void Movement()
    {
        playerRb.velocity = new Vector2(xInput * moveSpeed, playerRb.velocity.y);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
            AudioManager.instance.PlaySound(jumpAudioSource, jumpAudioClip);
            CreateDust();
        }
    }

    public void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public void FlipController()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePos.x < 0 && facingRight)
        {
            Flip();
        }
        else if (mousePos.x > 0 && !facingRight)
        {
            Flip();
        }
    }

    public void CollisionChecks()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
    public void CreateDust()
    {
        dustParticle.Play();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PowerUp"))
        {
            other.gameObject.SetActive(false);
            Instantiate(freezingParticle, other.transform.position, freezingParticle.transform.rotation);
            pickUpAudioSource.PlayOneShot(pickUpAudioClip);
            StartCoroutine(ActivateSlowMotion());
            freezingPowerUpText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PowerUp"))
        {
            StartCoroutine(FreezingPowerUpTextDelay());
        }
    }

    IEnumerator FreezingPowerUpTextDelay()
    {
        yield return new WaitForSeconds(freezingPowerUpTextDelay);
        freezingPowerUpText.SetActive(false);
    }

    IEnumerator ActivateSlowMotion()
    {
        if (!isSlowMotion)
        {
            isSlowMotion = true;
            Time.timeScale = slowMotionTimeScale;
            yield return new WaitForSecondsRealtime(slowMotionpowerUpDuration);
            Time.timeScale = originalTimeScale;
            isSlowMotion = false;
        }
    }
}
