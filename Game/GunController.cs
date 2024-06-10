using UnityEngine;

public class GunController : MonoBehaviour
{
    public PlayerController playerController;
    [SerializeField] private Animator gunAnim;
    public Transform gun;
    [SerializeField] private float gunDistance;
    private bool gunFacingRight = true;
    public GameObject shootingParticle;

    [Header("BULLET")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    public int currentBullets;
    public int maxBullets;

    [Header("AUDIO")]
    public AudioSource shootAudioSource;
    public AudioSource reloadAudioSource;
    public AudioClip shootAudioClip;
    public AudioClip reloadAudioClip;

    private void Start()
    {
        ReloadGun();
    }

    private void Update()
    {
        if (RoundManager.instance.currentState == GameState.playing)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mousePos - transform.position;

            gun.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            gun.position = transform.position + Quaternion.Euler(0, 0, angle) * new Vector3(gunDistance, 0, 0);

            if (Input.GetKeyDown(KeyCode.Mouse0) && HaveBullets())
            {
                Shoot(direction);
            }

            if (Input.GetKeyDown(KeyCode.R) && currentBullets <= 0)
            {
                ReloadGun();
                AudioManager.instance.PlaySound(reloadAudioSource, reloadAudioClip);
            }

            if (mousePos.x < gun.position.x && gunFacingRight)
            {
                GunFlip();
            }
            else if (mousePos.x > gun.position.x && !gunFacingRight)
            {
                GunFlip();
            }
        }
    }

    public void GunFlip()
    {
        gunFacingRight = !gunFacingRight;
        gun.localScale = new Vector3(gun.localScale.x, gun.localScale.y * -1, gun.localScale.z);
    }

    public void Shoot(Vector3 direction)
    {
        gunAnim.SetTrigger("Shoot");
        GameUI.instance.UpdateBulletsInfo(currentBullets, maxBullets);
        GameObject newBullet = Instantiate(bulletPrefab, gun.position, Quaternion.identity);
        AudioManager.instance.PlaySound(shootAudioSource, shootAudioClip);
        newBullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * bulletSpeed;
        Destroy(newBullet, 2);
        Instantiate(shootingParticle, gun.transform.position, Quaternion.identity);
    }

    public bool HaveBullets()
    {
        if (currentBullets <= 0)
        {
            return false;
        }
        else
        {
            currentBullets--;
            return true;
        }
    }

    public void ReloadGun()
    {
        if (currentBullets <= 0)
        {
            currentBullets = maxBullets;
            GameUI.instance.UpdateBulletsInfo(currentBullets, maxBullets);
        }
    }
}
