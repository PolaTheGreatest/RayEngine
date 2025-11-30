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
		public static RENDER_MODE RenderMode { get; } = RENDER_MODE.TWO_DIMENSIONAL;
	}
}