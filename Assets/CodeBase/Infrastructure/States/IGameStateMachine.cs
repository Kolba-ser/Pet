using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure
{
    public interface IGameStateMachine  : IService
    {
        void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>;
        void Enter<TState>() where TState : class, IState;
        TState GetState<TState>() where TState : class, IExitableState;
    }
}