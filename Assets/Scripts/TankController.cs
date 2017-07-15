using UnityEngine;

public class TankController : MonoBehaviour 
{
    [SerializeField]
    Transform turret;

    [SerializeField]
    TankCannon cannon;

    [SerializeField]
    float speed = 2;

    [SerializeField]
    float turnSpeed = 90;

    [SerializeField]
    float turretTurnSpeed = 90;

    public PlayerInput input { private get; set; }

    private new Rigidbody rigidbody;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    public void Reset()
    {
        rigidbody.velocity = Vector3.zero;

        transform.position = initialPosition;
        transform.rotation = initialRotation;

        turret.rotation = Quaternion.identity;
    }

    public void Halt()
    {
        rigidbody.velocity = Vector3.zero;
        input = new PlayerInput();
    }

    void FixedUpdate()
    {
        if (!cannon.OnCooldown())
        {
            Vector3 velocity = transform.forward * input.verticalMove * speed;
            float rotation = input.horizontalMove * turnSpeed;
            float turretRotation = input.rotation * turretTurnSpeed;

            transform.rotation = Quaternion.AngleAxis(rotation * Time.fixedDeltaTime, Vector3.up) * transform.rotation;
            turret.rotation = Quaternion.AngleAxis(turretRotation * Time.fixedDeltaTime, Vector3.up) * turret.rotation;

            rigidbody.velocity = velocity;

            if (input.fireDown)
                cannon.Fire();
        }
        else
        {
            rigidbody.velocity = Vector3.zero;
        }
    }
}
