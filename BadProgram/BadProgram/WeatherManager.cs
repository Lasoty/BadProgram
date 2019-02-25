using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace BadProgram
{
	using System.Text;
	using System.Threading;
	using System.Threading.Tasks;
	using ConsoleTables;
	using RestSharp;

	class WeatherManager
	{
		Weather w;

		public async void GetW()
		{
			var client = new RestClient("http://api.openweathermap.org/data/2.5/");

			var request = new RestRequest("weather", Method.GET, DataFormat.Json);
			request.AddParameter("q", Library.AktuMias);
			request.AddParameter("APPID", "be9040cf6bef0f68feb9d29802460c87", ParameterType.GetOrPost);
			var fullUrl = client.BuildUri(request);
			client.ExecuteAsync(request, response => {


				w = Weather.FromJson(response.Content);
				w.MeasurmentDate = DateTime.Now;


			});
		}

		//internal void CheckDb()
		//{
		//	Baza baza = new Baza();
		//	var res = baza.Database.ExecuteSqlCommand("Select 1");
		//}

		public void Show()
		{
			while (true)
			{
				if (w == null)
				{
					Thread.Sleep(1000);
					continue;
				}
				break;
			}

			var s = "" +
                "Aktualna pogoda (na godzinę: " + DateTime.Now.ToShortTimeString() + "):" + Environment.NewLine +
                "\t" + "Zachmurzenie " + w.Clouds.All + "\n" + "\t" + "Wilgotność (%) " + w.Main.Humidity + "\r" +
                "\t" + "Ciśnienie (hPa) " + w.Main.Pressure + Environment.NewLine +
                "\t" + "Temperatura: " + (w.Main.Temp - 273.15) + "\r\n" +
                "\t" + "Temperatura maksymalna: " + (w.Main.TempMax - 273.15) + Environment.NewLine +
                "\t" + "Temperatura minimalna: " + (w.Main.TempMin - 273.15) + "\n" +
                "\t" + "Siła wiatru (m/s): " + w.Wind.Speed + "\n" + "\t" + "Widoczność (m): " + w.Visibility + "\n" +
                "\t" + "Opis: " + w.Base + "\n";


			Console.WriteLine(s);
		}

		internal void ShowAH()
		{
       Baza baza = new Baza(); object lista;
       
       try
       {
       	var query = baza.Database.SqlQuery<Weather>("SELECT Weathers.Id, Weathers.MeasurmentDate, Weathers.Visibility, Weathers.Clouds_All, Mains.Temp, Mains.Humidity, Mains.Pressure, Weathers.Base\r\nFROM Mains INNER JOIN Weathers ON Mains.Id = Weathers.Main_Id INNER JOIN Winds ON Weathers.Wind_Id = Winds.Id WHERE (Weathers.Name LIKE N'" + Library.AktuMias + "') ");
       	lista = query.ToListAsync().Result;
       }
       catch
       {
       	lista = baza.Weather.Include(x => x.Main).Include(x => x.WeatherElements).ToList();
       }
       var table = new ConsoleTable("L.p.", "Data", "Zachmurzenie", "Temperatura", "Wilgotność", "Ciśnienie", "Opis");
       for (int i = 0; i < ((List<Weather>)lista).Count; i++)
       {
       	table.AddRow(i, ((List<Weather>)lista)[i].MeasurmentDate, ((List<Weather>)lista)[i].Clouds.All, ((List<Weather>)lista)[i].Main.Temp, ((List<Weather>)lista)[i].Main.Humidity, ((List<Weather>)lista)[i].Main.Pressure, ((List<Weather>)lista)[i].WeatherElements.FirstOrDefault()?.Description);
       }
			table.Write();
		}

		public void Zapisz()
		{
			while (true)
			{
				if (w == null)
				{
					Thread.Sleep(1000);
					continue;
				}
				break;
			}
			Baza baza = new Baza();
			w.Main.Temp = (w.Main.Temp - 273.15);
			w.Main.TempMax = (w.Main.TempMax - 273.15);
			w.Main.TempMin = (w.Main.TempMin - 273.15);
			baza.Weather.Add(w);baza.SaveChanges();
			Console.WriteLine("Zapisano.");
		}


	}}
