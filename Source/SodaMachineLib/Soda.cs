namespace SodaMachineLib
{
	public abstract class Soda
	{
		protected Soda(string name, int price)
		{
			Name = name;
			Price = price;
		}

		public string Name { get; }
		public int Price { get; }
		public abstract Soda Create();
	}

	public class Cola : Soda
	{
		public Cola()
			: base(ProductName, ProductPrice) { }

		public static string ProductName = "Cola";
		public static int ProductPrice = 20;

		public override Soda Create()
		{
			return new Cola();
		}
	}

	public class Fanta : Soda
	{
		public Fanta()
			: base(ProductName, ProductPrice) { }

		public static string ProductName = "Fanta";
		public static int ProductPrice = 15;

		public override Soda Create()
		{
			return new Fanta();
		}
	}

	public class Sprite : Soda
	{
		public Sprite()
			: base(ProductName, ProductPrice) { }

		public static string ProductName = "Sprite";
		public static int ProductPrice = 15;

		public override Soda Create()
		{
			return new Sprite();
		}
	}
}