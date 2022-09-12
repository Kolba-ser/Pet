using Pet.Infrastructure.Factory;
using Pet.Infrastructure.SaveLoad;
using Pet.Infrastructure.Services.Progress;
using Pet.Logic;
using Pet.StaticData;
using Pet.UI.Services;
using System;
using System.Collections.Generic;

namespace Pet.Infrastructure
{
    public class StateMachine : IStateMachine
    {
        private Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public StateMachine(SceneLoader sceneLoader, LoadingScreen loadingScreen, Services.Services services)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstarpState)] = new BootstarpState(this, sceneLoader, services),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingScreen, services.Single<IGameFactory>(), services.Single<IProgressHolderService>(), services.Single<ISettingsDataRegistry>(), services.Single<IUIFactory>()),
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