using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfNetFramework.Lib
{
    public static class IconLoader
    {
        /// <summary>
        /// Loads an icon as an ImageSource for WPF controls (Image, Window.Icon, etc).
        /// </summary>
        public static ImageSource LoadAsImageSource(string relativePath)
        {
            var uri = new Uri($"pack://application:,,,/{Assembly.GetEntryAssembly().GetName().Name};component/{relativePath}", UriKind.Absolute);
            return BitmapFrame.Create(uri);
        }

        /// <summary>
        /// Loads an icon as a System.Drawing.Icon (useful for interop, tray icons, etc).
        /// </summary>
        public static Icon LoadAsIcon(string relativePath)
        {
            var stream = Application.GetResourceStream(
                new Uri($"/{Assembly.GetEntryAssembly().GetName().Name};component/{relativePath}", UriKind.Relative))
                ?.Stream;

            return stream != null ? new Icon(stream) : null;
        }
    }
}
