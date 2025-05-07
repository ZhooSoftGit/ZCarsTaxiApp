using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ZCarsDriver.UIHelper;
using ZCarsDriver.UIModel;
using ZhooSoft.Auth.Model;
using ZhooSoft.Controls;
using ZhooSoft.Core;

namespace ZCarsDriver.ViewModel
{
    public partial class DynamicFormViewModel : ViewModelBase
    {
        #region Fields

        private CheckListItem _checklistItem;

        [ObservableProperty]
        private ObservableCollection<FormField> _formFields;
        private object _data;

        #endregion

        #region Constructors

        public DynamicFormViewModel()
        {
            PageTitleName = "Form";
            SaveCommand = new AsyncRelayCommand(Save);
        }

        #endregion

        #region Properties

        public IAsyncRelayCommand SaveCommand { get; }

        #endregion

        #region Methods

        public override async void OnAppearing()
        {
            base.OnAppearing();
            IsBusy = true;
            if (NavigationParams != null && NavigationParams["checklist"] is CheckListItem item)
            {
                _checklistItem = item;
                PageTitleName = _checklistItem.ItemName;
                if (NavigationParams.ContainsKey("data"))
                {
                    _data = NavigationParams["data"];
                }
                LoadFormFields();
            }
            await Task.Delay(100);
            IsBusy = false;
        }

        private void LoadFormFields()
        {
            FormFields = new ObservableCollection<FormField>(FormFieldGenerator.GenerateFormFields(_checklistItem.CheckListType, _data));
        }

        private async Task Save()
        {
            var isNotValid = FormFields.ToList().Exists(x => x.IsRequired 
             && (string.IsNullOrEmpty(x.Value) && x.DateValue == null && x.IsChecked == null));
            if (isNotValid)
            {
                await _alertService.ShowAlert("Validation", "Please fill the data", "Ok");
            }
            else
            {
                //API call
                await _alertService.ShowAlert("Success", $"{_checklistItem.ItemName} is saved", "ok");
                _checklistItem.IsCompleted = true;
                await _navigationService.PopAsync();
            }
        }

        #endregion
    }
}
