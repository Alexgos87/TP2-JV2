public static class CharacterVitalsExtensions
{
    public static void RaiseAll(this CharacterVitals vitals)
    {
        vitals.RaiseHunger();
        vitals.RaiseLoneliness();
        vitals.RaiseSleepiness();
    }
}