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
    public static class PasswordBoxHelper
    {
        public static readonly DependencyProperty BoundPasswordProperty =
            DependencyProperty.RegisterAttached("BoundPassword", typeof(SecureString), typeof(PasswordBoxHelper),
                new PropertyMetadata(null));

        public static SecureString GetBoundPassword(DependencyObject d) =>
            (SecureString)d.GetValue(BoundPasswordProperty);

        public static void SetBoundPassword(DependencyObject d, SecureString value) =>
            d.SetValue(BoundPasswordProperty, value);

        public static readonly DependencyProperty AttachProperty =
            DependencyProperty.RegisterAttached("Attach", typeof(bool), typeof(PasswordBoxHelper),
                new PropertyMetadata(false, OnAttachChanged));

        public static bool GetAttach(DependencyObject dp) =>
            (bool)dp.GetValue(AttachProperty);

        public static void SetAttach(DependencyObject dp, bool value) =>
            dp.SetValue(AttachProperty, value);

        private static void OnAttachChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                if ((bool)e.NewValue)
                {
                    passwordBox.PasswordChanged += (s, args) =>
                    {
                        SetBoundPassword(passwordBox, passwordBox.SecurePassword.Copy());
                    };
                }
            }
        }
    }
}
