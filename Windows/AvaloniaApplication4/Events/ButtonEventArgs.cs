namespace EventArgs
{
    public class ButtonEventArgs
    {
        public ButtonEventArgs(byte b, bool p) {
            this.Button = b;
            this.Pressed = p;
        }

        public ButtonEventArgs(string off, bool p)
        {
            this.offset= off;
            this.Pressed = p;
        }
        public byte Button { get; set; }
        public bool Pressed { get; set; }

        public string offset;
    }
}
