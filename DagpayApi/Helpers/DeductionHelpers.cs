using System;

namespace DagpayApi.Helpers
{
    public class DeductionHelpers
    {
        public static decimal CalculateDeduction(decimal cost, decimal discountFactor)
        {
            return Decimal.Round(((.01M * cost * discountFactor) / 26), 2, MidpointRounding.AwayFromZero);
        }

        public static int CalculateDiscountFactor(string firstName)
        {
            int discountFactor = 100;

            if (firstName.Substring(0, 1) == "A")
            {
                discountFactor = 90;
            }

            return discountFactor;
        }
    }
}