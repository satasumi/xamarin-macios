﻿//
// Unit tests for GKPolygonObstacle
//
// Authors:
//	Alex Soto <alex.soto@xamarin.com>
//	
//
// Copyright 2015 Xamarin Inc. All rights reserved.
//

#if !__WATCHOS__

using System;
using OpenTK;

#if XAMCORE_2_0
using Foundation;
using GameplayKit;
#else
using MonoTouch.Foundation;
using MonoTouch.GameplayKit;
using nuint = System.UInt32;
#endif
using NUnit.Framework;

namespace MonoTouchFixtures.GamePlayKit {

	[TestFixture]
	[Preserve (AllMembers = true)]
	public class GKPolygonObstacleTests {

		Vector2[] points = new Vector2[] { 
			new Vector2 (0,0), new Vector2 (0,1), new Vector2 (0,2), new Vector2 (0,3),
			new Vector2 (1,0), new Vector2 (1,1), new Vector2 (1,2), new Vector2 (1,3)
		};

		[Test]
		public void FromPointsTest ()
		{
			if (!TestRuntime.CheckSystemAndSDKVersion (9, 0))
				Assert.Ignore ("Ignoring GameplayKit tests: Requires iOS9+");

			var obstacle = GKPolygonObstacle.FromPoints (points);
			Assert.NotNull (obstacle, "GKPolygonObstacle.FromPoints should not be null");

			var count = obstacle.VertexCount;
			Assert.AreEqual (points.Length, (int)count, "GKPolygonObstacle lengt should be equal");

			for (nuint i = 0; i < count; i++)
				Assert.AreEqual (points [(int)i], obstacle.GetVertex (i), "GKPolygonObstacle vectors should be equal");
		}

		[Test]
		public void InitWithPointsTest ()
		{
			if (!TestRuntime.CheckSystemAndSDKVersion (9, 0))
				Assert.Ignore ("Ignoring GameplayKit tests: Requires iOS9+");

			var obstacle = new GKPolygonObstacle (points);
			Assert.NotNull (obstacle, "GKPolygonObstacle ctor should not be null");

			var count = obstacle.VertexCount;
			Assert.AreEqual (points.Length, (int)count, "GKPolygonObstacle lengt should be equal");

			for (nuint i = 0; i < count; i++)
				Assert.AreEqual (points [(int)i], obstacle.GetVertex (i), "GKPolygonObstacle vectors should be equal");
		}
	}
}

#endif // __WATCHOS__
