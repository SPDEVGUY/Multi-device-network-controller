using UnityEngine;
using NetworkController.Client.Logic.DataTypes.Interfaces;

/// <summary>
/// Usage: public SliderBind MouseX = new SliderBind("Mouse.X");
/// </summary>
public class SliderBind
{
    public SliderBind(string sliderName)
    {
        SliderName = sliderName;
    }
    public string SliderName;

    private IDeltaSliderState _slider;
    private long lastReceived = 0;

    public bool Enabled
    {
        get
        {
            var root = NetworkControlReceiver.Instance;
            if (root != null && root.Receiver.PacketsReceived > lastReceived)
                _slider = root.Receiver.GetSliderState(SliderName);

            return _slider != null;
        }
    }
    public int Value { get { return Enabled ? _slider.Value : 0; } }
    public int MaxValue { get { return Enabled ? _slider.MaxValue : 0; } }
    public int MinValue { get { return Enabled ? _slider.MinValue : 0; } }
    public double Acceleration { get { return Enabled ? _slider.Acceleration : 0; } }
    public double Velocity { get { return Enabled ? _slider.Velocity : 0; } }
    public int LastValue { get { return Enabled ? _slider.LastValue : 0; } }
    public float ValueNorm
    {
        get
        {
            return Enabled ?
                ((float)(_slider.Value + _slider.MinValue) / (_slider.MaxValue - _slider.MinValue))
                : 0;
        }
    }
    public float LastValueNorm
    {
        get
        {
            return Enabled ?
                ((float)(_slider.LastValue + _slider.MinValue) / (_slider.MaxValue - _slider.MinValue))
                : 0;
        }
    }

}
