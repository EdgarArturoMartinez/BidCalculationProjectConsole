using BidCalculationProject.FeesCalculation;
using BidCalculationProject.VehicleProcess;
using System.Globalization;


namespace BidCalculationProject
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Welcome to Bid Calculation Project");
			Console.WriteLine("===========================================");

			try
			{
				Vehicle vehicleBID = new Vehicle();
				Console.WriteLine("\r\nPlease enter the Vehicle Base Price");
				vehicleBID.BasePrice = decimal.Parse(Console.ReadLine());

				Console.WriteLine("\r\nPlease Choose One Type of Vehicle:");
				foreach (EnumVehicleType vehicle in Enum.GetValues(typeof(EnumVehicleType)))
				{
					Console.WriteLine($"{(int)vehicle}. {vehicle}");
				}

				if (int.TryParse(Console.ReadLine(), out int userInput) && Enum.IsDefined(typeof(EnumVehicleType), userInput))
				{
					Console.WriteLine($"\r\nYou have entered {vehicleBID.BasePrice.ToString("C", CultureInfo.CurrentCulture)} as Base Price");
					string? vehicleType = Enum.GetName(typeof(EnumVehicleType), userInput);
					Console.WriteLine($"A {vehicleType} Car has been selected to Calculate the following Fee costs:\r\n");
					var fees = new IFee[]
					{
						new BasicUserFee(),
						new SellerSpecialFee(),
						new AssociationCostFee(),
						new StorageFee()
					};

					decimal totalCost = vehicleBID.BasePrice;
					foreach (var fee in fees)
					{
						decimal feeAmount = fee.CalculateFee(userInput, vehicleBID.BasePrice);
						Console.WriteLine($"{fee.GetType().Name}:  {fee.CalculateFee(userInput, vehicleBID.BasePrice).ToString("C", CultureInfo.CurrentCulture)}");
						totalCost += feeAmount;
					}
					Console.WriteLine($"Total Cost: {totalCost.ToString("C", CultureInfo.CurrentCulture)}");
				}
				else
				{
					Console.WriteLine("Invalid choice. Please enter a valid number corresponding to the options.");
				}
			}
			catch (FormatException)
			{
				Console.WriteLine("Error: Invalid input format. Please enter a valid numeric value for the base price.");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An unexpected error occurred: {ex.Message}");
			}
		}
	}
}
