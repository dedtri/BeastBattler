using CommunityToolkit.Mvvm.ComponentModel;

namespace BeastBattler.Client.Mobile.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotLoading))]
        bool isLoading;

        [ObservableProperty]
        string title;

        public bool IsNotLoading => !IsLoading;

        public BaseViewModel()
        {

        }
    }
}
