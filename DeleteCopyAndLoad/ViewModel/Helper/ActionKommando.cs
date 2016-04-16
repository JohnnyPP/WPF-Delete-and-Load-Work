using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MVVMMaschine
{
    /// <summary>
    /// Helper-Klasse für die Abstraktion des ICommand-Interfaces
    /// </summary>
    public class ActionCommando : ICommand
    {
        private Action actAktion;
        
        public ActionCommando(Action Anweisung)
        {
            actAktion = Anweisung;
        }

        private bool _bolIsEnabled = true;
        public bool IsEnabled
        {
            get { return _bolIsEnabled; }
            set
            {
                _bolIsEnabled = value;
                if (CanExecuteChanged != null)
                {
                    CanExecuteChanged(this, EventArgs.Empty);
                }
            }
        }

        public string AnzeigeName { get; set; }

        #region ICommand-Implementierung
        public bool CanExecute(object parameter)
        {
            return this.IsEnabled;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (actAktion != null)
            {
                actAktion();
            }
        }
        #endregion        
        
    }
}
