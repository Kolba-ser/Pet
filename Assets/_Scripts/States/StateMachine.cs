using Pet.Factory;
using Pet.SaveLoad;
using Pet.Service.Progress;
using Pet.Logic;
using Pet.StaticData;
using System;
using System.Collections.Generic;

namespace Pet.Infrastructure
{
    public class StateMachine : IStateMachine
    {
        private Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public StateMachine(SceneLoader sceneLoader, LoadingScreen loadingScreen, Service.Services services)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstarpState)] = new BootstarpState(this, sceneLoader, services),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingScreen, services.Single<IGameFactory>(), services.Single<IProgressHolderService>(), services.Single<ISettingsDataRegistry>()),
                [typeof(LoadProgressState)] = new LoadProgressState(this, services.Single<IProgressHolderService>(), services.Single<ISaveLoadService>()),
                [typeof(GameLoopState)] = new GameLoopState(this),
            };
        }

        public void Enter<TState>() where TState : class, IState =>
            ChangeState<TState>().Enter();

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload> =>
            ChangeState<TState>().Enter(payload);

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }

        public TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
    }
}