using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 6;
    public float runSpeed = 12;
    public float rotationSpeed = 10;
    public float walkFootstepInterval = 0.7f; 
    public float runFootstepInterval = 0.5f;  
    public AudioClip[] walkFootstepClips;
    public AudioClip[] runFootstepClips;

    private Rigidbody rig;
    private Vector2 input;
    private Vector3 movementVector;
    private Animator animator;
    private AudioSource footstepAudioSource;
    private float footstepTimer = 0f;
    private bool isRunning = false;

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        rig.freezeRotation = true;

        animator = GetComponent<Animator>();
        footstepAudioSource = GetComponent<AudioSource>();
        footstepAudioSource.playOnAwake = false;
    }

    private void Update()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        isRunning = Input.GetKey(KeyCode.LeftShift);

        animator.SetBool("IsWalking", input.magnitude > 0);
        animator.SetBool("IsRunning", isRunning);
    }

    private void FixedUpdate()
    {
        movementVector = input.x * transform.right * (isRunning ? runSpeed : walkSpeed) + input.y * transform.forward * (isRunning ? runSpeed : walkSpeed);
        rig.velocity = new Vector3(movementVector.x, rig.velocity.y, movementVector.z);

        float footstepInterval = isRunning ? runFootstepInterval : walkFootstepInterval;
        UpdateFootstepSounds(isRunning ? runFootstepClips : walkFootstepClips, isRunning ? runSpeed : walkSpeed, footstepInterval);
    }

    private void UpdateFootstepSounds(AudioClip[] footstepClips, float speed, float interval)
    {
        if (footstepAudioSource != null && input.magnitude > 0)
        {
            footstepTimer += Time.deltaTime;

            if (footstepTimer >= interval)
            {
                footstepTimer = 0f;

                if (footstepClips.Length > 0)
                {
                    int randomClipIndex = Random.Range(0, footstepClips.Length);
                    footstepAudioSource.clip = footstepClips[randomClipIndex];
                    footstepAudioSource.Play();
                }
            }
        }
    }
}
