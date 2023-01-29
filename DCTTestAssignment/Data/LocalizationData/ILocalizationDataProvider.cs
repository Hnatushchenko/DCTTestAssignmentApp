namespace DCTTestAssignment.Data.LocalizationData
{
    public interface ILocalizationDataProvider<TLocalizationData>
    {
        TLocalizationData GetLocalizationData(string language);
    }
}