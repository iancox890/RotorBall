using UnityEngine;
using System;

namespace PsychedelicGames.RotorBall.Boosts
{
    using Files;

    /// <summary>
    /// Parent class for any boost.
    /// </summary>
    [System.Serializable]
    public abstract class Boost : ScriptableObject
    {
        [SerializeField] protected float[] boosts = new float[MaxLevel + 1];
        [SerializeField] private int[] rpForUpgrade = new int[MaxLevel];
        [Space]
        [SerializeField] private int rpForStock;
        [Space]
        [SerializeField] private string title;
        [SerializeField] private string description;
        [Space]
        [SerializeField] private Sprite icon;

        public const int MaxLevel = 10;
        public const int MaxStock = 1000;

        protected BoostFile boostFile;

        public string Title { get => title; }
        public string Description
        {
            get
            {
                int index = GetLevel();

                if (description.Contains("<variable>"))
                {
                    return description.Replace("<variable>", boosts[index].ToString());
                }
                else if (description.Contains("<percent>"))
                {
                    return description.Replace("<percent>", (boosts[index] * 100).ToString() + "%");
                }
                else
                {
                    return description;
                }
            }
        }

        public Sprite Icon { get => icon; }
        public int RpForStock { get => rpForStock; }

        public static event Action OnUpgraded;
        public static event Action OnStocked;

        public event Action OnUnaffordable;
        public event Action OnMaxed;

        public abstract void Activate(Gameplay.Ball ball);

        public bool TutorialNeeded()
        {
            boostFile = FileUtility.GetFile<BoostFile>(name);
            return GetStock() > 0 && !boostFile.TutorialComplete;
        }
        public void CompleteTutorial()
        {
            boostFile = FileUtility.GetFile<BoostFile>(name);
            boostFile.TutorialComplete = true;
            FileUtility.OverwriteFile(boostFile, name);

            //TutorialFile.CompleteBoostTutorial(this);
        }

        public void UpdateStock()
        {
            boostFile = FileUtility.GetFile<BoostFile>(name);
            boostFile.Stock--;
            FileUtility.OverwriteFile(boostFile, name);
        }

        public float GetBoostValue(int index) => boosts[index];
        public int GetRpValue(int index) => rpForUpgrade[index];

        public void Upgrade()
        {
            if (IsPurchasable(0, true))
            {
                boostFile = FileUtility.GetFile<BoostFile>(name);
                boostFile.Level++;

                FileUtility.OverwriteFile(boostFile, name);

                if (boostFile.Level == MaxLevel)
                {
                    OnMaxed?.Invoke();
                }

                OnUpgraded?.Invoke();
            }
        }
        public void AddPrize() => AddPrize(1);
        public void AddPrize(int amount)
        {
            boostFile = FileUtility.GetFile<BoostFile>(name);
            boostFile.Stock = Mathf.Min(MaxStock, boostFile.Stock + amount); // (boostFile.Stock < MaxStock) ? boostFile.Stock + amount : MaxStock;
            FileUtility.OverwriteFile(boostFile, name);
            
            if (boostFile.Stock == MaxStock)
            {
                OnMaxed?.Invoke();
            }
            OnStocked?.Invoke();

            //if (boostFile.Stock == MaxStock)
            //{
            //    OnMaxed?.Invoke();
            //}

            //OnStocked?.Invoke();
        }

        public void AddStock()
        {
            if (IsPurchasable(1, true))
            {
                boostFile = FileUtility.GetFile<BoostFile>(name);
                boostFile.Stock = (boostFile.Stock < MaxStock) ? boostFile.Stock + 1 : MaxStock;

                FileUtility.OverwriteFile(boostFile, name);

                if (boostFile.Stock == MaxStock)
                {
                    OnMaxed?.Invoke();
                }

                OnStocked?.Invoke();
            }
        }

        public bool IsActivatable()
        {
            boostFile = FileUtility.GetFile<BoostFile>(name);
            int stock = boostFile.Stock;

            if (stock > 0)
            {
                return true;
            }
            return false;
        }
        

        public int GetPreviousUpgradePrice()
        {
            int level = GetLevel();
            if (level == 0) {
                return rpForUpgrade[level];
            } else {
                return rpForUpgrade[level-1];
            }
        }

        /// <summary>
        /// Returns whether or not we can upgrade this boost or add to stock.
        /// </summary>
        /// <param name="id">0 for upgrade, 1 for stock.</param>
        /// <returns></returns>
        public bool IsPurchasable(int id, bool purchaseIfApplicable)
        {
            PlayerFile playerFile = PlayerFile.GetFile();

            if (id == 0)
            {
                int level = GetLevel();

                if (level != MaxLevel)
                {
                    int rp = rpForUpgrade[GetLevel()];

                    if (playerFile.RotorPoints >= rp)
                    {
                        if (purchaseIfApplicable)
                        {
                            playerFile.RotorPoints -= rp;
                            FileUtility.OverwriteFile(playerFile, PlayerFile.FileName);
                        }
                        return true;
                    }
                }
                OnUnaffordable?.Invoke();
                return false;
            }
            else
            {
                int stock = GetStock();

                if (stock != MaxStock)
                {
                    if (playerFile.RotorPoints >= rpForStock)
                    {
                        if (purchaseIfApplicable)
                        {
                            StatisticsFile statisticsFile = StatisticsFile.File;
                            statisticsFile.RPSpent += rpForStock;
                            StatisticsFile.File = statisticsFile;

                            playerFile.RotorPoints -= rpForStock;
                            FileUtility.OverwriteFile(playerFile, PlayerFile.FileName);
                        }
                        return true;
                    }
                }
                OnUnaffordable?.Invoke();
                return false;
            }
        }

        /// <summary>
        /// Returns whether or not we are fully upgraded or fully stocked.
        /// </summary>
        /// <param name="id">0 for upgrade, 1 for stock.</param>
        /// <returns></returns>
        public bool IsMaxed(int id)
        {
            boostFile = FileUtility.GetFile<BoostFile>(name);
            if (id == 0)
            {
                if (boostFile.Level == MaxLevel)
                {
                    OnMaxed?.Invoke();
                    return true;
                }
                return false;
            }
            else
            {
                if (boostFile.Stock == MaxStock)
                {
                    OnMaxed?.Invoke();
                    return true;
                }
                return false;
            }
        }

        public int GetLevel() => FileUtility.GetFile<BoostFile>(name).Level;
        public int GetStock() {
            BoostFile file = FileUtility.GetFile<BoostFile>(name);
            IsMaxed(1);
            return file.Stock;
        }
    }
}
