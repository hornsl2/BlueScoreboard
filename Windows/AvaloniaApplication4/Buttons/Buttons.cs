using System;
using System.Collections.Generic;
using System.ComponentModel;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Windows;
using SharpDX.DirectInput;
using EventArgs;
//using Usb.GameControllers;
namespace ButtonFiles { 
    public class Buttons: Control , IGamepadController
    {
        String[] buttonString;
       public int team1Score;
        public int team2Score;
        public DirectInput directInput;
        public List<Joystick> joysticks;
       public  List<string> toReturn;

        private System.Threading.Timer pollTimer;

        public Buttons(int team1Score, int team2Score)
        {
            this.team1Score = team1Score;
            this.team2Score = team2Score;
            buttonString = new string[2];
            this.directInput = new DirectInput();
            this.joysticks= SetupController();
            this.toReturn = new List<string>();

            pollTimer = new System.Threading.Timer(PollJoystick, null, 0, 100);
        }

        public Buttons() 
        {this.toReturn = new List<string>();
            this.team1Score = 0;
            this.team2Score = 0;
            buttonString = new string[2];
            this.directInput = new DirectInput();
            this.joysticks = SetupController();

            pollTimer = new System.Threading.Timer(PollJoystick, null, 0, 100);

        }

        private void PollJoystick(object state)
        {
            ButtonLogic();
        }

        /// <summary>
        /// EventHandler to allow the notification of Button changes.
        /// </summary>
        public event EventHandler<ButtonEventArgs> ButtonChanged;
       // {
      //     add => AddHandler(ButtonChangedEvent, value);
       //    remove => RemoveHandler(ButtonChangedEvent, val

        public static readonly RoutedEvent<RoutedEventArgs> ButtonChangedEvent;
        public event EventHandler<AxisEventArgs> AxisChanged;

        protected virtual void OnButtonChanged(ButtonEventArgs but)
        {
             this.toReturn.Add("routed event Button: {but.Button}  Pressed: { but.Pressed}");
          if (but.Pressed)  ButtonChanged?.Invoke(this, but);
            //RaiseEvent(args);
        }

        public List<Joystick> SetupController()
        {
            // Initialize DirectInput
            Console.WriteLine("Hello World!");

            // Find a Joystick Guid
            var joystickGuid = Guid.Empty;
            var joystickGuida = new List<Guid>();
            var joystickList = new List<Joystick>();

            foreach (var deviceInstance in directInput.GetDevices(DeviceType.Gamepad,
                        DeviceEnumerationFlags.AllDevices))
            {
                joystickGuid = deviceInstance.InstanceGuid;
                joystickGuida.Add(deviceInstance.InstanceGuid);

            }
            // If Gamepad not found, look for a Joystick
            if (joystickGuid == Guid.Empty)
                foreach (var deviceInstance in directInput.GetDevices(DeviceType.Joystick,
                        DeviceEnumerationFlags.AllDevices))
                {
                    joystickGuid = deviceInstance.InstanceGuid;
                    joystickGuida.Add(deviceInstance.InstanceGuid);
                }

            // If Joystick not found, throws an error
            if (joystickGuid == Guid.Empty)
            {
                Console.WriteLine("No joystick/Gamepad found.");
                toReturn.Add("No joystick/Gamepad found.");
              Console.ReadKey();
             //   Environment.Exit(1);
            }

            // Instantiate the joystick
            foreach (Guid joyGUId in joystickGuida)
            {
                var joystick = new Joystick(directInput, joyGUId);
                joystickList.Add(new Joystick(directInput, joyGUId));

                Console.WriteLine("Found Joystick/Gamepad with GUID: {0}", joyGUId);
                toReturn.Add(String.Format("Found Joystick/Gamepad with GUID: {0}", joyGUId));
                // Query all suported ForceFeedback effects
                var allEffects = joystick.GetEffects();
                foreach (var effectInfo in allEffects)
                {
                    Console.WriteLine("Effect available {0}", effectInfo.Name);
                    toReturn.Add(String.Format("Effect available {0}", effectInfo.Name));
                }

            }

            foreach (Joystick joystick in joystickList)
            {
                // Set BufferSize in order to use buffered data.
                joystick.Properties.BufferSize = 128;

                // Acquire the joystick
                joystick.Acquire();
            }

            return joystickList;
        }

        public List<String> ButtonLogic()
        {
          
            string output;
            foreach (Joystick joystick in joysticks)
            {
              joystick.Poll();
                var datas = joystick.GetBufferedData();
                foreach (var state in datas)
                {
                    #region "Button Logic"
                    if (state.Offset.ToString().Contains("Button"))

                        #region "Button Pressed"  
                        if (state.Value.Equals(128))//button press
                        {
                            buttonString = state.Offset.ToString().Split("s");

                            switch (Int32.Parse((state.Offset.ToString().Split("s")[1])))
                            {
                                #region "Team 1 Buttons"
                                case (int)PlayerButtons.Team1Button0://reset
                                    team1Score = 0;
                                    break;
                                case (int)PlayerButtons.Team1Button1://team 1 score ++
                                    team1Score++;
                                    break;
                                case (int)PlayerButtons.Team1Button2: // Team 1 score --
                                    team1Score = (team1Score > 0) ? --team1Score : 0;
                                    break;
                                case (int)PlayerButtons.Team1Button3:
                                    team2Score = 0;
                                    break;
                                case (int)PlayerButtons.Team1Button4:
                                   team2Score++; 
                                    break;
                                case (int)PlayerButtons.Team1Button5:
                                    team2Score = (team2Score > 0) ? --team2Score : 0;
                                    break;
                                #endregion
                                #region "Team 2 Buttons"
                                case (int)PlayerButtons.Team2Button0:
                                    team1Score++;
                                    break;
                                case (int)PlayerButtons.Team2Button1:
                                    team1Score = (team1Score > 0) ? --team1Score : 0;
                                    break;
                                case (int)PlayerButtons.Team2Button2:
                                    team2Score++;
                                    break;
                                case (int)PlayerButtons.Team2Button3:
                                    team2Score = (team2Score > 0) ? --team1Score : 0;

                                    break;
                                case (int)PlayerButtons.Team2Button4:

                                    break;
                                case (int)PlayerButtons.Team2Button5:

                                    break;
                                    #endregion
                            }
                            OnButtonChanged(new ButtonEventArgs(state.Offset.ToString(), true));

                            buttonString = state.Offset.ToString().Split("s");
                            output = "pressed";
                            Console.WriteLine(buttonString[0] + " " + buttonString[1] + " " + output + "||  Team 1:" + team1Score + " -- Team 2:" + team2Score);
                            toReturn.Add(buttonString[0] + " " + buttonString[1] + " " + output + "||  Team 1:" + team1Score + " -- Team 2:" + team2Score +" Joy:" +joystick.NativePointer );
                        }
                        #endregion
                        else if (state.Value.Equals(0)) { }//button release
                    #region "Button Release"

                    #endregion
                    #endregion



                }//end forreach press
            }//end forreach stick
            return toReturn;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }



        // static void Main()
        //    {
        //    new Buttons();
        //   }



    }
}
    

    
