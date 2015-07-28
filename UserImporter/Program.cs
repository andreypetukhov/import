using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gigya.Socialize.SDK;

namespace UserImporter
{
    class Program
    {
        static void Main(string[] args)
        {

        }

        private static void insertIdentities(string userKey, string userSecretKey, string apiKey, int quantity, GSArray identitiesCollection)
        { 

        
        }

        private static GSArray modifyIdentities(GSArray identitiesCollection)
        {
            GSArray array = new GSArray();
            foreach (GSArray identitiesArray in identitiesCollection)
            {
                GSArray tempArray = new GSArray();
                foreach (GSObject identity in identitiesArray)
                {
                    GSObject tempObject = GenerateIdentity(identity);
                    tempArray.Add(tempObject);
                }
                array.Add(tempArray);
            }

            return array;
        }

        private static GSObject GenerateIdentity(GSObject identity)
        {

            identity.Put("providerUID", GenerateString(20));
            identity.Put("nickname", GenerateString(20));
            identity.Put("firstName", GenerateString(8));
            identity.Put("lastName", GenerateString(10));
            identity.Put("email", "user_"+GenerateString(16)+ "@" + GenerateString(6) + ".com");
            identity.Put("address", GenerateString(30));
            GSObject phone = new GSObject();
            GSArray phones = new GSArray();
            phone.Put("type", "mobile");
            phone.Put("number", GenerateString(9));
            phones.Add(phone);
            phone = new GSObject();
            phone.Put("type", "home");
            phone.Put("number", GenerateString(9));
            phones.Add(phone);
            phone = new GSObject();
            phone.Put("type", "work");
            phone.Put("number", GenerateString(9));
            phones.Add(phone);

            identity.Put("phones", phones);

            return identity;
        }

        private static GSArray ReadFromFileRetrieveIdentities(string filename)
        {
            string line;
            GSArray array = new GSArray();
            StreamReader file = new StreamReader(filename);
            while ((line = file.ReadLine()) != null)
            {
                try
                {
                    GSObject temp = new GSObject(line);
                    GSArray tempArray = temp.GetArray("identities", new GSArray());
                    if (tempArray != null)
                        array.Add(tempArray);

                }
                catch { throw new GSException("incorrect input string"); }


            }
            file.Close();
            return array;
        }

        private static string GenerateString(int num)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var result = new string(
                                    Enumerable.Repeat(chars, num)
                                    .Select(s => s[random.Next(s.Length)])
                                    .ToArray());
            return result;

        }
    }
}
