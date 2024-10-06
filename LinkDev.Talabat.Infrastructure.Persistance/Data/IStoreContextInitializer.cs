using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data
{
	public interface IStoreContextInitializer
	{
		Task InitializeAsync();
		Task SeedAsync();

	}
}
