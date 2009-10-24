﻿// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="David Srbecký" email="dsrbecky@gmail.com"/>
//     <version>$Revision: 3230 $</version>
// </file>

using System;
using System.Collections.Generic;

namespace Debugger.Tests.TestPrograms
{
	public class DebugType_CompilerGeneratedClasses
	{
		delegate void IntDelegate(int i);
		
		public static void Main()
		{
			new List<object>(new DebugType_CompilerGeneratedClasses().MyEnum());
		}
		
		IEnumerable<object> MyEnum()
		{
			int stateFullVar = 101;
			int stateFullVar_DelegRef = 102;
			int stateFullVar_NestedDelegRef = 103;
			
			yield return stateFullVar + stateFullVar_DelegRef + stateFullVar_NestedDelegRef;
			
			{
				int stateLessVar = 201;
				int stateLessVar_DelegRef = 202;
				int stateLessVar_NestedDelegRef = 203;
				
				System.Diagnostics.Debugger.Break();
				
				IntDelegate deleg = delegate (int delegArg_NestedDelegRef) {
					int delegVar = 301;
					int delegVar_NestedDelegRef = 302;
					Console.WriteLine(stateFullVar_DelegRef);
					Console.WriteLine(stateLessVar_DelegRef);
					
					IntDelegate nestedDeleg = delegate (int nestedDelegArg) {
						int nestedDelegVar = 303;
						Console.WriteLine(delegArg_NestedDelegRef);
						Console.WriteLine(delegVar_NestedDelegRef);
						Console.WriteLine(stateFullVar_NestedDelegRef);
						Console.WriteLine(stateLessVar_NestedDelegRef);
						Console.WriteLine(this);
						
						System.Diagnostics.Debugger.Break();
					};
					
					System.Diagnostics.Debugger.Break();
					nestedDeleg(402);
				};
				deleg(401);
			}
		}
	}
}

#if TEST_CODE
namespace Debugger.Tests {
	using System.Linq;
	
	public partial class DebuggerTests
	{
		[NUnit.Framework.Test]
		public void DebugType_CompilerGeneratedClasses()
		{
			StartTest("DebugType_CompilerGeneratedClasses.cs");
			PrintLocalVariables("YieldLocalVariables");
			process.Continue();
			PrintLocalVariables("OutterDelegateLocalVariables");
			process.Continue();
			PrintLocalVariables("InnterDelegateLocalVariables");
			EndTest();
		}
	}
}
#endif

