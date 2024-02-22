using System;

[Serializable]
public struct UnlockableImageInventoryData
{
    public UnlockableImageSO Level;
    public string Name;
    public bool IsUnlocked;
    public float Progress;

    public UnlockableImageInventoryData(UnlockableImageSO level, bool isUnlocked, float progress)
    {
        Level = level;
        Name = level.name;
        IsUnlocked = isUnlocked;
        Progress = progress;
    }

    public UnlockableImageInventoryData ChangeProgress(float amount)
    {
        return new UnlockableImageInventoryData
        {
            Progress = this.Progress + amount,
            Level = this.Level,
            Name = this.Name,
            IsUnlocked = this.IsUnlocked
        };
    }

    public UnlockableImageInventoryData UnlockItem()
    {
        return new UnlockableImageInventoryData(this.Level, true, 1f);
    }
}
