namespace SodaMachineLib
{
	public class Soda
	{
		protected Soda(string name, int price)
		{
			Name = name;
			Price = price;
		}

		public string Name { get; }
		public int Price { get; }
	}

	public class Cola : Soda
	{
		public Cola()
			: base(ProductName, ProductPrice) { }

		public static string ProductName = "Cola";
		public static int ProductPrice = 20;
	}
}