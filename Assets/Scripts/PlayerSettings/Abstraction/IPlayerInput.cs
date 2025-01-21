using UnityEngine;

namespace PlayerSettings.Abstraction
{
    public interface IPlayerInput
    {
        Vector2 GetMoveInput();
        bool IsInteractPressed();
        bool IsDropPressed();
    }
}