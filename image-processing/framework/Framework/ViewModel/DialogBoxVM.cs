using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Framework.View;
using Framework.Model;
using Framework.Utilities;
using System.Windows;
using System.Drawing;

namespace Framework.ViewModel
{
    class DialogBoxVM : BaseVM
    {
        public void CreateParameters(List<string> labels)
        {
            Height = (labels.Count + 3) * 40;

            foreach (var label in labels)
            {
                Parameters.Add(new DialogBoxParameter(label, Theme.TextForeground, 20));
            }
        }

        public List<string> GetValues()
        {
            var values = new List<string>();

            foreach (var parameter in Parameters)
            {
                string value = string.Empty;
                if (parameter.Value != string.Empty)
                {
                    value = parameter.Value;
                }
                values.Add(value);
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

        #endregion
    }
}