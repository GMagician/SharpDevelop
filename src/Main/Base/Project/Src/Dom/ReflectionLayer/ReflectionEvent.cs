﻿// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version value="$version"/>
// </file>
using System;
using System.Collections;
using System.Reflection;
using System.Xml;

namespace ICSharpCode.SharpDevelop.Dom
{
	[Serializable]
	public class ReflectionEvent : DefaultEvent
	{
		EventInfo eventInfo;
		
		public override IReturnType ReturnType {
			get {
				return ReflectionReturnType.Create(this, eventInfo.EventHandlerType, false);
			}
			set {
			}
		}
		
		public ReflectionEvent(EventInfo eventInfo, IClass declaringType) : base(declaringType, eventInfo.Name)
		{
			this.eventInfo = eventInfo;
			
			// get modifiers
			MethodInfo methodBase = null;
			try {
				methodBase = eventInfo.GetAddMethod(true);
			} catch (Exception) {}
			
			if (methodBase == null) {
				try {
					methodBase = eventInfo.GetRemoveMethod(true);
				} catch (Exception) {}
			}
			
			ModifierEnum modifiers  = ModifierEnum.None;
			if (methodBase != null) {
				if (methodBase.IsStatic) {
					modifiers |= ModifierEnum.Static;
				}
				
				if (methodBase.IsAssembly) {
					modifiers |= ModifierEnum.Internal;
				}
				
				if (methodBase.IsPrivate) {
					// I assume that private is used most and public last (at least should be)
					modifiers |= ModifierEnum.Private;
				} else if (methodBase.IsFamily) {
					modifiers |= ModifierEnum.Protected;
				} else if (methodBase.IsPublic) {
					modifiers |= ModifierEnum.Public;
				} else if (methodBase.IsFamilyOrAssembly) {
					modifiers |= ModifierEnum.ProtectedOrInternal;
				} else if (methodBase.IsFamilyAndAssembly) {
					modifiers |= ModifierEnum.Protected;
					modifiers |= ModifierEnum.Internal;
				}
			} else {
				// assume public property, if no methodBase could be get.
				modifiers = ModifierEnum.Public;
			}
			this.Modifiers = modifiers;
		
		}
	}
}
