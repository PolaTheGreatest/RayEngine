using System;

public enum RENDER_MODE
{
    TWO_DIMENSIONAL,
	THREE_DIMENSIONAL
}

namespace RayEngine
{
	public class Config
	{
		public static int ScreenWidth { get; } = 1280;
		public static int ScreenHeight { get; } = 720;

		public static RENDER_MODE RenderMode = RENDER_MODE.TWO_DIMENSIONAL;

		public static string VersionNumber = "0.0.1";
	}
}