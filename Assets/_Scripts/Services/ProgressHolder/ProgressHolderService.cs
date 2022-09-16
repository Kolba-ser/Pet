using Pet.Data;

namespace Pet.Service.Progress
{
    public class ProgressHolderService : IProgressHolderService
    {
        public PlayerProgress Progress
        {
            get; set;
        }

        public void NewProgress()
        {
            Progress = new PlayerProgress(initialLevel: "MainMenu");

            Progress.HeroState.MaxHealth = 50;
            Progress.HeroState.ResetHealth();
            Progress.HeroStats.Damage = 20;
            Progress.HeroStats.Radius = 1f;
        }
    }
}