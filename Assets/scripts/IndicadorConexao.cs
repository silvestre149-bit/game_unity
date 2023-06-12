using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;
using TMPro;

public class IndicadorConexao : MonoBehaviour
{
    public Image indicadorConexao;
    public TextMeshProUGUI mensagemTexto;

    private SerialPort serialPort;

    void Start()
    {
        indicadorConexao.color = Color.red;
        mensagemTexto.text = "Plataforma Desconectada";
        serialPort = new SerialPort("/dev/ttyACM0", 9600);

        try
        {
            serialPort.Open();
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }

    void Update()
    {
        if (VerificarConexaoArduino())
        {
            indicadorConexao.color = Color.green;
            mensagemTexto.text = "Plataforma Conectada";
        }
        else
        {
            indicadorConexao.color = Color.red;
            mensagemTexto.text = "Plataforma Desconectada";
        }
    }

    bool VerificarConexaoArduino()
    {
        return serialPort.IsOpen;
    }
}
