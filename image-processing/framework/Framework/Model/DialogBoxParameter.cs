namespace Framework.Model
{
    class DialogBoxParameter
    {
        public string Label { get; set; }
        public string Value { get; set; }
        public string Foreground { get; set; }
        public double Height { get; set; }

        #region Constructors

        public DialogBoxParameter()
        {
            Label = string.Empty;
            Value = string.Empty;
            Foreground = string.Empty;
            Height = 0;
        }

        public DialogBoxParameter(string label, string foreground, double height) : this(label, string.Empty, foreground, height)
        {
            // empty
        }

        public DialogBoxParameter(string label, string value, string foreground, double height)
        {
            Label = label;
            Value = value;
            Foreground = foreground;
            Height = height;
        }

        #endregion
    }
}