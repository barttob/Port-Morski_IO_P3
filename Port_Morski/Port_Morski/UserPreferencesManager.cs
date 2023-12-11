using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace Port_Morski
{
    public static class UserPreferencesManager
    {
        private static readonly string PreferencesFolderPath = AppDomain.CurrentDomain.BaseDirectory; // Ścieżka na folder projektu
        private static readonly string PreferencesFileName = "UserPreferences.xml";
        private static readonly string PreferencesFilePath = Path.Combine(PreferencesFolderPath, PreferencesFileName);


        public static void SavePreferences(UserPreferences preferences)
        {
            var serializer = new XmlSerializer(typeof(List<UserPreferences>));

            // Odczytaj aktualne preferencje dla wszystkich użytkowników
            var allPreferences = LoadAllPreferences();

            // Zapisz lub aktualizuj preferencje dla danego użytkownika
            var existingPreferences = allPreferences.FirstOrDefault(p => p.UserId == preferences.UserId);
            if (existingPreferences != null)
            {
                existingPreferences.SelectedTheme = preferences.SelectedTheme;
                existingPreferences.SelectedSize = preferences.SelectedSize; // Update size preference
                
            }
            else
            {
                allPreferences.Add(preferences);
            }

            // Zapisz aktualizowaną listę preferencji
            using (var streamWriter = new StreamWriter(PreferencesFilePath))
            {
                serializer.Serialize(streamWriter, allPreferences);
            }
        }


        public static UserPreferences LoadPreferences(int userId)
        {
            var allPreferences = LoadAllPreferences();
            return allPreferences.FirstOrDefault(p => p.UserId == userId) ?? new UserPreferences { UserId = userId };
        }
        private static List<UserPreferences> LoadAllPreferences()
        {
            if (File.Exists(PreferencesFilePath))
            {
                var serializer = new XmlSerializer(typeof(List<UserPreferences>));
                try
                {
                    using (var streamReader = new StreamReader(PreferencesFilePath))
                    {
                        return (List<UserPreferences>)serializer.Deserialize(streamReader);
                    }
                }
                catch (InvalidOperationException ex)
                {
                    // Obsługa wyjątku w przypadku pustego pliku XML
                    Console.WriteLine($"Błąd deserializacji: {ex.Message}");
                }
            }
            return new List<UserPreferences>();
        }
    }
}
