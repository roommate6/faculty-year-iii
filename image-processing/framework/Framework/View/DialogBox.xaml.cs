using System.Windows;
using System.Collections.Generic;

using Framework.ViewModel;
using System.ComponentModel;
using Framework.Utilities;

namespace Framework.View
{
    public partial class DialogBox : Window
    {
        private readonly DialogBoxVM _dialogBoxVM;

        public DialogBox(MainVM mainVM, List<string> parameters)
        {
            InitializeComponent();

            _dialogBoxVM = new DialogBoxVM();

            _dialogBoxVM.Theme = mainVM.Theme;
            _dialogBoxVM.CreateParameters(parameters);

            DataContext = _dialogBoxVM;
        }

        public List<string> GetValues()
        {
            return _dialogBoxVM.GetValues();
        }

        public byte InputSize
        {
            get
            {
                return _dialogBoxVM.InputSize;
            }
        }
    }
}