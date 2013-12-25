using NetworkController.Client.Logic;
using NetworkController.Client.Logic.DataTypes.Interfaces;

/// <summary>
/// Usage: 
///     public GestureBind MouseLeft = new GestureBind("Mouse.Left");
///     void Update() { 
///         if(MouseLeft.WasPressed) { DoStuffOnce(); } 
///         if(MouseLeft.IsPressed) { DoStuffWhileHoldingGestureDown(); } 
///     } 
/// </summary>
public class GestureBind
{
    public GestureBind(string gestureName)
    {
        GestureName = gestureName;
    }
    public string GestureName;

    private IDeltaGestureState _gesture;
    private long lastReceived = 0;

    public bool Enabled
    {
        get
        {
            var root = ReceiverSingleton.Instance;
            if (root != null && root.PacketsReceived > lastReceived)
                _gesture = root.GetGestureState(GestureName);

            return _gesture != null;
        }
    }
    public bool DetectedLastTick { get { return Enabled && _gesture.DetectedLastTick; } }
    public bool Detected { get { return Enabled && _gesture.DetectedThisTick; } }
    public int Intensity { get { return Enabled ? _gesture.Intensity : 0; } }
    public int LastIntensity { get { return Enabled ? _gesture.Intensity : 0; } }
    public double Acceleration { get { return Enabled ? _gesture.Acceleration : 0; } }
    public double Velocity { get { return Enabled ? _gesture.Velocity : 0; } }

}
