using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration.Attributes;

namespace dadata_test
{
    /// <summary>
    /// Отображение регионов от dadata.ru: https://github.com/hflabs/region/blob/master/region.csv
    /// </summary>
    class Region
    {
        [Name("name")]
        public string Name { get; set; }

        [Name("type")]
        public string Type { get; set; }

        [Name("name_with_type")]
        public string NameWithType { get; set; }

        [Name("federal_district")]
        public string FederalDistrict { get; set; }

        [Name("kladr_id")]
        public string KladrId { get; set; }

        [Name("fias_id")]
        public string FiasId { get; set; }

        [Name("okato")]
        public string Okato { get; set; }

        [Name("oktmo")]
        public string Oktmo { get; set; }

        /// <summary>
        /// Код ИФНС
        /// </summary>
        [Name("tax_office")]
        public string TaxOffice { get; set; }

        [Name("postal_code")]
        public string PostalCode { get; set; }

        [Name("iso_code")]
        public string ISOCode { get; set; }

        [Name("timezone")]
        public string Timezone { get; set; }

        [Name("geoname_code")]
        public string GeoNameCode { get; set; }

        [Name("geoname_id")]
        public string GeoNameId { get; set; }

        [Name("geoname_name")]
        public string GeoNameName { get; set; }

    }
}
