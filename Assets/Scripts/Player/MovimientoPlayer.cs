using System;
using UnityEngine;

public class MovimientoPlayer : MonoBehaviour
{
    public CharacterController characterController;
    public float speed;

    //Gravedad
    public float gravity = -9.8f;
    Vector3 velocity;

    //GroundCheck
    public Transform groundCheck;
    public float sphereRadius = 0.3f;
    public LayerMask groundMask;
    public bool isGrounded;

    //Salto
    public float jumpHeight=300f;

    //Correr
    public bool isRunning;
    public float runningSpeedMultiplier = 2f;
    public float runningSpeed = 1; //velocidad cuando no estamos corriendo

    private StaminaBar staminaSlider;
    public float staminaAmount = 5;

    public Vector3 move;


    // Start is called before the first frame update
    void Start()
    {
        staminaSlider = FindObjectOfType<StaminaBar>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.muerto)
        {
            return;
        }

        //Movimiento
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        move = transform.right * x + transform.forward * z;
        characterController.Move(move * speed * Time.deltaTime * runningSpeed);

        //Gravedad
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        //Grounded check
        isGrounded=Physics.CheckSphere(groundCheck.position,sphereRadius, groundMask);
        if(isGrounded&& velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //Salto
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity * Time.deltaTime);
        }

        RunCheck();
    }

    private void RunCheck()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = !isRunning;

            if(isRunning)
            {
                runningSpeed = runningSpeedMultiplier;
                staminaSlider.UseStamina(staminaAmount);
            }
            else
            {
                runningSpeed = 1;
                staminaSlider.UseStamina(0);
            }
        }
    }
}
