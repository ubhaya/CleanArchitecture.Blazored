using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Maui.MobileUi.Mobile.ViewModels.Todo;

namespace CleanArchitecture.Maui.MobileUi.Mobile.Views.Todo;

public partial class TodoListPage : ContentPage
{
    public TodoListPage(TodoListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}