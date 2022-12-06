using WindowsInput;
using SharpDX.XInput;
using WindowsInput.Native;

namespace XBoxAsMouse
{
    public class XBoxControllerAsMouse
	{
		private const int MovementDivider = 2_000;
		private const int ScrollDivider = 10_000;
		private const int RefreshRate = 60;

		private System.Threading.Timer _timer;
		private Controller _controller;
		private IMouseSimulator _mouseSimulator;
		private IKeyboardSimulator _keyboardSimulator;

		private bool _wasADown;
		private bool _wasBDown;
		private bool _wasStartDown;
		private DateTime _hotkeyLastPressed;
		public bool MouseModeEnabled;

		public XBoxControllerAsMouse()
		{
			_controller = new Controller(UserIndex.One);			
			_mouseSimulator = new InputSimulator().Mouse;
			_keyboardSimulator = new InputSimulator().Keyboard;
			_timer = new  System.Threading.Timer(obj => Update());
			_hotkeyLastPressed = new DateTime();
		}

		public void Start()
		{
			_hotkeyLastPressed = DateTime.Now;
			_timer.Change(0, 1000 / RefreshRate);
		}

		private void Update()
		{
			_controller.GetState(out var state);
			if (MouseModeEnabled)
			{
				Movement(state);
				Scroll(state);
				LeftButton(state);
				RightButton(state);
			}
			//StartButton(state);
			MonitorHotKeys(state);
		}

		private void RightButton(State state)
		{
			var isBDown = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.B);
			if (isBDown && !_wasBDown) _mouseSimulator.RightButtonDown();
			if (!isBDown && _wasBDown) _mouseSimulator.RightButtonUp();
			_wasBDown = isBDown;
		}
		
		private void StartButton(State state)
		{
			var isStartDown = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.Start);
            //if (isStartDown && !_wasStartDown) _keyboardSimulator.KeyPress(VirtualKeyCode.LEFT);			            
			if (!isStartDown && _wasStartDown)
			{
				_keyboardSimulator.KeyDown(VirtualKeyCode.LMENU);
				_keyboardSimulator.KeyPress(VirtualKeyCode.F4);
				_keyboardSimulator.KeyUp(VirtualKeyCode.LMENU);
			}
			_wasStartDown = isStartDown;
		}

		private void MonitorHotKeys(State state)
		{
			var TimeBetweenHotKeys = DateTime.Now - _hotkeyLastPressed;
			Console.WriteLine("TimeBetweenHotkeys: " + TimeBetweenHotKeys.Seconds);
			
			if (TimeBetweenHotKeys.Seconds > 1)
			{
				var isStartDown = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.Start);
				var isBackDown = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.Back);
				var isR2Down =  state.Gamepad.RightTrigger > 0;
				var isR1Down =  state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.RightShoulder);
				if (isStartDown && isR2Down) 			
				{
					_keyboardSimulator.KeyDown(VirtualKeyCode.LMENU);
					_keyboardSimulator.KeyPress(VirtualKeyCode.F4);
					_keyboardSimulator.KeyUp(VirtualKeyCode.LMENU);				
					_hotkeyLastPressed = DateTime.Now;				
				}
				if (isStartDown && isR1Down) 			
				{
					_keyboardSimulator.KeyDown(VirtualKeyCode.LCONTROL);
					_keyboardSimulator.KeyDown(VirtualKeyCode.LSHIFT);
					_keyboardSimulator.KeyPress(VirtualKeyCode.ESCAPE);
					_keyboardSimulator.KeyUp(VirtualKeyCode.LCONTROL);				
					_keyboardSimulator.KeyUp(VirtualKeyCode.LSHIFT);				
					_hotkeyLastPressed = DateTime.Now;
				}				
				if (isStartDown && isBackDown) 			
				{
					MouseModeEnabled = !MouseModeEnabled;
					_hotkeyLastPressed = DateTime.Now;
				}								
			} 
		}


		private void LeftButton(State state)
		{
			var isADown = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.A);
			if (isADown && !_wasADown) _mouseSimulator.LeftButtonDown();
			if (!isADown && _wasADown) _mouseSimulator.LeftButtonUp();
			_wasADown = isADown;			
		}

		private void Scroll(State state)
		{
			var x = state.Gamepad.RightThumbX / ScrollDivider;
			var y = state.Gamepad.RightThumbY / ScrollDivider;
			_mouseSimulator.HorizontalScroll(x);
			_mouseSimulator.VerticalScroll(y);
		}

		private void Movement(State state)
		{
			var x = state.Gamepad.LeftThumbX / MovementDivider;
			var y = state.Gamepad.LeftThumbY / MovementDivider;
			_mouseSimulator.MoveMouseBy(x, -y);
		}
	}
}