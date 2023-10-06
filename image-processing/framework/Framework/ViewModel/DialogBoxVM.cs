using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Framework.View;
using Framework.Model;
using Framework.Utilities;
using System.Windows;

namespace Framework.ViewModel
{
    class DialogBoxVM : BaseVM
    {
        public void CreateParameters(List<string> texts)
        {
            Height = (texts.Count + 3) * 40;

            foreach (var text in texts)
            {
                Parameters.Add(new DialogBoxParameter()
                {
                    ParamText = text,
                    Foreground = Theme.TextForeground,
                    Height = 20,
                });
            }
        }

        public List<string> GetValues()
        {
            var values = new List<string>();

            foreach (var parameter in Parameters)
            {
                values.Add(parameter.InputText);
            }

            return values;
        }

        #region Properties and commands
        public double Height { get; set; }

        public IThemeMode Theme { get; set; } =
            LimeGreenTheme.Instance;

        public ObservableCollection<DialogBoxParameter> Parameters { get; } =
            new ObservableCollection<DialogBoxParameter>();

        private ICommand _submitCommand;
        public ICommand SubmitCommand
        {
            get
            {
                if (_submitCommand == null)
                    _submitCommand = new RelayCommand(p =>
                    {
                        DataProvider.CloseWindow<DialogBox>(true);
                    });

                return _submitCommand;
            }
        }

        public byte InputSize
        {
            get
            {
                byte size = 0;

                foreach (var parameter in Parameters)
                {
                    if (parameter.InputText != null && parameter.InputText != string.Empty)
                    {
                        ++size;
                    }
                }

                return size;
            }
        }

        #endregion
    }
}