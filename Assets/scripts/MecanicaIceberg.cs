using UnityEngine;

public class MecanicaIceberg : MonoBehaviour
{
    public float forcaRotacao = 5f; // Força de rotação aplicada ao iceberg

    private Rigidbody2D icebergRb; // Rigidbody do iceberg
    private bool emContatoComAgua = false; // Flag para verificar se o iceberg está em contato com a água

    private void Start()
    {
        icebergRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float movimentoHorizontal = Input.GetAxis("Horizontal");
        if (movimentoHorizontal > 0)
        {
            RotacionarDireita();
        }
        else if (movimentoHorizontal < 0)
        {
            RotacionarEsquerda();
        }
    }

    private void RotacionarDireita()
    {
        icebergRb.AddTorque(-forcaRotacao);
    }

    private void RotacionarEsquerda()
    {
        icebergRb.AddTorque(forcaRotacao);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Agua"))
        {
            emContatoComAgua = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Agua"))
        {
            emContatoComAgua = false;
        }
    }

    private void FixedUpdate()
    {
        if (emContatoComAgua)
        {
            icebergRb.velocity = Vector2.zero; // Define a velocidade do iceberg como zero
            icebergRb.angularVelocity = 0f; // Define a velocidade angular do iceberg como zero
        }
    }
}
