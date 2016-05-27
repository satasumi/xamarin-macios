using System;
using System.Threading.Tasks;
using NUnit.Framework;

#if !XAMCORE_2_0
using MonoMac.AppKit;
using MonoMac.CoreAnimation;
using MonoMac.CoreGraphics;
using MonoMac.Foundation;
using CGRect = System.Drawing.RectangleF;
#else
using AppKit;
using CoreAnimation;
using CoreGraphics;
using Foundation;
#endif

namespace Xamarin.Mac.Tests
{
	[TestFixture]
	public class CAKeyFrameAnimationTests
	{
		[Test]
		public void CAKeyFrameAnimation_ValuesTests ()
		{
			CAKeyFrameAnimation keyFrameAni = new CAKeyFrameAnimation();
			keyFrameAni.Values = new NSObject [] { new NSNumber (5) };
			Assert.AreEqual (1, keyFrameAni.Values.Length);
			NSNumber arrayNumber = (NSNumber)keyFrameAni.Values[0];
			Assert.AreEqual (5, arrayNumber.Int32Value);

			CGRect frame = new CGRect (10, 10, 0, 0);
			CGImage image = CGImage.ScreenImage (0, frame);

			keyFrameAni.SetValues (new CGImage [] {image, image});
			Assert.AreEqual (2, keyFrameAni.Values.Length);
			CGImage arrayImage = (CGImage)keyFrameAni.GetValuesAs<CGImage> ()[1];
			Assert.AreEqual (image.Handle, arrayImage.Handle);
		}
	}
}

