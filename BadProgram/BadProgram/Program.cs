using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyConsole;

namespace BadProgram
{


	internal class Page2 : Page
	{
		public Page2(BadProgram badProgram) : base("Zrzut do pliku", badProgram)
		{

		}

		public override void Display()
		{
			var res = Input.ReadString("Podaj nazwę pliku: ");
			if (!string.IsNullOrWhiteSpace(res))
			{
				Baza baza = new Baza();
				FileStream fs = new FileStream("dupa.txt",FileMode.Create);
				StreamWriter wr = new StreamWriter(fs);
				foreach (Weather item in baza.Weather.Include(x => x.Main).Include(x => x.Wind))
				{
					wr.WriteLineAsync(item.Id + ";" + item.Base + ";" + item.Visibility + ";" + item.Clouds.All + ";" + item.Dt +
														";" + item.Name + ";" + item.MeasurmentDate + ";" + item.Main.TempMin + ";" +
														item.Main.Temp + ";" + item.Main.TempMax + ";" + item.Main.Humidity + ";" +
														item.Main.Pressure + ";" + item.Wind.Speed + ";" + item.Wind.Deg);
				}

									Output.WriteLine(ConsoleColor.Blue, "Zapisano.");
									Input.ReadString("Press [Enter] to navigate home");
									Program.NavigateHome();
			}
		}
	}



	internal class Page1B : MenuPage
	{
		public Page1B(BadProgram badProgram) : base("Historia pogody", badProgram, new Option("Wprowadź nazwę miasta", () => badProgram.NavigateTo<InputPage>()))
		{
			
		}

		public override void Display()
		{
			Library.ShowAct = false;
			base.Display();

			Input.ReadString("Press [Enter] to navigate home");
			Program.NavigateHome();
		}
	}




	internal class Page1A : MenuPage
	{
		public Page1A(BadProgram badProgram) : base("Aktualna pogoda", badProgram, new Option("Wprowadź nazwę miasta", () => badProgram.NavigateTo<InputPage>()))
		{}

		public override void Display()
		{
			Library.ShowAct = true;
			base.Display();


			Input.ReadString("Press [Enter] to navigate home");
			Program.NavigateHome();
		}
	}

	class BadProgram : Program
	{
		public BadProgram()
			: base("Weather program ", breadcrumbHeader: true)
		{
			AddPage(new MainPage(this));
			AddPage(new Page1(this));
			AddPage(new Page1A(this));
			AddPage(new Page1B(this));
			AddPage(new Page2(this));
			AddPage(new InputPage(this));
			AddPage(new ShowWeatherPage(this));

			SetPage<MainPage>();
		}
	}

	internal class Page1 : MenuPage
	{
		public Page1(BadProgram badProgram) : base("Wybór miasta", badProgram, new Option("Aktualna pogoda", () => badProgram.NavigateTo<Page1A>()),
                                                               new Option("Historia pogody", () => badProgram.NavigateTo<Page1B>()))
		{
		}
	}

	internal class MainPage : MenuPage
	{
		public MainPage(BadProgram badProgram) : base("Strona główna", badProgram, 
                                           new Option("Wybór miasta", () => badProgram.NavigateTo<Page1>()),
                                           new Option("Zrzut do pliku", () => badProgram.NavigateTo<Page2>()))
		{
		}
	}





	class InputPage : Page
	{
		public InputPage(Program program)
			: base("Input", program)
		{
		}

		public override void Display()
		{
			base.Display();

			string m = Input.ReadString("Podaj nazwę miasta: ");
			Output.WriteLine(ConsoleColor.Green, "You selected {0}", m);

			Input.ReadString("Wciśnij [Enter] aby wyświetlić pogodę");
			Library.AktuMias = m;
			Program.NavigateTo<ShowWeatherPage>();
		}
	}

internal class ShowWeatherPage : Page
{
public ShowWeatherPage(Program program) : base("Wynik", program)
{

}

public override void Display()
{
if (Library.ShowAct)
{
WeatherManager wm = new WeatherManager();
wm.GetW();
wm.Show();
var res = Input.ReadString("Czy chcesz zapisać aktualną pogodę w bazie danych? [T|n] ");
if (res.ToLower() != "n")
{
wm.Zapisz();
}
Input.ReadString("Press [Enter] to navigate home");
Program.NavigateHome();
}
else
{
WeatherManager wm = new WeatherManager();
wm.ShowAH();
Input.ReadString("\nPress [Enter] to navigate home");
Program.NavigateHome();
}
}

}

	enum Fruit
	{
		Apple,
		Banana,
		Coconut
	}

	internal class ConsoleManager
	{
		internal static void Start()
		{
			var menu = new EasyConsole.Menu()
								.Add("foo", () => Console.WriteLine("foo selected"))
								.Add("bar", () => Console.WriteLine("bar selected"));
			menu.Display();
		}
	}

	class Runner
	{
		static void Main(string[] args)
		{
			new BadProgram().Run();
		}
	}

	public static class Library{public static string AktuMias;public static bool ShowAct { get; internal set; }}
}
