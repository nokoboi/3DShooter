using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public Transform playerPosition;
    public float maxDistance = 100;
    private Animation anim;
    private AudioSource audio;
    public AudioClip muerte;
    public AudioClip perseguir;
    public AudioClip puerta;

    public float detectionDistance = 10f;
    public float attackDistance = 5f;
    public float movementSpeed = 3f;

    public int healthPoints = 0;

    private CharacterController characterController;

    private bool isAudioPlaying = false;
    public GameObject door;

    void Start()
    {
        InvokeRepeating("ShootPlayer", 3, 3);
        anim = GetComponent<Animation>();
        characterController = GetComponent<CharacterController>();
        healthPoints = 3;
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        MoveTowardsPlayer();

        //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, 0);


    }

    void ShootPlayer()
    {
        if (playerPosition != null && CanSeeTarget())
        {
            transform.LookAt(playerPosition);

            if (IsPlayerCloseEnough())
            {
                anim.Play("Attack2");
                // Fijar la rotación en Y a 0
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
                

            }
            
        }
    }

    bool CanSeeTarget()
    {
        if (playerPosition == null)
            return false;

        Vector3 direction = playerPosition.position - transform.position;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, maxDistance))
        {
            if (hit.collider.transform == playerPosition)
            {
                 return true;
            }
        }

        

        return false;
    }

    bool IsPlayerCloseEnough()
    {
        float distance = Vector3.Distance(transform.position, playerPosition.position);
        float enemyRadius = Mathf.Max(transform.localScale.x, transform.localScale.y, transform.localScale.z) * 5f;


        //Debug.Log("Distancia al jugador: " + distance);
        //Debug.Log("Radio del enemigo: " + enemyRadius);

        return distance <= enemyRadius;
    }

    void MoveTowardsPlayer()
    {
        if (playerPosition != null && !IsPlayerCloseEnough() && CanSeeTarget())
        {
            Vector3 direction = playerPosition.position - transform.position;
            direction.y = 0; // Ensure that the movement is horizontal only

            characterController.center = new Vector3(0, characterController.height/1.5f, 0);


            transform.LookAt(playerPosition);
            characterController.SimpleMove(direction.normalized * movementSpeed);

            
            anim.Play("Run");
            if (!isAudioPlaying)
            {
                audio.clip = perseguir;
                audio.Play();
                isAudioPlaying = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            if(healthPoints>1)
            {
                healthPoints--;
            }
            else
            {
                audio.clip = muerte;
                audio.Play();
                anim.Play("Death");
                audio.clip = puerta;
                audio.Play();

                Destroy(gameObject,0.7f);
                Destroy(door);
            }
        }
    }
}


