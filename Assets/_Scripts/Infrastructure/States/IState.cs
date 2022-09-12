namespace Pet.Infrastructure
{
    public interface IState : IExitableState
    {
        public void Enter();
    }

    public interface IPayloadState<TPayload> : IExitableState
    {
        public void Enter(TPayload payload);
    }

    public interface IExitableState
    {
        public void Exit();
    }
}