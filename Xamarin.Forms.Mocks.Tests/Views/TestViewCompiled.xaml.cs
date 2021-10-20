using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Xamarin.Forms.Mocks.Tests
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestViewCompiled : ContentView
    {
        public TestViewCompiled()
        {
            InitializeComponent();
        }
    }
}
