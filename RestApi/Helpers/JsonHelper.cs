using Newtonsoft.Json;
using RestApi.RestApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApi.Helpers
{
    class JsonHelper
    {
        public static string CreateJsonNewCountry()
        {
            RestApisCountries countries = new RestApisCountries
            {
                capital = "LoonieTown",
                name = "Gitstan",
                region = "upper region of som other important region",
                subregion = "seventh subregion"
            };

            //Serialize the countries object into a string representation of the json
            return JsonConvert.SerializeObject(countries); 
        }

        //This method isn't used. 
        public static string CreateJsonNewCountryWithParams(string capitalIn, string nameIn, string regionIn, string subregionIn)
        {
            RestApisCountries countries = new RestApisCountries
            {
                capital = capitalIn,
                name = nameIn,
                region = regionIn,
                subregion = subregionIn
            };

            //Serialize the countries object into a string representation of the json
            return JsonConvert.SerializeObject(countries);
        }

        public static string CreateJsonNewBook()
        {
            Author author = new Author
            {
                Name = "Bogdan"
            };

            Author author2 = new Author
            {
                Name = "Sylwester"
            };

            RestApisBooks books = new RestApisBooks
            {
                ISBN = "9789000022222",
                LanguageCode = "nl",
                Authors = new List<Author>
                {
                    author,
                    author2,
                },
                Title = "Talking bollocks",
                SubTitle = "The language of the outrovert",
                Description = "For those not interested in listening to others",
                CoverThumb = "",
                Subjects = new List<string>
                {
                    "Some kind of subject",
                    "Another subject"
                },
            };
            
            //Serialize the books object into a string representation of the json
            return JsonConvert.SerializeObject(books); 
        }
    }
}
