using HarmonyLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;
using System.Reflection.Emit;

namespace BepInEx.Harmony.Tests
{
	[TestClass]
	public class HarmonyWrapperTests
	{
		[TestMethod]
		public void EmitDelegateTest()
		{
			CodeInstruction instruction = HarmonyWrapper.EmitDelegate<Action>(StaticAssets.TestStaticMethod);

			Assert.AreEqual(OpCodes.Call, instruction.opcode);
			Assert.IsTrue(instruction.operand is MethodInfo);

			instruction = HarmonyWrapper.EmitDelegate<Action>(() => StaticAssets.TestStaticField = 5);

			Assert.AreEqual(OpCodes.Call, instruction.opcode);
			Assert.IsTrue(instruction.operand is MethodInfo);

			CompileInstruction(instruction)();

			Assert.AreEqual(5, StaticAssets.TestStaticField);

			int dummy = 0;

			instruction = HarmonyWrapper.EmitDelegate<Action>(() => dummy = 15);

			Assert.AreEqual(OpCodes.Call, instruction.opcode);
			Assert.IsTrue(instruction.operand is MethodInfo);

			CompileInstruction(instruction)();

			Assert.AreEqual(15, dummy);
		}

		private static Action CompileInstruction(CodeInstruction instruction)
		{
			return (Action)((DynamicMethod)instruction.operand).CreateDelegate(typeof(Action));
		}
	}
}