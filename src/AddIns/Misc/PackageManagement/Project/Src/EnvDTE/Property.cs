﻿// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using ICSharpCode.SharpDevelop.Project;

namespace ICSharpCode.PackageManagement.EnvDTE
{
	public class Property
	{
		IPackageManagementProjectService projectService;
		MSBuildBasedProject project;
		string name;
		
		public Property(MSBuildBasedProject project, string name)
			: this(project, name, new PackageManagementProjectService())
		{
		}
		
		public Property(MSBuildBasedProject project, string name, IPackageManagementProjectService projectService)
		{
			this.project = project;
			this.name = name;
			this.projectService = projectService;
		}
		
		public object Value {
			get { return project.GetEvaluatedProperty(name); }
			set {
				bool escapeValue = false;
				project.SetProperty(name, value as string, escapeValue);
				SaveProject();
			}
		}
		
		void SaveProject()
		{
			projectService.Save(project);
		}
	}
}
