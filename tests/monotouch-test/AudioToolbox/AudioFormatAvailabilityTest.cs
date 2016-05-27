//
// Unit tests for AudioFormatAvailabilityTest
//
// Authors:
//	Marek Safar (marek.safar@gmail.com)
//
// Copyright 2012 Xamarin Inc. All rights reserved.
//

#if !__WATCHOS__

#if XAMCORE_2_0
using Foundation;
using AudioToolbox;
#else
using MonoTouch.Foundation;
using MonoTouch.AudioToolbox;
#endif
using NUnit.Framework;

namespace MonoTouchFixtures.AudioToolbox {

	[TestFixture]
	[Preserve (AllMembers = true)]
	public class AudioFormatAvailabilityTest
	{
		[Test]
		public void GetDecoders ()
		{
			Assert.IsNotNull (AudioFormatAvailability.GetDecoders (AudioFormatType.LinearPCM));
		}

		[Test]
		public void GetEncoders ()
		{
			Assert.IsNotNull (AudioFormatAvailability.GetEncoders (AudioFormatType.AC3));
		}
	}
}

#endif // !__WATCHOS__
