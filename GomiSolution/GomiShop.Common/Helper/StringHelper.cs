using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GomiShop.Common.Helper
{
    public static class StringHelper
    {
        public static string RandomString(int length, string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")
        {
            const int byteSize = 0x100;
            var allowedCharSet = new HashSet<char>(allowedChars).ToArray();
            if (byteSize < allowedCharSet.Length) throw new ArgumentException(String.Format("allowedChars may contain no more than {0} characters.", byteSize));

            // Guid.NewGuid and System.Random are not particularly random. By using a
            // cryptographically-secure random number generator, the caller is always
            // protected, regardless of use.
            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                var result = new StringBuilder();
                var buf = new byte[128];
                while (result.Length < length)
                {
                    rng.GetBytes(buf);
                    for (var i = 0; i < buf.Length && result.Length < length; ++i)
                    {
                        // Divide the byte into allowedCharSet-sized groups. If the
                        // random value falls into the last group and the last group is
                        // too small to choose from the entire allowedCharSet, ignore
                        // the value in order to avoid biasing the result.
                        var outOfRangeStart = byteSize - (byteSize % allowedCharSet.Length);
                        if (outOfRangeStart <= buf[i]) continue;
                        result.Append(allowedCharSet[buf[i] % allowedCharSet.Length]);
                    }
                }
                return result.ToString();
            }
        }

        public static DataTable StringListToTable(List<string> list)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[1] { new DataColumn("value", typeof(string)) });

            foreach (string value in list)
            {
                if (!String.IsNullOrEmpty(value.Trim()))
                    dt.Rows.Add(value.Trim());
            }

            return dt;
        }

        public static string FormatCurrency(decimal value, string format, string strCountry = "vi-VN")
        {
            var text = String.Format(System.Globalization.CultureInfo.GetCultureInfo(strCountry), format, 0);
            if (value != 0)
            {
                text = String.Format(System.Globalization.CultureInfo.GetCultureInfo(strCountry), format, value);
            }
            return text;
        }

        public static string FormatTransactionCode(long number)
        {

            string code = "000000000" + (number > 0 ? number : 1);
            return DateTime.Today.ToString("yyyyMMdd") + "_" + code.Substring(code.Length - 9);
        }
    }
}
