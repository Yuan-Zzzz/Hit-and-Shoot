using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{
   private static InputSystem input = new InputSystem();
    public static Vector2 Move => input.Gameplay.Move.ReadValue<Vector2>();
    public static Vector2 MousePos => input.Gameplay.MousePosition.ReadValue<Vector2>();
    public static bool Shoot => input.Gameplay.Shoot.WasPerformedThisFrame();
    //��������ϵͳ
    public static void OnEnable()
    {
        input.Enable();
    }
}
