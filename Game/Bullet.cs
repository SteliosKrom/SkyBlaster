using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D bulletRb;
    public GameObject hitParticle;

    private void Start()
    {
        bulletRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        transform.right = bulletRb.velocity;    
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Target"))
        {         
            gameObject.SetActive(false);
            other.gameObject.SetActive(false);
            GameUI.instance.UpdateScore();
            Instantiate(hitParticle, other.transform.position, hitParticle.transform.rotation);
        }
    }
}
