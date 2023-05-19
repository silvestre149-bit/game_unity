using UnityEngine;

public class IcebergController : MonoBehaviour
{
    public float impulseForce = 10f; // Força de impulso aplicada ao personagem
    public float centerOfMassOffset = 0.5f; // Deslocamento do centro de massa do iceberg

    private Rigidbody2D personagemRb;
    private Rigidbody2D icebergRb;
    private HingeJoint2D hingeJoint;

    void Start()
    {
        personagemRb = GameObject.Find("personagem").GetComponent<Rigidbody2D>();
        icebergRb = GetComponent<Rigidbody2D>();
        hingeJoint = GetComponent<HingeJoint2D>();

        // Define a posição inicial do centro de massa do iceberg
        Vector2 com = icebergRb.centerOfMass;
        com.y += centerOfMassOffset;
        icebergRb.centerOfMass = com;
    }

    void FixedUpdate()
    {
        if (personagemRb.GetComponent<Collider2D>().IsTouching(icebergRb.GetComponent<Collider2D>()))

        {
            // Calcula a força de impulso com base na entrada do jogador
            float moveInput = Input.GetAxisRaw("Horizontal");
            Vector2 impulse = new Vector2(moveInput, 0f) * impulseForce;

            // Aplica a força de impulso no personagem
            personagemRb.AddForce(impulse);

            // Calcula a nova posição do centro de massa do iceberg
            float totalWeight = personagemRb.mass + icebergRb.mass;
            float personagemWeightPercentage = personagemRb.mass / totalWeight;
            float icebergWeightPercentage = icebergRb.mass / totalWeight;
            float personagemPosition = personagemRb.position.x;
            float icebergPosition = transform.position.x;
            float newCenterOfMass = (personagemPosition * personagemWeightPercentage) + 
                                    (icebergPosition * icebergWeightPercentage) + 
                                    centerOfMassOffset;
            Vector2 com = icebergRb.centerOfMass;
            com.y = newCenterOfMass;
            icebergRb.centerOfMass = com;

            // Aplica a força de torque para simular o movimento de gangorra do iceberg
            float rotationForce = personagemPosition > icebergPosition ? -1f : 1f;
            hingeJoint.motor = new JointMotor2D { motorSpeed = rotationForce * 50f, maxMotorTorque = 10000f };
        }
    }
}
