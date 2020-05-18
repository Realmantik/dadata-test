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
        static string token = "your_token";

        //Секретный ключ для стандартизации
        static string secret = "your_secret";

        static List<Region> regions;

        static void Main(string[] args)
        {

            using (TextReader reader = new StreamReader(@"Y:\FIAS_XML\regions.csv"))//"Y:\FIAS_XML\regions.csv"
            using (CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.HasHeaderRecord = true;
                regions = csv.GetRecords<Region>().ToList();
            }

            var client = new ApiClient(token, secret);

            AddressResponse response = client.SuggestionsQueryAddress(new AddressRequest() 
            {
                Query = "vcr cjrjkmybxtcrfz" //мск сокольническая
            }).GetAwaiter().GetResult();

            Console.WriteLine("Request: <Query = \"vcr cjrjkmybxtcrfz>");
            PrintSuggestions(response);

            string id = "95dbf7fb-0dd4-4a04-8100-4f6c847564b5";
            // HouseGUID == e97154b2-18d0-49ce-8ec1-8a2b9284806c == Пермский край, г Александровск, ул Братьев Давыдовых, д 46
            // AOGUID = 34b39dc7-7340-4c91-9c46-0f9fbd3bd68b == Пермский край, г Александровск, ул Братьев Давыдовых

            var house = client.AdditionalQueryFindAddressById(id).GetAwaiter().GetResult();

            Console.WriteLine($"Request: < FindAddressById = {id} >");
            Console.WriteLine("Response:   ");
            Console.WriteLine("Suggestions.Count: " + house.Suggestions.Count + "\n");

            var myPropertyInfo = house.Suggestions[0].Data.GetType().GetProperties();

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
            PrintSuggestions(piter);

            Console.ReadLine();
        }

        static void PrintSuggestions(AddressResponse ar)
        {
            Console.WriteLine("Response:   ");
            Console.WriteLine("Suggestions.Count: " + ar.Suggestions.Count + "\n");

            foreach (var addrResult in ar.Suggestions)
            {
                Console.WriteLine(addrResult.Value);
                Console.WriteLine(addrResult.UnrestrictedValue);
            }
        }
    }
}