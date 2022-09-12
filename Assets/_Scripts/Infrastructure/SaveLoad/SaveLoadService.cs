using Pet.Data;
using Pet.Infrastructure.Factory;
using Pet.Infrastructure.Services.Progress;
using UnityEngine;

namespace Pet.Infrastructure.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string PROGRESS_KEY = "Progress";

        private readonly IGameFactory _gameFactory;
        private readonly IProgressHolderService _progressService;

        public SaveLoadService(IGameFactory gameFactory, IProgressHolderService progressService)
        {
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public PlayerProgress LoadProgress() =>
            PlayerPrefs.GetString(PROGRESS_KEY)
            ?.ToDeserialized<PlayerProgress>();

        public void SaveProgress()
        {
            foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
                progressWriter.UpdateProgress(_progressService.Progress);

            PlayerPrefs.SetString(PROGRESS_KEY, _progressService.Progress.ToJson());
        }
    }
}