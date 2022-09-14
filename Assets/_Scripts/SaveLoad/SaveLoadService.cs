using Pet.Data;
using Pet.Factory;
using Pet.Services.Progress;
using UnityEngine;

namespace Pet.SaveLoad
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
                progressWriter.Save(_progressService.Progress);

            PlayerPrefs.SetString(PROGRESS_KEY, _progressService.Progress.ToJson());
        }
    }
}