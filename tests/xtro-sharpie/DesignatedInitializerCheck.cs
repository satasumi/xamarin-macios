﻿//
// The rule reports
//
// !incorrect-designated-initializer!
//		when a method, instead of a constructor, is decorated with [DesignatedInitializer] attribute
//
// !missing-designated-initializer!
//		when a managed constructor is missing its [DesignatedInitializer] attribute
//
// !extra-designated-initializer!
//		when a managed constructor has no business to have an [DesignatedInitializer] attribute
//

using System;
using System.Collections.Generic;

using Mono.Cecil;

using Clang.Ast;

namespace Extrospection {

	public class DesignatedInitializerCheck : BaseVisitor {

		static Dictionary<string,TypeDefinition> types = new Dictionary<string,TypeDefinition> ();
		static Dictionary<string,MethodDefinition> methods = new Dictionary<string,MethodDefinition> ();

		static TypeDefinition GetType (ObjCInterfaceDecl decl)
		{
			TypeDefinition td;
			types.TryGetValue (decl.Name, out td);
			return td;
		}

		static MethodDefinition GetMethod (ObjCMethodDecl decl)
		{
			MethodDefinition md;
			methods.TryGetValue (decl.GetName (), out md);
			return md;
		}


		public override void VisitManagedMethod (MethodDefinition method)
		{
			var key = method.GetName ();
			if (key == null)
				return;
			
			// we still have one case to fix with duplicate selectors :|
			if (!methods.ContainsKey (key))
				methods.Add (key, method);
		}

		public override void VisitObjCMethodDecl (ObjCMethodDecl decl, VisitKind visitKind)
		{
			if (visitKind != VisitKind.Enter)
				return;

			// don't process methods (or types) that are unavailable for the current platform
			if (!decl.IsAvailable () || !(decl.DeclContext as Decl).IsAvailable ())
				return;

			var method = GetMethod (decl);
			// don't report missing [DesignatedInitializer] for types that are not bound - that's a different problem
			if (method == null)
				return;

			var designated_initializer = method.IsDesignatedInitializer ();

			if (!method.IsConstructor) {
				if (designated_initializer)
					Console.WriteLine ("!incorrect-designated-initializer! {0} is not a constructor", method.GetName ());
			} else if (decl.IsDesignatedInitializer) {
				if (!designated_initializer)
					Console.WriteLine ("!missing-designated-initializer! {0} is missing an [DesignatedInitializer] attribute", method.GetName ());
			} else {
				if (designated_initializer)
					Console.WriteLine ("!extra-designated-initializer! {0} is incorrectly decorated with an [DesignatedInitializer] attribute", method.GetName ());
			}
		}
	}
}
