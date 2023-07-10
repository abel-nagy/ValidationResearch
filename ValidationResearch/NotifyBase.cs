using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ValidationResearch
{
    public class NotifyBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged == null) return;
            var e = new PropertyChangedEventArgs(propertyName);
            PropertyChanged(this, e);
        }
    }
}