using UnityEngine;

namespace PlayerSettings.Abstraction
{
    public interface IPlayerInput
    {
        Vector2 GetMoveInput();
        Vector2 GetLookInput();
    }
}