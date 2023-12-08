using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Port_Morski
{
   public class MainViewModel : INotifyPropertyChanged
    {
        private string _nazwaKontroli;
        public event EventHandler<string> NazwaKontroliZmieniona;
        public string NazwaKontroli
        {
            get { return _nazwaKontroli; }
            set
            {
                if (_nazwaKontroli != value)
                {
                    _nazwaKontroli = value;
                    OnPropertyChanged(nameof(NazwaKontroli));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void AktualizujNazweKontroli(string nowaNazwa)
        {
            NazwaKontroli = nowaNazwa;
            NazwaKontroliZmieniona?.Invoke(this, nowaNazwa);
        }
    }
}
