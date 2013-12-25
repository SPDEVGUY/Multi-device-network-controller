using System.Threading;
using NetworkController.Client.Logic;
using UnityEngine;

public class NetworkControlReceiver : MonoBehaviour
{
    public int ListenPort = 9050;

    // Use this for initialization
    private void Start()
    {
        ReceiverSingleton.Instance.Port = ListenPort;
        ReceiverSingleton.Instance.Start();
    }

    private void OnApplicationQuit()
    {
        ReceiverSingleton.Instance.Stop();
        Thread.Sleep(10);
    }
}
