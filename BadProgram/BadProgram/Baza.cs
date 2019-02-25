namespace BadProgram
{
	using System;
	using System.Data.Entity;
	using System.Linq;

	public class Baza : DbContext
	{
		public Baza()
				: base(@"data source=LLEWANDOWSKI-NB\SQLSERVER2017;initial catalog=Weather;integrated security=True;MultipleActiveResultSets=True;App=BadProgram")
		{
		}

		public virtual DbSet<Weather> Weather { get; set; }
	}
}