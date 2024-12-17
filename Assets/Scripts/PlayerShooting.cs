using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] Image firstBullet;
    [SerializeField] Image secondBullet;
    [SerializeField] Image thirdBullet;

    [SerializeField] TMP_Text numberBulletsText;
    [SerializeField] TMP_Text numberGrenadesText;

    Camera cam;
    [SerializeField] Transform shootingPoint;

    private PlayerInput playerInput;
    private InputAction shootAction;

    float damage = 25f;
    float range = 40f;
    float delay = 0.15f;
    float timer = 0f;

    bool shootingMode = false;
    bool singleModeFlag = true;
    bool canShoot = true;

    int maxNumberBullets = 400;
    int currentNumberBullets;

    float recoilAmount = 0.015f;

    [SerializeField] GameObject hole;

    ParticleSystem smokeEffect;

    [SerializeField] GameObject grenade;
    [SerializeField] Transform grenadePoint;

    int maxNumberGrenades = 3;
    int currentNumberGrenades;

    void Start()
    {
        cam = GetComponentInChildren<Camera>();
        playerInput = GetComponent<PlayerInput>();
        shootAction = playerInput.actions["Shoot"];
        currentNumberBullets = maxNumberBullets;
        currentNumberGrenades = maxNumberGrenades;
        smokeEffect = GetComponentInChildren<ParticleSystem>();
    }

    public void OnSwitchMode(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            shootingMode = !shootingMode;
            if (shootingMode)
            {
                firstBullet.enabled = false;
                thirdBullet.enabled = false;
            }
            else
            {
                firstBullet.enabled = true;
                thirdBullet.enabled = true;
            }
        }
    }

    public void OnGrenade(InputAction.CallbackContext context)
    {
        if (context.started && currentNumberGrenades > 0)
        {
            --currentNumberGrenades;
            GameObject newGrenade = Instantiate(grenade, grenadePoint.position, Quaternion.identity);
            newGrenade.GetComponent<Rigidbody>().AddForce(grenadePoint.transform.forward * 10, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        if (timer > 0)
        {
            canShoot = false;
            timer -= Time.fixedDeltaTime;
        }
        else
        {
            canShoot = true;
        }

        if (shootAction.ReadValue<float>() == 1 && currentNumberBullets > 0)
        {
            if (shootingMode)
            {
                if (canShoot && singleModeFlag)
                {
                    singleModeFlag = false;
                    Shoot();
                } 
            }
            else
            {
                if (canShoot)
                {
                    Shoot();
                }
            }
        }
        else
        {
            singleModeFlag = true;
        }

        numberBulletsText.text = $"{currentNumberBullets} / {maxNumberBullets}";

        numberGrenadesText.text = $"{currentNumberGrenades}";
    }

    void Shoot()
    {
        smokeEffect.Play();
        --currentNumberBullets;
        timer = delay;
        Vector3 recoil = new Vector3(Random.Range(-recoilAmount, +recoilAmount), Random.Range(-recoilAmount, +recoilAmount), Random.Range(-recoilAmount, +recoilAmount));
        Vector3 direction = cam.transform.forward + recoil;
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, direction, out hit, range))
        {
            Instantiate(hole, hit.point, Quaternion.identity);
            if (hit.collider.tag == "Enemy1")
            {
                hit.collider.GetComponent<RunningEnemy>().takeDamage(damage);
            }
            else if (hit.collider.tag == "Enemy2")
            {
                hit.collider.GetComponent<HidingEnemy>().takeDamage(damage);
            }
        }
    }

    public void takeBullets()
    {
        currentNumberBullets = maxNumberBullets;
    }

    public void takeGrenades()
    {
        currentNumberBullets = maxNumberBullets;
        currentNumberGrenades = maxNumberGrenades;
    }
}
