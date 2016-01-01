
namespace HomeRunner.Foundation.Infrastructure.Extension
{
	public static class ObjectExtension
	{
		public static bool IsDefault<TInstance>(this TInstance instance)
		{
			return Equals(instance, default(TInstance));
		}
	}
}

