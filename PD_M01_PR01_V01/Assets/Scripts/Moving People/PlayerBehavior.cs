using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerBehavior : MonoBehaviour
{
    public float rotateSpeed = 75f;
    public float jumpVelocity = 5f;
    public float distanceToGround = 0.1f;
    public LayerMask groundLayer;
    public GameObject bullet;
    public float bulletSpeed = 100f;
    public GameBehavior gameManager;
    public delegate void JumpingEvent();
    public event JumpingEvent playerJump;

    private float vInput;
    private float hInput;
    private Rigidbody _rb;
    private CapsuleCollider _col;
    private GameBehavior _gameManager;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
    }

    void Update()
    {
        vInput = Input.GetAxis("Vertical") * gameManager.moveSpeed;
        hInput = Input.GetAxis("Horizontal") * rotateSpeed;

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
            playerJump();
        }
    }

    void FixedUpdate()
    {
        Vector3 rotation = Vector3.up * hInput;
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        _rb.MovePosition(this.transform.position + this.transform.forward * vInput * Time.fixedDeltaTime);
        _rb.MoveRotation(_rb.rotation * angleRot);

        if (Input.GetMouseButtonDown(0))
        {
            GameObject newBullet = Instantiate(bullet, this.transform.position + new Vector3(1, 0, 0), this.transform.rotation) as GameObject;
            Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();
            bulletRB.velocity = this.transform.forward * bulletSpeed;
            var audioSource = GetComponent<AudioSource>();
            audioSource.Play();
        }

    }

    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x, _col.bounds.min.y, _col.bounds.center.z);
        bool grounded = Physics.CheckCapsule(_col.bounds.center, capsuleBottom, distanceToGround, groundLayer, QueryTriggerInteraction.Ignore);
        return grounded;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Enemy")
        {
            _gameManager.HP -= 2;
        }

    }
}
