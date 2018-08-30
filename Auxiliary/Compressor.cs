using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Auxiliary
{
    public interface ICompress<T>
    {
        T FromString(string compressed);
    }

    public static class Compressor
    {
        public static string Compress<T>(IList<IList<T>> data)
        {
            var sb = new StringBuilder();

            foreach (var list in data)
            {
                sb.Append(Compress(list));

                if (list != data.Last())
                    sb.Append('\n');
            }

            return sb.ToString();
        }

        public static string Compress<T>(IList<T> data)
        {
            var sb = new StringBuilder();

            foreach (var x in data)
            {
                sb.Append(x.ToString());

                if (!x.Equals(data.Last()))
                    sb.Append('|');
            }

            return sb.ToString();
        }

        public static IList<IList<T>> DecompressJaggedList<T>(string data) where T : class, ICompress<T>, new()
        {
            if (string.IsNullOrEmpty(data))
                return new List<IList<T>>();

            return data.Split('\n').Select(x => DecompressList<T>(x)).ToList();
        }
        
        public static IList<T> DecompressList<T>(string data) where T : class, ICompress<T>, new()
        {
            if (string.IsNullOrEmpty(data))
                return new List<T>();

            return data.Split('|').Select(x => new T().FromString(x)).ToList();
        }
    }
}
