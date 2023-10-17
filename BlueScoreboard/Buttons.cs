using System;
using SharpDX.DirectInput;
using Usb.GameControllers;
namespace BlueScoreboard
{
    public class Buttons
    {
        String[] buttonString;
        int team1Score;
        int team2Score;
        DirectInput directInput;
        Joystick joystick;

        public Buttons(int team1Score, int team2Score)
        {
            this.team1Score = team1Score;
            this.team2Score = team2Score;
            buttonString = new string[2];
            this.directInput = new DirectInput();
            this.joystick = SetupController();

            while (true)
            { ButtonLogic(); }
        }

        public Buttons()
        {
            new Buttons(0, 0);
        }


        public Joystick SetupController()
        {
            // Initialize DirectInput
            Console.WriteLine("Hello World!");

            // Find a Joystick Guid
            var joystickGuid = Guid.Empty;

            foreach (var deviceInstance in directInput.GetDevices(DeviceType.Gamepad,
                        DeviceEnumerationFlags.AllDevices))
                joystickGuid = deviceInstance.InstanceGuid;

            // If Gamepad not found, look for a Joystick
            if (joystickGuid == Guid.Empty)
                foreach (var deviceInstance in directInput.GetDevices(DeviceType.Joystick,
                        DeviceEnumerationFlags.AllDevices))
                    joystickGuid = deviceInstance.InstanceGuid;

            // If Joystick not found, throws an error
            if (joystickGuid == Guid.Empty)
            {
                Console.WriteLine("No joystick/Gamepad found.");
                Console.ReadKey();
                Environment.Exit(1);
            }

            // Instantiate the joystick
            var joystick = new Joystick(directInput, joystickGuid);

            Console.WriteLine("Found Joystick/Gamepad with GUID: {0}", joystickGuid);

            // Query all suported ForceFeedback effects
            var allEffects = joystick.GetEffects();
            foreach (var effectInfo in allEffects)
                Console.WriteLine("Effect available {0}", effectInfo.Name);

            // Set BufferSize in order to use buffered data.
            joystick.Properties.BufferSize = 128;

            // Acquire the joystick
            joystick.Acquire();

            return joystick;

        }

        public void ButtonLogic()
        {
            string output;

            this.joystick.Poll();
            var datas = this.joystick.GetBufferedData();
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
                            case (int)PlayerButtons.Team1Button0:
                                team1Score++;
                                break;
                            case (int)PlayerButtons.Team1Button1:
                                team1Score = (team1Score > 0) ? --team1Score : 0;
                                break;
                            case (int)PlayerButtons.Team1Button2:
                                //reset
                                break;
                            case (int)PlayerButtons.Team1Button3:
                                //new game
                                break;
                            case (int)PlayerButtons.Team1Button4:
                                //new game
                                break;
                            case (int)PlayerButtons.Team1Button5:
                                //new game
                                break;
                            #endregion
                            #region "Team 2 Buttons"
                            case (int)PlayerButtons.Team2Button0:
                                team2Score++;
                                break;
                            case (int)PlayerButtons.Team2Button1:
                                team1Score = (team1Score > 0) ? --team1Score : 0;
                                break;
                            case (int)PlayerButtons.Team2Button2:
                                //reset
                                break;
                            case (int)PlayerButtons.Team2Button3:
                                //new game
                                break;
                            case (int)PlayerButtons.Team2Button4:
                                //new game
                                break;
                            case (int)PlayerButtons.Team2Button5:
                                //new game
                                break;
                                #endregion
                        }


                        buttonString = state.Offset.ToString().Split("s");
                        output = "pressed";
                        Console.WriteLine(buttonString[0] + " " + buttonString[1] + " " + output + "||  Team 1:" + team1Score + " -- Team 2:" +team2Score);

                    }
                    #endregion
                    else if (state.Value.Equals(0)) { }//button release
                #region "Button Release"

                #endregion
                #endregion
            }
        }


        static void Main()
        {
         new Buttons();
        }



    }
}
    

    
