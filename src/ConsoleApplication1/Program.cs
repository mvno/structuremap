using System.Threading.Tasks;
using StructureMap;

namespace ConsoleApplication1
{
	class Program
	{
		static void Main(string[] args)
		{
			ObjectFactory.Initialize(x => x.Scan(scanner =>
			{
				scanner.AssemblyContainingType<ITest>();
				scanner.WithDefaultConventions();
			}));

			var container = ObjectFactory.Container;

			Parallel.For(0, 1000000, x =>
			{
				using (var nestedContainer = container.GetNestedContainer())
				{
					var instance = nestedContainer.GetInstance<ITest>();
					instance.Index = x;
				}
			});
		}
	}

	public interface ITest
	{
		int Index
		{
			get;
			set;
		}
	}

	public class Test : ITest
	{
		public int Index
		{
			get;
			set;
		}
	}
}
