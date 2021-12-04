using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Interop;

namespace SYNOPEX_ICT.Models
{
	[ExcludeFromCodeCoverage]
	public class WindowHandleMVVMBehavior : Behavior<Window>
	{
		public IntPtr WindowHandle
		{
			get { return (IntPtr)GetValue(WindowHandleProperty); }
			set { SetValue(WindowHandleProperty, value); }
		}
		public static readonly DependencyProperty WindowHandleProperty =

			DependencyProperty.Register("WindowHandle", typeof(IntPtr), typeof(WindowHandleMVVMBehavior),

				new FrameworkPropertyMetadata(IntPtr.Zero, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
		protected override void OnAttached()
		{
			base.OnAttached();
			if (AssociatedObject != null)
			{
				AssociatedObject.Loaded += OnWindowLoaded;
			}
		}
		private void OnWindowLoaded(object sender, RoutedEventArgs e)

		{
			var window = sender as Window;
			var windowHandle = GetWindowHandle(window);
			WindowHandle = windowHandle;
		}
		protected override void OnDetaching()
		{
			base.OnDetaching();
			if (AssociatedObject != null)
			{
				AssociatedObject.Loaded -= OnWindowLoaded;
			}
		}
		private IntPtr GetWindowHandle(Window window)
		{
			return window is null ? IntPtr.Zero : new WindowInteropHelper(window).Handle;
		}
	}
}
