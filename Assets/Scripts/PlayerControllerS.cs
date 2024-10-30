using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerS : MonoBehaviour
{
    public static PlayerControllerS PlayerControllerSInstance;
    public float moveSpeed = 5f;

    private Rigidbody rb;
    public bool isGrounded;
    public bool isCrouched = false;

    public float jumpForce = 10f;
    Vector3 movementInput;
    float verticalInput;
    float horizontalInput;

    [Header("PlayerHealth")]
    [SerializeField] public float health = 100f;

    void Start()
    {
        if (PlayerControllerSInstance == null)
        {
            PlayerControllerSInstance = this;
        }

        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        horizontalInput = ControlFreak2.CF2Input.GetAxis("Horizontal");
        verticalInput = ControlFreak2.CF2Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, 0f) * moveSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + transform.TransformDirection(movement));


        if (ControlFreak2.CF2Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    #region Collision
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    #endregion
    #region Health

    public void DecreaseHealth(float DamageAmount)
    {
        if (!isCrouched)
        {
            health -= DamageAmount;
            if (health <= 0)
            {
                health = 0;
                GameManager.instance.StartCoroutine(GameManager.instance.LevelFailedHealth());
            }
            GameManager.instance.HealthSlider.GetComponent<Slider>().value = health;
            GameManager.instance.RemainingHealth = health;
        }
        
    }
    #endregion
}
