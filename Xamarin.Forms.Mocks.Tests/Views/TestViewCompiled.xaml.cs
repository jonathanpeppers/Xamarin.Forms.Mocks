using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
