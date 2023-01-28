using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCTTestAssignment.Data.LocalizationData.ConvertViewLocalizationData
{
    public class ConvertViewUkrainianLocalizationData : IConvertViewLocalizationData
    {
        public string From =>"Назва криптовалюти, з якої конвертувати";

        public string To => "Криптовалюта, у яку конвертувати";

        public string Amount => "Кількість";

        public string Convert =>"Конвертувати";

        public string CalculatedAmount => "Конвертована сума";

        public string Loading => "Завантаження...";

        public string TimeoutMessage => "Час очікування вичервано. Вибачне.";

        public string CoinNotFoundMessage => "Криптовалюту не знайдено";
    }
}
