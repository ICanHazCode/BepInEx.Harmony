﻿namespace BepInEx.Harmony.Tests
{
	internal static class StaticAssets
	{
		internal static int TestStaticField = 0;

		internal static void TestStaticMethod()
		{
			int i = int.MaxValue;
			int b = i.CompareTo(i);
		}
	}
}