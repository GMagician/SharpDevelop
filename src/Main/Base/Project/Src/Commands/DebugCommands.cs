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
using System.Windows.Forms;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Editor.Bookmarks;
using ICSharpCode.SharpDevelop.Debugging;
using ICSharpCode.SharpDevelop.Editor;
using ICSharpCode.SharpDevelop.Gui;

namespace ICSharpCode.SharpDevelop.Project.Commands
{
	public class Execute : AbstractMenuCommand
	{
		protected bool withDebugger = true;
		
		public override void Run()
		{
			Build build = new BuildBeforeExecute();
			build.BuildComplete += delegate {
				if (build.LastBuildResults.ErrorCount == 0) {
					IProject startupProject = ProjectService.OpenSolution.StartupProject;
					if (startupProject != null) {
						LoggingService.Info("Debugger Command: Start (withDebugger=" + withDebugger + ")");
						startupProject.Start(withDebugger);
					} else {
						MessageService.ShowError("${res:BackendBindings.ExecutionManager.CantExecuteDLLError}");
					}
				}
			};
			build.Run();
		}
	}
	
	public class ExecuteWithoutDebugger : Execute
	{
		public override void Run()
		{
			withDebugger = false;
			base.Run();
		}
	}
	
	public class ContinueDebuggingCommand : AbstractMenuCommand
	{
		public override void Run()
		{
			if (DebuggerService.CurrentDebugger.IsDebugging) {
				LoggingService.Info("Debugger Command: Continue");
				DebuggerService.CurrentDebugger.Continue();
			} else {
				LoggingService.Info("Debugger Command: Run");
				new Execute().Run();
			}
		}
	}
	
	public class BreakDebuggingCommand : AbstractMenuCommand
	{
		public override void Run()
		{
			LoggingService.Info("Debugger Command: Break");
			DebuggerService.CurrentDebugger.Break();
		}
	}
	
	public class StopDebuggingCommand : AbstractMenuCommand
	{
		public override void Run()
		{
			LoggingService.Info("Debugger Command: Stop");
			DebuggerService.CurrentDebugger.Stop();
		}
	}
	
	public class StepDebuggingCommand : AbstractMenuCommand
	{
		public override void Run()
		{
			LoggingService.Info("Debugger Command: StepOver");
			if (!DebuggerService.CurrentDebugger.IsDebugging) {
				DebuggerService.CurrentDebugger.BreakAtBeginning = true;
				new Execute().Run();
			} else {
				DebuggerService.CurrentDebugger.StepOver();
			}
		}
	}
	
	public class StepIntoDebuggingCommand : AbstractMenuCommand
	{
		public override void Run()
		{
			LoggingService.Info("Debugger Command: StepInto");
			if (!DebuggerService.CurrentDebugger.IsDebugging) {
				DebuggerService.CurrentDebugger.BreakAtBeginning = true;
				new Execute().Run();
			} else {
				DebuggerService.CurrentDebugger.StepInto();
			}
		}
	}
	
	public class StepOutDebuggingCommand : AbstractMenuCommand
	{
		public override void Run()
		{
			LoggingService.Info("Debugger Command: StepOut");
			DebuggerService.CurrentDebugger.StepOut();
		}
	}
	
	public class ToggleBreakpointCommand : AbstractMenuCommand
	{
		public override void Run()
		{
			ITextEditor editor = SD.GetActiveViewContentService<ITextEditor>();
			
			if (editor != null) {
				if (!string.IsNullOrEmpty(editor.FileName)) {
					DebuggerService.ToggleBreakpointAt(editor, editor.Caret.Line);
				}
			}
		}
	}
	
	public class AttachToProcessCommand : AbstractMenuCommand
	{
		public override void Run()
		{
			DebuggerService.CurrentDebugger.ShowAttachDialog();
		}
	}
	
	public class DetachFromProcessCommand : AbstractMenuCommand
	{
		public override void Run()
		{
			DebuggerService.CurrentDebugger.Detach();
		}
	}
	
	public class ExecuteLastBuild : AbstractMenuCommand
	{
		public override void Run()
		{
			if (ProjectService.OpenSolution == null)
				return;
			
			if (ProjectService.OpenSolution.StartupProject == null)
				return;
			
			ProjectService.OpenSolution.StartupProject.Start(false);
		}
	}
}
