using ButtonFiles;
using EventArgs;
using System;

namespace ButtonFiles
{
    public interface IGamepadController : IDisposable
    {
        event EventHandler<ButtonEventArgs> ButtonChanged;
        event EventHandler<AxisEventArgs> AxisChanged;
    }
}