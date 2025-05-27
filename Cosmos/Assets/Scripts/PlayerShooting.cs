using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject tripleBulletPrefab;
    public Transform firePoint;

    [Header("Kontrol")]
    public bool isPlayerOne = true;
    public bool isTripleShot = false;

    [Header("Ateşleme")]
    public float fireRate = 0.5f;
    private float fireTimer;

    [Header("Overheat Ayarları")]
    public float maxHeat = 100f;
    public float heatPerShot = 25f;
    public float coolRate = 20f;
    private float currentHeat = 0f;

    private bool isCooling = false;
    private bool isOverheated = false;
    private float overheatCooldownTimer = 0f;
    public float overheatWaitDuration = 1f;

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

        if (isOverheated)
        {
            overheatCooldownTimer += Time.deltaTime;
            if (overheatCooldownTimer >= overheatWaitDuration)
            {
                isOverheated = false;
                isCooling = true;
            }
        }

        if (isCooling)
        {
            HandleCooling();
            if (currentHeat <= 0f)
                isCooling = false;
        }

        UpdateUI();
    }

    void HandleCooling()
    {
        if (currentHeat > 0f)
        {
            currentHeat -= coolRate * Time.deltaTime;
            currentHeat = Mathf.Max(currentHeat, 0f);
        }
    }

    void Fire()
    {
        GameObject bullet = isTripleShot ? tripleBulletPrefab : bulletPrefab;
        Instantiate(bullet, firePoint.position, Quaternion.identity);

        currentHeat += heatPerShot;
        currentHeat = Mathf.Clamp(currentHeat, 0f, maxHeat);

        if (currentHeat >= maxHeat)
        {
            currentHeat = maxHeat;
            isOverheated = true;
            isCooling = false;
            overheatCooldownTimer = 0f;
            Debug.Log("🔥 OVERHEAT!");
        }

        fireTimer = 0f;
    }

    // ✅ Yeni: Input System üzerinden ateş
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed && !isOverheated && fireTimer >= fireRate)
        {
            Fire();
        }
    }

    void UpdateUI()
    {
        if (overheatSlider != null)
        {
            float fill = currentHeat / maxHeat;
            overheatSlider.value = fill;
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
            coolRate = 20f;
        }
        else
        {
            heatPerShot = 20f;
            coolRate = 20f;
        }
    }
}