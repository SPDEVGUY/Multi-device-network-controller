using NetworkController.Client.Logic;
using NetworkController.Client.Logic.DataTypes.Interfaces;

/// <summary>
/// Usage: 
///     public ButtonBind MouseLeft = new ButtonBind("Mouse.Left");
///     void Update() { 
///         if(MouseLeft.WasPressed) { DoStuffOnce(); } 
///         if(MouseLeft.IsPressed) { DoStuffWhileHoldingButtonDown(); } 
///     } 
/// </summary>
public class ButtonBind
{
    public ButtonBind(string buttonName)
    {
        ButtonName = buttonName;
    }
    public string ButtonName;

    private IDeltaButtonState _button;
    private long lastReceived = 0;

    public bool Enabled
    {
        get
        {
            var root = ReceiverSingleton.Instance;
            if (root != null && root.PacketsReceived > lastReceived)
                _button = root.GetButtonState(ButtonName);

            return _button != null;
        }
    }
    public bool IsPressed { get { return Enabled && _button.IsPressed; } }
    public bool WasPressed { get { return Enabled && _button.PressCount > 0; } }
    public int PressCount { get { return Enabled ? _button.PressCount : 0; } }
    public int LastPressCount { get { return Enabled ? _button.LastPressCount : 0; } }
    public double Acceleration { get { return Enabled ? _button.Acceleration : 0; } }
    public double Velocity { get { return Enabled ? _button.Velocity : 0; } }

}
