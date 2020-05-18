using CsvHelper;
using DaData;
using DaData.Models.Additional.Requests;
using DaData.Models.Suggestions.Requests;
using DaData.Models.Suggestions.Responses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace dadata_test
{
    class Program
    {
        //API ключ
        static string token = "d39e30f1b71eb905940d759a159d543449315a47";

        //Секретный ключ для стандартизации
        static string secret = "c74a45d694baf3a1b4d5403e6817facaad3a0767";

        static void Main(string[] args)
        {

            List<Region> records;

            using (TextReader reader = new StreamReader(@"Y:\FIAS_XML\regions.csv"))//"Y:\FIAS_XML\regions.csv"
            using (CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.HasHeaderRecord = true;
                records = csv.GetRecords<Region>().ToList();
            }

            var client = new ApiClient(token, secret);

            AddressResponse response = client.SuggestionsQueryAddress(new AddressRequest() 
            {
                Query = "vcr cjrjkmybxtcrfz" //мск сокольническая
            }).GetAwaiter().GetResult();

            Console.WriteLine("Request: <Query = \"vcr cjrjkmybxtcrfz>");
            Console.WriteLine("Response:   ");
            Console.WriteLine("Suggestions.Count: " + response.Suggestions.Count + "\n");

            foreach (var addrResult in response.Suggestions)
            {
                Console.WriteLine(addrResult.Value);
                Console.WriteLine(addrResult.UnrestrictedValue);
                Console.WriteLine(addrResult.Data.RegionFiasId);
            }

            string id = "95dbf7fb-0dd4-4a04-8100-4f6c847564b5";
            // HouseGUID == e97154b2-18d0-49ce-8ec1-8a2b9284806c == Пермский край, г Александровск, ул Братьев Давыдовых, д 46
            // AOGUID = 34b39dc7-7340-4c91-9c46-0f9fbd3bd68b == Пермский край, г Александровск, ул Братьев Давыдовых

            var house = client.AdditionalQueryFindAddressById(id).GetAwaiter().GetResult();

            Console.WriteLine($"Request: < FindAddressById = {id} >");
            Console.WriteLine("Response:   ");
            Console.WriteLine("Suggestions.Count: " + house.Suggestions.Count + "\n");

            PropertyInfo[] myPropertyInfo;
            myPropertyInfo = house.Suggestions[0].Data.GetType().GetProperties();

            for (int i = 0; i < myPropertyInfo.Length; i++)
            {
                object val = myPropertyInfo[i].GetValue(house.Suggestions[0].Data, null);
                if (val != null)
                {
                    val = val.ToString();
                }
                Console.WriteLine(myPropertyInfo[i].Name + ": " + val);
            }

            foreach (var addrResult in house.Suggestions)
            {
                Console.WriteLine(addrResult.Value);
            }

            var piter = client.SuggestionsQueryAddress("gbnth ytdcrbq").GetAwaiter().GetResult();

            Console.WriteLine($"Request: < QueryAddress = \"gbnth ytdcrbq\" >");
            Console.WriteLine("Response:   ");
            Console.WriteLine("Suggestions.Count: " + piter.Suggestions.Count + "\n");

            foreach (var addrResult in piter.Suggestions)
            {
                Console.WriteLine(addrResult.Value);
            }

            Console.ReadLine();
        }

        public static object GetPropertyValue(object source, string propertyName)
        {
            PropertyInfo property = source.GetType().GetProperty(propertyName);
            return property.GetValue(source, null);
        }
    }
}
