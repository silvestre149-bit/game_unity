using System.IO.Ports;
using UnityEngine;

public class Serial_Reader : MonoBehaviour
{
    public float velocidadeMovimento = 5f;

    SerialPort serialPort;
    private GameObject character;

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
    }

    void MoverEsquerda()
    {
        character.transform.Translate(Vector3.left * velocidadeMovimento * Time.deltaTime);
    }

    void OnApplicationQuit()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}
