using System.IO.Ports;
using UnityEngine;

public class Serial_Reader : MonoBehaviour
{
    SerialPort serialPort;
    private GameObject character;

    void Start()
    {
        // Inicializa a porta serial
        serialPort = new SerialPort("/dev/ttyACM0", 9600);

        try
        {
            // Tenta abrir a porta serial
            serialPort.Open();
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.Message);
        }

        // Encontra o objeto do personagem pelo nome ou por uma tag
        character = GameObject.Find("personagem"); // Substitua "NomeDoPersonagem" pelo nome real do objeto do personagem
        // Ou
        character = GameObject.FindGameObjectWithTag("personagem"); // Substitua "TagDoPersonagem" pela tag real do objeto do personagem
    }

    void Update()
    {
        if (serialPort.IsOpen)
        {
            try
            {
                // Lê os dados da porta serial
                string data = serialPort.ReadLine();
                Debug.Log(data);

                switch (data.Trim())
                {
                    case "DIREITA":
                        character.transform.Translate(Vector3.right * Time.deltaTime);
                        break;
                    case "ESQUERDA":
                        character.transform.Translate(Vector3.left * Time.deltaTime);
                        break;
                    case "CENTRO":
                        // Não faz nada, personagem permanece parado
                        break;
                    default:
                        Debug.LogWarning("Comando inválido recebido: " + data);
                        break;
                }

                // Exibe uma mensagem de sucesso no console do Unity
                Debug.Log("Dados lidos com sucesso!");
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.Message);
            }
        }
    }

    void OnApplicationQuit()
    {
        // Fecha a porta serial antes de sair do aplicativo
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}
