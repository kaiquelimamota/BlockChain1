using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain
{
    public class helpers
    {
        public helpers()
        {

        }
        public string createHash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public bool hashValidado(string hashPow, int difficulty, string prefixo)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < difficulty; i++)
            {
                builder.Append(prefixo);
            }

            return hashPow.Substring(0, difficulty) == builder.ToString();
        }
    }
}
