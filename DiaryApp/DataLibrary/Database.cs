using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;

namespace DataLibrary
{
    public class Database
    {
        // Fixed paths for data files
        public static string DataDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..", "..", "..");
        public static string UsersCsvPath = Path.Combine(DataDirectory, "users.csv");
        public static string DiariesCsvPath = Path.Combine(DataDirectory, "diaries.csv");

        static Database()
        {
            try
            {
                DataDirectory = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..", "..", ".."));
                UsersCsvPath = Path.GetFullPath(Path.Combine(DataDirectory, "users.csv"));
                DiariesCsvPath = Path.GetFullPath(Path.Combine(DataDirectory, "diaries.csv"));

                Debug.WriteLine($"Data directory: {DataDirectory}");
                Debug.WriteLine($"Users CSV path: {UsersCsvPath}");
                Debug.WriteLine($"Diaries CSV path: {DiariesCsvPath}");

                // Dosyaların varlığını kontrol et ve gerekirse oluştur
                EnsureFileExists(UsersCsvPath, "Id,Name,Password");
                EnsureFileExists(DiariesCsvPath, "Id,UserId,Blog,Date_time");
                
                Console.WriteLine($"Database initialized with paths: \nUsers: {UsersCsvPath} \nDiaries: {DiariesCsvPath}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Database initialization error: {ex.Message}");
            }
        }

        private static void EnsureFileExists(string path, string header)
        {
            if (!File.Exists(path))
            {
                using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8))
                {
                    writer.WriteLine(header);
                }
                Console.WriteLine($"Created file: {path}");
            }
        }

        // Read all lines from CSV
        public static List<string[]> ReadAllFromCsv(string path)
        {
            List<string[]> records = new List<string[]>();
            
            try
            {
                using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
                {
                    // Skip header
                    reader.ReadLine();
                    
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            string[] values = ParseCsvLine(line);
                            records.Add(values);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading CSV: {ex.Message}");
            }
            
            return records;
        }

        private static string[] ParseCsvLine(string line)
        {
            List<string> result = new List<string>();
            bool inQuotes = false;
            StringBuilder field = new StringBuilder();

            foreach (char c in line)
            {
                if (c == '"')
                {
                    inQuotes = !inQuotes;
                }
                else if (c == ',' && !inQuotes)
                {
                    result.Add(field.ToString());
                    field.Clear();
                }
                else
                {
                    field.Append(c);
                }
            }

            result.Add(field.ToString());
            return result.ToArray();
        }

        // Append a record to CSV
        public static bool AppendToCsv(string path, string[] values)
        {
            try
            {
                // Format values with proper CSV escaping
                for (int i = 0; i < values.Length; i++)
                {
                    // Escape quotes and enclose in quotes if needed
                    if (values[i].Contains(",") || values[i].Contains("\"") || values[i].Contains("\n"))
                    {
                        values[i] = "\"" + values[i].Replace("\"", "\"\"") + "\"";
                    }
                }

                string line = string.Join(",", values);
                
                using (StreamWriter writer = new StreamWriter(path, true, Encoding.UTF8))
                {
                    writer.WriteLine(line);
                }
                
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error appending to CSV: {ex.Message}");
                return false;
            }
        }

        // Update a record in CSV
        public static bool UpdateCsvRecord(string path, int idIndex, string idValue, string[] newValues)
        {
            try
            {
                List<string> lines = new List<string>();
                bool recordFound = false;
                
                // Read all lines
                using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
                {
                    // Preserve header
                    string header = reader.ReadLine();
                    lines.Add(header);
                    
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] values = ParseCsvLine(line);
                        
                        // Check if this is the record to update
                        if (values.Length > idIndex && values[idIndex] == idValue)
                        {
                            // Format new values with proper CSV escaping
                            for (int i = 0; i < newValues.Length; i++)
                            {
                                if (newValues[i].Contains(",") || newValues[i].Contains("\"") || newValues[i].Contains("\n"))
                                {
                                    newValues[i] = "\"" + newValues[i].Replace("\"", "\"\"") + "\"";
                                }
                            }
                            
                            lines.Add(string.Join(",", newValues));
                            recordFound = true;
                        }
                        else
                        {
                            // Keep original line
                            lines.Add(line);
                        }
                    }
                }
                
                // If record was found and updated, write back all lines
                if (recordFound)
                {
                    File.WriteAllLines(path, lines, Encoding.UTF8);
                    return true;
                }
                
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating CSV: {ex.Message}");
                return false;
            }
        }

        // Delete a record from CSV
        public static bool DeleteFromCsv(string path, int idIndex, string idValue)
        {
            try
            {
                List<string> lines = new List<string>();
                bool recordFound = false;
                
                // Read all lines
                using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
                {
                    // Preserve header
                    string header = reader.ReadLine();
                    lines.Add(header);
                    
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] values = ParseCsvLine(line);
                        
                        // Skip the record to delete
                        if (values.Length > idIndex && values[idIndex] == idValue)
                        {
                            recordFound = true;
                        }
                        else
                        {
                            // Keep other lines
                            lines.Add(line);
                        }
                    }
                }
                
                // If record was found and deleted, write back all lines
                if (recordFound)
                {
                    File.WriteAllLines(path, lines, Encoding.UTF8);
                    return true;
                }
                
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting from CSV: {ex.Message}");
                return false;
            }
        }

        // Generate a new ID for a record
        public static int GetNextId(string path)
        {
            try
            {
                int maxId = 0;
                
                // Read all lines to find the highest ID
                using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
                {
                    // Skip header
                    reader.ReadLine();
                    
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            string[] values = ParseCsvLine(line);
                            if (values.Length > 0 && int.TryParse(values[0], out int id))
                            {
                                maxId = Math.Max(maxId, id);
                            }
                        }
                    }
                }
                
                return maxId + 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting next ID: {ex.Message}");
                return 1; // Default to 1 if there's an error
            }
        }

        // Get paths to CSV files
        public static string GetUsersCsvPath()
        {
            return UsersCsvPath;
        }

        public static string GetDiariesCsvPath()
        {
            return DiariesCsvPath;
        }
    }
}
