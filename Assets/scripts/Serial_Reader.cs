using System.IO.Ports;
using UnityEngine;

public class Serial_Reader : MonoBehaviour
{
    public float velocidadeMovimento = 5f;
    public float forcaIceberg = 10f; // Força que será aplicada ao iceberg

    SerialPort serialPort;
    private GameObject character;
    private GameObject iceberg; // Referência ao iceberg
    private Rigidbody2D icebergRb; // Rigidbody do iceberg

    void Start()
    {
        serialPort = new SerialPort("/dev/ttyACM0", 9600);

        try
        {
            serialPort.Open();
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.Message);
        }

        character = GameObject.Find("personagem");
        if (character == null)
        {
            character = GameObject.FindGameObjectWithTag("personagem");
        }
        
        iceberg = GameObject.Find("Iceberg");
        if (iceberg == null)
        {
            iceberg = GameObject.FindGameObjectWithTag("Iceberg");
        }
        
        icebergRb = iceberg.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (serialPort.IsOpen)
        {
            try
            {
                string data = serialPort.ReadLine();
                Debug.Log(data);

                switch (data.Trim())
                {
                    case "DIREITA":
                        MoverDireita();
                        break;
                    case "ESQUERDA":
                        MoverEsquerda();
                        break;
                    case "CENTRO":
                        // Não faz nada, personagem permanece parado
                        break;
                    default:
                        Debug.LogWarning("Comando inválido recebido: " + data);
                        break;
                }

                Debug.Log("Dados lidos com sucesso!");
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.Message);
            }
        }

        float movimentoHorizontal = Input.GetAxis("Horizontal");
        if (movimentoHorizontal > 0)
        {
            MoverDireita();
        }
        else if (movimentoHorizontal < 0)
        {
            MoverEsquerda();
        }
    }

    void MoverDireita()
    {
        character.transform.Translate(Vector3.right * velocidadeMovimento * Time.deltaTime);
        icebergRb.AddForce(Vector2.left * forcaIceberg); // Adiciona uma força ao iceberg na direção oposta
    }

    void MoverEsquerda()
    {
        character.transform.Translate(Vector3.left * velocidadeMovimento * Time.deltaTime);
        icebergRb.AddForce(Vector2.right * forcaIceberg); // Adiciona uma força ao iceberg na direção oposta
    }

    void OnApplicationQuit()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}
