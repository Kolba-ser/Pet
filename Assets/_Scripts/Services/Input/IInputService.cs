using Pet.Service;
using UnityEngine;

namespace Pet.Service.Input
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