using System;
using System.Linq;
using System.Text;

namespace TestApp
{
    public class BinaryFormatter
    {


        /// <summary>
        /// Numeric printable ASCII characters (between 0x30 and 0x39)
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="length"></param>
        /// <returns></returns>
        /// <remarks>
        /// np. 10.00 i rozmiar 12
        /// 2 ostatnie miejsca to grosze
        // np. 1000 -> 30 30 30 30 30 30 30 30 31 30 30 30         
        /// </remarks>
        public byte[] Format(decimal value, byte length = 12)
        {
            return Encoding.ASCII.GetBytes(value.ToString());
        }

        // Hint: Metoda do wizualizacji HEX Convert.ToHexString()

        private static string Format(string hex, string separator = " ")
        {
            var list = Enumerable
                            .Range(0, hex.Length / 2)
                            .Select(i => hex.Substring(i * 2, 2));

            return string.Join(separator, list);
        }
    }
}
