using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject tripleBulletPrefab;
    public Transform firePoint;

    [Header("Kontrol")]
    public bool isPlayerOne = true;
    public bool isTripleShot = false;
    public bool useJoystick = false;

    [Header("Ateşleme")]
    public float fireRate = 0.5f;
    private float fireTimer;

    [Header("Overheat Ayarları")]
    public float maxHeat = 100f;
    public float heatPerShot = 25f;
    public float coolRate = 20f;
    private float currentHeat = 0f;
    private bool isOverheated = false;

    [Header("Overheat UI")]
    public Slider overheatSlider;

    void Start()
    {
        ApplyOverheatSettings();
        UpdateUI();
    }

    void Update()
    {
        fireTimer += Time.deltaTime;

        HandleCooling();
        UpdateUI();

        if (!isOverheated && fireTimer >= fireRate)
        {
            if (ShouldFire())
            {
                Fire();
            }
        }
    }

    void HandleCooling()
    {
        if (currentHeat > 0f)
        {
            currentHeat -= coolRate * Time.deltaTime;
            currentHeat = Mathf.Max(currentHeat, 0f);
        }

        if (currentHeat >= maxHeat)
            isOverheated = true;

        if (isOverheated && currentHeat <= 0f)
            isOverheated = false;
    }

    bool ShouldFire()
    {
        if (useJoystick)
        {
            return Input.GetButtonDown("Fire1");
        }
        else
        {
            return isPlayerOne
                ? Input.GetKeyDown(KeyCode.Space)
                : Input.GetKeyDown(KeyCode.RightShift);
        }
    }

    void Fire()
    {
        GameObject bullet = isTripleShot ? tripleBulletPrefab : bulletPrefab;
        Instantiate(bullet, firePoint.position, Quaternion.identity);

        currentHeat += heatPerShot;
        currentHeat = Mathf.Clamp(currentHeat, 0f, maxHeat);
        fireTimer = 0f;
    }

    void UpdateUI()
    {
        if (overheatSlider != null)
        {
            overheatSlider.value = currentHeat / maxHeat;
        }
    }

    public void Init(bool isPlayerOneValue, Slider slider)
    {
        isPlayerOne = isPlayerOneValue;
        overheatSlider = slider;
        ApplyOverheatSettings();
    }

    void ApplyOverheatSettings()
    {
        if (isPlayerOne)
        {
            heatPerShot = 20f;
            coolRate = 15f;
        }
        else
        {
            heatPerShot = 30f;
            coolRate = 10f;
        }
    }
}