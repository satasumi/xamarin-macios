﻿//
// Test the generated API for all Mac CoreImage filters
//
// Authors:
//	Sebastien Pouliot  <sebastien@xamarin.com>
//	Alex Soto  <alex.soto@xamarin.com>
//
// Copyright 2013, 2015 Xamarin Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

using System;
using System.Collections.Generic;
using System.Reflection;

using NUnit.Framework;
using TouchUnit.Bindings;

#if XAMCORE_2_0
using CoreImage;
using Foundation;
using ObjCRuntime;
#elif MONOMAC
using MonoMac.CoreImage;
using MonoMac.Foundation;
using MonoMac.ObjCRuntime;
#endif

namespace MonoMacFixtures {

	[TestFixture]
	// we want the tests to be available because we use the linker
	[Preserve (AllMembers = true)]
	public class MacCoreImageFiltersTest : ApiCoreImageFiltersTest
	{
		protected override bool Skip (string nativeName)
		{
			switch (nativeName) {
			case "CIMaskedVariableBlur": // Appears removed in 10.11 but not documented
				if (Mac.CheckSystemVersion (10, 11))
					return true;
				return false;
			case "CICMYKHalftone": // Renamed as CICmykHalftone
				return true;
			default:
				return base.Skip (nativeName);
			}
		}
	}
}

