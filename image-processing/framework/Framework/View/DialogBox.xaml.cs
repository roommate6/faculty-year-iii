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

        #region Properties

        public List<string> Values
        {
            get
            {
                return _dialogBoxVM.GetValues();
            }
        }

        public int AmountOfNonemptyValues
        {
            get
            {
                List<string> values = Values;

                int size = 0;

                foreach(string value in values)
                {
                    if (value != string.Empty)
                    {
                        ++size;
                    }
                }

                return size;
            }
        }

        #endregion

        public DialogBox(MainVM mainVM, List<string> parameters)
        {
            InitializeComponent();

            _dialogBoxVM = new DialogBoxVM();

            _dialogBoxVM.Theme = mainVM.Theme;
            _dialogBoxVM.CreateParameters(parameters);

            DataContext = _dialogBoxVM;
        }
    }
}