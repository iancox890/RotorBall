namespace PsychedelicGames.RotorBall.UI
{
    using LevelManagement;

    /// <summary>
    /// Loads the next level when clicked.
    /// </summary>
    public class NextLevelButton : UIButton
    {
        protected override void OnClicked()
        {
            LevelData[] data = TierData.Current.Data;
            LevelData current = LevelData.Current;

            for (int i = 0; i < 29; i++)
            {
                if (current.Equals(data[i]))
                {
                    SceneLoader.Load(data[i + 1].Scene);
                }
            }
        }
    }
}
