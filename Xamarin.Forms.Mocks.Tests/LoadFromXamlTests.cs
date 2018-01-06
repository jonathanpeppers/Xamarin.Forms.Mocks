﻿using NUnit.Framework;
using Xamarin.Forms.Xaml;

namespace Xamarin.Forms.Mocks.Tests
{
    [TestFixture]
    public class LoadFromXamlTests
    {
        [SetUp]
        public void SetUp()
        {
            MockForms.Init();
        }

        [Test]
        public void LoadFromXaml()
        {
            var label = new Label();
            label.LoadFromXaml("<Label Text=\"Woot\" />");
            Assert.AreEqual("Woot", label.Text);
        }

        [Test]
        public void MarkupExtension()
        {
            var label = new Label();
            label.LoadFromXaml("<Label xmlns:f=\"clr-namespace:Xamarin.Forms.Mocks.Tests;assembly=Xamarin.Forms.Mocks.Tests\" Text=\"{f:Terrible}\" />");
            Assert.AreEqual("2016", label.Text);
        }

        [Test]
        public void LoadViaNew()
        {
            var view = new TestView();
            var label = (Label)view.Content;
            Assert.AreEqual("Woot", label.Text);
        }

        [Test]
        public void LoadViaNewCompiled()
        {
            var view = new TestViewCompiled();
            var label = (Label)view.Content;
            Assert.AreEqual("Compiled", label.Text);
        }

        [Test]
        public void NativeViews()
        {
            var page = new ContentPage();
            page.LoadFromXaml(@"
<ContentPage xmlns=""http://xamarin.com/schemas/2014/forms""
        xmlns:x=""http://schemas.microsoft.com/winfx/2009/xaml""
        xmlns:ios=""clr-namespace:UIKit;assembly=Xamarin.iOS;targetPlatform=iOS""
        xmlns:androidWidget=""clr-namespace:Android.Widget;assembly=Mono.Android;targetPlatform=Android""
        xmlns:formsAndroid=""clr-namespace:Xamarin.Forms;assembly=Xamarin.Forms.Platform.Android;targetPlatform=Android""
        xmlns:win=""clr-namespace:Windows.UI.Xaml.Controls;assembly=Windows, Version=255.255.255.255,
            Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime;targetPlatform=Windows""
        x:Class=""NativeViews.NativeViewDemo"">
    <StackLayout Margin=""20"">
        <ios:UILabel Text=""Hello World"" TextColor=""{x:Static ios:UIColor.Red}"" View.HorizontalOptions=""Start"" />
        <androidWidget:TextView Text=""Hello World"" x:Arguments=""{x:Static formsandroid:Forms.Context}"" />
        <win:TextBlock Text=""Hello World"" />
    </StackLayout>
</ContentPage>");

            var layout = page.Content as StackLayout;
            Assert.IsNotNull(layout);
            Assert.AreEqual(new Thickness(20), layout.Margin);
        }
    }
}
