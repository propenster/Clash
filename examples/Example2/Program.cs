// See https://aka.ms/new-console-template for more information
using Clash;


//Let's assume you're trying to receive burger orders from a CLI application...
[Command(About = "This is a burger-ordering CLI application", Author = "Mac Donalds", Version = "0.1.0")]
internal class Burger
{
    [Arg(Short = "t", Long = "type", Required = true, DefaultValue = "Cheeseburger")]
    public string Type { get; set; } = string.Empty;
    [Arg(Short = "q", Long = "quantity", Required = true, DefaultValue = 1)]
    public int Quantity { get; set; }
    [Arg(Short = "d", Long = "details", Required = false)]
    public string OrderDetails { get; set; } = string.Empty;


}

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var parser = new Parser();
        if(parser.TryParse<Burger>(args, out Burger burger))
        {
            Console.WriteLine("Burger Type >>> {0}", burger.Type);
            Console.WriteLine("Quantity >>> {0}", burger.Quantity);
            if (!string.IsNullOrWhiteSpace(burger.OrderDetails)) Console.WriteLine("Order Special Details >>> {0}", burger.OrderDetails);

            //process 
            SendBurgerOrderToKitchen(burger);

        }
    }
    private static void SendBurgerOrderToKitchen(Burger burger)
    {
        Console.WriteLine("Processing order...");
    }


}