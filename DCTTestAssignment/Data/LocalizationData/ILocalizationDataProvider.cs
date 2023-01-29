namespace DCTTestAssignment.Data.LocalizationData
{
    public interface ILocalizationDataProvider<TLocalizationData>
    {
        public string CurrentLocalizationName { get; set; }
        TLocalizationData GetLocalizationData(string localization);
        TLocalizationData GetLocalizationData();
    }
}