namespace DCTTestAssignment.Data
{
    public interface ILocalizationDataProvider<TLocalizationData>
    {
        TLocalizationData GetLocalizationData(string language);
    }
}