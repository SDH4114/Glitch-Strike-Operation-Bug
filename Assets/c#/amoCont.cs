using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class amoCont : MonoBehaviour
{
    public CharacterController controller;
    public Transform playerCamera;

    public float speed = 5f;
    public float sprintSpeed = 8f;
    public float mouseSensitivity = 2f;
    public float gravity = -9f;
    public float minJumpHeight = 1.0f;
    public float maxJumpHeight = 2.5f;
    public float jumpChargeTime = 0.5f;

    public LayerMask groundLayer;
    public Transform groundCheck;

    public float stamina = 5f;
    public float maxStamina = 5f;
    public float staminaDrain = 1f;
    public float staminaRegen = 0.5f;
    private bool isSprinting;

    private float verticalVelocity;
    private float xRotation = 0f;
    private float jumpHoldTime = 0f;
    private bool isChargingJump = false;

    public int hp = 100;
    public TextMeshProUGUI health;

    public float slowSpeed = 2f;
    public Image staminaBar;
    private float staminaFullTimer = 0f;

    public int damageFromEnemy = 10;

    public Transform[] DeathPoints;

    public market mark;

    public bool isTP = true;

    void Start()
    {
        if(isTP == true)
        {
            int index = Random.Range(0, DeathPoints.Length);
            transform.position = DeathPoints[index].position;
            Cursor.lockState = CursorLockMode.Locked;
        }
        stamina = maxStamina;

        if (staminaBar != null)
            staminaBar.enabled = false;

        UpdateStaminaUI();
    }

    void Update()
    {
        MovePlayer();
        RotateCamera();
        UpdateStaminaUI();

        if (health != null)
            health.text = hp.ToString();

        if (hp == 0)
        {
            int index = Random.Range(0, DeathPoints.Length);
            controller.enabled = false;

            mark.pistolGun.AllCountB = 32;
            mark.akGun.AllCountB = 84;
            mark.piPistolGun.AllCountB = 36;
            mark.shotgunGun.AllCountB = 18;

            mark.hasAK52 = false;
            mark.hasPiPistol = false;
            mark.hasShotgun = false;
            mark.hasBaryer = false;

            mark.money = 250;

            transform.position = DeathPoints[index].position;
            controller.enabled = true;
            hp = 100;
        }

        if (Input.GetKeyDown(KeyCode.T) && hp > 10)
        {
            int index = Random.Range(0, DeathPoints.Length);
            controller.enabled = false;
            transform.position = DeathPoints[index].position;
            controller.enabled = true;

            TakeDamage(10);
        }
}

    void MovePlayer()
    {
        isSprinting = Input.GetKey(KeyCode.LeftShift) && stamina > 0;

        float moveSpeed;

        if (stamina < 1f)
        {
            float t = Mathf.Clamp01(stamina);
            moveSpeed = Mathf.Lerp(slowSpeed, speed, t);
            isSprinting = false;
        }
        else
        {
            moveSpeed = isSprinting ? sprintSpeed : speed;
        }

        Vector3 move = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        bool isGrounded = Physics.CheckSphere(groundCheck.position, 0.3f, groundLayer);

        if (isGrounded && verticalVelocity < 0)
            verticalVelocity = -1f;

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isChargingJump = true;
            jumpHoldTime = 0f;
        }

        if (isChargingJump && Input.GetButton("Jump"))
        {
            jumpHoldTime += Time.deltaTime;
        }

        if (isChargingJump && Input.GetButtonUp("Jump"))
        {
            float jumpForce = Mathf.Lerp(minJumpHeight, maxJumpHeight, Mathf.Clamp01(jumpHoldTime / jumpChargeTime));
            verticalVelocity = Mathf.Sqrt(jumpForce * -2f * gravity);
            isChargingJump = false;
        }

        verticalVelocity += gravity * Time.deltaTime * 0.9f;
        move.y = verticalVelocity;
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (isSprinting)
        {
            stamina -= staminaDrain * Time.deltaTime;
        }
        else if (stamina < maxStamina)
        {
            stamina += staminaRegen * Time.deltaTime;
        }

        stamina = Mathf.Clamp(stamina, 0, maxStamina);
    }

    void UpdateStaminaUI()
    {
        if (staminaBar == null) return;

        staminaBar.fillAmount = stamina / maxStamina;

        if (stamina >= maxStamina)
        {
            staminaFullTimer += Time.deltaTime;

            if (staminaFullTimer > 0.3f)
                staminaBar.enabled = false;
        }
        else
        {
            staminaFullTimer = 0f;
            staminaBar.enabled = true;
        }
    }

    void RotateCamera()
    {
        xRotation = Mathf.Clamp(xRotation - Input.GetAxis("Mouse Y") * mouseSensitivity, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivity);
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        hp = Mathf.Clamp(hp, 0, 999);
    }
}