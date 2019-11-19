using System;
using Microsoft.VisualBasic;
using System.Collections;
using System.Collections.Generic;
//using System.Data;
using System.Diagnostics;
using SwinGameSDK;


public class Instruction
{
	private const int INST_LEFT = 10;

	public static void HandleInstruction ()
	{
		if (SwinGame.MouseClicked (MouseButton.LeftButton) || SwinGame.KeyTyped (KeyCode.vk_ESCAPE) || SwinGame.KeyTyped (KeyCode.vk_RETURN)) {
			GameController.EndCurrentState ();
		}
	}
	public static void DrawInstruction ()
	{
		const int INST_HEADING = 40;
		SwinGame.DrawText ("Instruction", Color.White, GameResources.GameFont ("Courier"), INST_LEFT, INST_HEADING);
	}

	public static void ViewInstruction ()
	{

		GameController.AddNewState (GameState.ViewingInstruction);
		UtilityFunctions.DrawBackground ();
		DrawInstruction ();
	}
}