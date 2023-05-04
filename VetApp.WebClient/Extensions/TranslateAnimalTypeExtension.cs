using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VDMJasminka.WebClient.Extensions
{
    public static class TranslateAnimalTypeExtension
    {
        public static string TranslateAnimalType(this string animalType)
        {
            var animalTypesDict = new Dictionary<string, string>
            {
                {"kuce", "Куче"  },
                {"Mačka", "Мачка" },
                {"svinja", "Свиња" },
                {"kunić", "Кунич" },
                {"hrčak", "Хрчак" },
                {"kornjača", "Желка" },
                {"ververicka", "Верверичка" },
                {"papagal", "Папагал" },
                {"degu rodent", "Дегу родент" },
                {"Shugar Glider", "Шугар Глајдер" },
            };
            return animalTypesDict.TryGetValue(animalType, out var value) ? value : animalType;
        }
    }
}