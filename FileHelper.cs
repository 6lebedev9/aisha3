using System;
using System.IO;

namespace aisha3
{
    public static class FileHelper
    {
        private static readonly string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "userSettings.enc");

        public static void SaveEncryptedSettings(string data)
        {
            string encryptedData = EncryptionHelper.Encrypt(data);
            File.WriteAllText(FilePath, encryptedData);
        }

        public static string LoadEncryptedSettings()
        {
            if (File.Exists(FilePath))
            {
                string encryptedData = File.ReadAllText(FilePath);
                return EncryptionHelper.Decrypt(encryptedData);
            }

            return null;
        }
    }
}
