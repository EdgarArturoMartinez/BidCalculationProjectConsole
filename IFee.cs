namespace BidCalculationProject
{
    interface IFee
    {
        decimal CalculateFee(int vehicleType, decimal basePrice);        
    }
}
