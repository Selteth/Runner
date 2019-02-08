using System.Collections.Generic;

interface ILevelCreator
{
    IList<GeneratedPlatform> GetNextPlatforms(int count);
}