using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfNetFramework.Lib
{
    /* PasswordBoxHelper
    
    Description: A helper class for creating the BoundPassword dependency property
    Source: ChatGPT with some manual modification

    XAML Usage
        <PasswordBox
            local:PasswordBoxHelper.Attach="True"
            local:PasswordBoxHelper.BoundPassword="{Binding Password, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}" />

    How it works:
    1) local:PasswordBoxHelper.Attach="True"
        - triggers OnAttachChanged, which attaches the PasswordChanged event handler
    2) PasswordChanged fires when the user changes the password
    3) OnBoundPasswordChanged fires when the view model sets value bound to "BoundPassword"
    4) To prevent an infinite loop between PasswordChanged and OnBoundPasswordChanged, 
       we use an IsAlreadyUpdating property.
    */
    public static class PasswordBoxHelper
    {
        private static readonly DependencyProperty IsAlreadyUpdatingProperty =
            DependencyProperty.RegisterAttached(
                "IsAlreadyUpdating",
                typeof(bool),
                typeof(PasswordBoxHelper),
                new PropertyMetadata(false)
            );

        public static readonly DependencyProperty BoundPasswordProperty =
            DependencyProperty.RegisterAttached(
                "BoundPassword",
                typeof(SecureString),
                typeof(PasswordBoxHelper),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnBoundPasswordChanged)
            );

        public static SecureString GetBoundPassword(DependencyObject obj)
        {
            return (SecureString)obj.GetValue(BoundPasswordProperty);
        }

        public static void SetBoundPassword(DependencyObject obj, SecureString value)
        {
            obj.SetValue(BoundPasswordProperty, value);
        }

        public static readonly DependencyProperty AttachProperty =
            DependencyProperty.RegisterAttached(
                "Attach",
                typeof(bool),
                typeof(PasswordBoxHelper),
                new PropertyMetadata(false, OnAttachChanged)
            );

        public static bool GetAttach(DependencyObject obj)
        {
            return (bool)obj.GetValue(AttachProperty);
        }

        public static void SetAttach(DependencyObject obj, bool value)
        {
            obj.SetValue(AttachProperty, value);
        }

        private static void OnAttachChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox passwordBox)
            {
                if ((bool)e.NewValue)
                {
                    passwordBox.PasswordChanged += PasswordChanged;
                }
                else
                {
                    passwordBox.PasswordChanged -= PasswordChanged;
                }
            }
        }

        private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox passwordBox)
            {
                // prevent an infinite loop with PasswordChanged
                var isUpdating = (bool)passwordBox.GetValue(IsAlreadyUpdatingProperty);
                if (isUpdating)
                {
                    return;
                }

                passwordBox.SetValue(IsAlreadyUpdatingProperty, true);
                var newSecurePassword = e.NewValue as SecureString;
                passwordBox.Password = ConvertToUnsecureString(newSecurePassword);
                passwordBox.SetValue(IsAlreadyUpdatingProperty, false);
            }
        }

        private static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                // prevent an infinite loop with OnBoundPasswordChanged
                var isUpdating = (bool)passwordBox.GetValue(IsAlreadyUpdatingProperty);
                if (isUpdating)
                {
                    return;
                }

                passwordBox.SetValue(IsAlreadyUpdatingProperty, true);
                SetBoundPassword(passwordBox, passwordBox.SecurePassword.Copy());
                passwordBox.SetValue(IsAlreadyUpdatingProperty, false);
            }
        }

        public static string ConvertToUnsecureString(SecureString securePassword)
        {
            if (securePassword == null)
                return string.Empty;

            return new System.Net.NetworkCredential(string.Empty, securePassword).Password;
        }
    }
}
