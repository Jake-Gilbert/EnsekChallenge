using Ensek.API.Entities;
using Microsoft.Data.Sqlite;

namespace Ensek.API.Helpers
{
    public class DatabaseHelper
    {
        public IList<int> GetAccountIds(string source)
        {
            try
            {
                IList<int> tempAccountIds = new List<int>();
                string query =
                            @"
                            SELECT AccountId FROM Test_Accounts
                            ";
                using (var connection = new SqliteConnection(source))
                {
                    connection.Open();
                    using (var command = new SqliteCommand(query, connection))
                    {

                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            tempAccountIds.Add(Convert.ToInt32(reader["AccountId"]));
                        }
                    }
                }
                return tempAccountIds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int[] UploadValidData(string source, IList<MeterReading> validReadings, IList<int> existingAccountIds)
        {
            try
            {
                // SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());
                string query =
                            @"
                            REPLACE INTO Meter_Readings (AccountId, MeterReadingDateTime, MeterReadValue) 
                            VALUES (@AccountId, @MeterReadingDateTime, @MeterReadValue) 
                            ";
                int[] successfulAndFailed = new int[2];
                int validResults = 0;
                int invalidResults = 0;
                using (var connection = new SqliteConnection(source))
                {
                    connection.Open();
                    using (var command = new SqliteCommand(query, connection))
                    {
                        foreach (MeterReading meterReading in validReadings)
                        {
                            if (int.TryParse(meterReading.MeterReadValue, out int accountId))
                            {
                                try
                                {
                                    if (existingAccountIds.Contains(meterReading.AccountId))
                                    {
                                        command.Parameters.Add(new SqliteParameter("@AccountId", meterReading.AccountId));
                                        command.Parameters.Add(new SqliteParameter("@MeterReadingDateTime", meterReading.MeterReadingDateTime));
                                        command.Parameters.Add(new SqliteParameter("@MeterReadValue", meterReading.MeterReadValue));

                                        command.ExecuteNonQuery();
                                        validResults++;
                                        connection.Close();
                                    }
                                }
                                catch
                                {
                                    invalidResults++;
                                    continue;
                                }
                            }
                        }
                    }
                }
                successfulAndFailed[0] = validResults;
                successfulAndFailed[1] = invalidResults;
                return successfulAndFailed;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
