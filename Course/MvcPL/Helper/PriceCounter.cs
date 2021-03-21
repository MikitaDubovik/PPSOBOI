using MvcPL.Models;

namespace MvcPL.Helper
{
    public class PriceCounter
    {
        private const int MaxCostCountries = 1000;
        private const int OneCostCountries = 1000;

        private const int MaxCostSex = 300;
        private const int OneCostSex = 150;

        private const int MaxCostLanguage = 600;
        private const int OneCostLanguage = 120;

        private const int MaxCostAge = 1200;
        private const int OneCostAge = 40;

        public static string Calculate(UploadAdViewModel post)
        {
            var countriesNumber = post.Countries?.Split(',').Length;
            var sexNumber = post.Sex?.Split(',').Length;
            var languagesNumber = post.Language?.Split(',').Length;
            var ageNumber = post.Age?.Split(',').Length;

            var totalPrice = 0;

            if (!countriesNumber.HasValue)
            {
                totalPrice += MaxCostCountries;
            }
            else
            {
                totalPrice += countriesNumber.Value * OneCostCountries;
            }

            if (!sexNumber.HasValue)
            {
                totalPrice += MaxCostSex;
            }
            else
            {
                totalPrice += sexNumber.Value * OneCostSex;
            }

            if (!languagesNumber.HasValue)
            {
                totalPrice += MaxCostLanguage;
            }
            else
            {
                totalPrice += languagesNumber.Value * OneCostLanguage;
            }

            if (!ageNumber.HasValue)
            {
                totalPrice += MaxCostAge;
            }
            else
            {
                totalPrice += ageNumber.Value * OneCostAge;
            }

            return totalPrice.ToString();
        }
    }
}