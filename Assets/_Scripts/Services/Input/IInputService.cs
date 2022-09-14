using Pet.Services;
using UnityEngine;

namespace Pet.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 Axis
        {
            get;
        }

        bool IsAttackButtonUp();
    }
}