#if EXPECTED_OUTPUT
<?xml version="1.0" encoding="utf-8"?>
<DebuggerTests>
  <Test
    name="DebugType_CompilerGeneratedClasses.cs">
    <ProcessStarted />
    <ModuleLoaded>mscorlib.dll (No symbols)</ModuleLoaded>
    <ModuleLoaded>DebugType_CompilerGeneratedClasses.exe (Has symbols)</ModuleLoaded>
    <DebuggingPaused>Break DebugType_CompilerGeneratedClasses.cs:35,5-35,41</DebuggingPaused>
    <YieldLocalVariables>
      <Item>
        <LocalVariable
          Name="stateLessVar"
          Type="System.Int32"
          Value="201" />
      </Item>
      <Item>
        <LocalVariable
          Name="deleg"
          Type="Debugger.Tests.TestPrograms.DebugType_CompilerGeneratedClasses.IntDelegate"
          Value="null" />
      </Item>
      <Item>
        <LocalVariable
          Name="stateLessVar_DelegRef"
          Type="System.Int32"
          Value="202" />
      </Item>
      <Item>
        <LocalVariable
          Name="stateLessVar_NestedDelegRef"
          Type="System.Int32"
          Value="203" />
      </Item>
      <Item>
        <LocalVariable
          Name="this"
          Type="Debugger.Tests.TestPrograms.DebugType_CompilerGeneratedClasses"
          Value="{Debugger.Tests.TestPrograms.DebugType_CompilerGeneratedClasses}" />
      </Item>
      <Item>
        <LocalVariable
          Name="stateFullVar"
          Type="System.Int32"
          Value="101" />
      </Item>
      <Item>
        <LocalVariable
          Name="stateFullVar_DelegRef"
          Type="System.Int32"
          Value="102" />
      </Item>
      <Item>
        <LocalVariable
          Name="stateFullVar_NestedDelegRef"
          Type="System.Int32"
          Value="103" />
      </Item>
    </YieldLocalVariables>
    <DebuggingPaused>Break DebugType_CompilerGeneratedClasses.cs:54,6-54,42</DebuggingPaused>
    <OutterDelegateLocalVariables>
      <Item>
        <LocalVariable
          Name="delegVar"
          Type="System.Int32"
          Value="301" />
      </Item>
      <Item>
        <LocalVariable
          Name="nestedDeleg"
          Type="Debugger.Tests.TestPrograms.DebugType_CompilerGeneratedClasses.IntDelegate"
          Value="{Debugger.Tests.TestPrograms.DebugType_CompilerGeneratedClasses.IntDelegate}" />
      </Item>
      <Item>
        <LocalVariable
          Name="delegVar_NestedDelegRef"
          Type="System.Int32"
          Value="302" />
      </Item>
      <Item>
        <LocalVariable
          Name="delegArg_NestedDelegRef"
          Type="System.Int32"
          Value="401" />
      </Item>
      <Item>
        <LocalVariable
          Name="stateLessVar_DelegRef"
          Type="System.Int32"
          Value="202" />
      </Item>
      <Item>
        <LocalVariable
          Name="stateLessVar_NestedDelegRef"
          Type="System.Int32"
          Value="203" />
      </Item>
      <Item>
        <LocalVariable
          Name="stateFullVar_DelegRef"
          Type="System.Int32"
          Value="102" />
      </Item>
      <Item>
        <LocalVariable
          Name="stateFullVar_NestedDelegRef"
          Type="System.Int32"
          Value="103" />
      </Item>
      <Item>
        <LocalVariable
          Name="this"
          Type="Debugger.Tests.TestPrograms.DebugType_CompilerGeneratedClasses"
          Value="{Debugger.Tests.TestPrograms.DebugType_CompilerGeneratedClasses}" />
      </Item>
    </OutterDelegateLocalVariables>
    <DebuggingPaused>Break DebugType_CompilerGeneratedClasses.cs:51,7-51,43</DebuggingPaused>
    <InnterDelegateLocalVariables>
      <Item>
        <LocalVariable
          Name="nestedDelegVar"
          Type="System.Int32"
          Value="303" />
      </Item>
      <Item>
        <LocalVariable
          Name="delegVar_NestedDelegRef"
          Type="System.Int32"
          Value="302" />
      </Item>
      <Item>
        <LocalVariable
          Name="delegArg_NestedDelegRef"
          Type="System.Int32"
          Value="401" />
      </Item>
      <Item>
        <LocalVariable
          Name="stateLessVar_DelegRef"
          Type="System.Int32"
          Value="202" />
      </Item>
      <Item>
        <LocalVariable
          Name="stateLessVar_NestedDelegRef"
          Type="System.Int32"
          Value="203" />
      </Item>
      <Item>
        <LocalVariable
          Name="stateFullVar_DelegRef"
          Type="System.Int32"
          Value="102" />
      </Item>
      <Item>
        <LocalVariable
          Name="stateFullVar_NestedDelegRef"
          Type="System.Int32"
          Value="103" />
      </Item>
      <Item>
        <LocalVariable
          Name="this"
          Type="Debugger.Tests.TestPrograms.DebugType_CompilerGeneratedClasses"
          Value="{Debugger.Tests.TestPrograms.DebugType_CompilerGeneratedClasses}" />
      </Item>
    </InnterDelegateLocalVariables>
    <ProcessExited />
  </Test>
</DebuggerTests>
#endif // EXPECTED_OUTPUT
