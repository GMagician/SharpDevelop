﻿// Copyright (c) 2014 AlphaSierraPapa for the SharpDevelop Team
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this
// software and associated documentation files (the "Software"), to deal in the Software
// without restriction, including without limitation the rights to use, copy, modify, merge,
// publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons
// to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
// PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Shapes;

using ICSharpCode.Core;
using ICSharpCode.Profiler.Controls;
using ICSharpCode.Profiler.AddIn.Views;

namespace ICSharpCode.Profiler.AddIn.Commands
{
	/// <summary>
	/// Description of ProfilerMenuCommand.
	/// </summary>
	public abstract class ProfilerMenuCommand : AbstractMenuCommand
	{
		public abstract override void Run();
		
		protected virtual IEnumerable<CallTreeNodeViewModel> GetSelectedItems()
		{
			if (Owner is Shape)
				yield return (Owner as Shape).Tag as CallTreeNodeViewModel;
			else {
				var fe = TryToFindParent(typeof(QueryView)) as QueryView;
				
				if (fe != null) {
					foreach (var item in fe.SelectedItems)
						yield return item;
				}
			}
		}
		
		protected virtual ProfilerView Parent {
			get {
				return TryToFindParent(typeof(ProfilerView)) as ProfilerView;
			}
		}
		
		FrameworkElement TryToFindParent(Type type)
		{
			FrameworkElement start = Owner as FrameworkElement;
			
			if (start == null)
				return null;
			
			while (start != null && !start.GetType().Equals(type))
				start = start.Parent as FrameworkElement;
			
			return start;
		}
	}
}
