namespace Auxiliary
{
    public static class ArrayAllocatorUtils
    {
        public static T[] Allocate<T>(int dim0)
        {
            return new T[dim0];
        }

        public static T[][] Allocate<T>(int dim0, int dim1)
        {
            var array = new T[dim0][];

            for (int i = 0; i < dim0; i++)
                array[i] = Allocate<T>(dim1);

            return array;
        }

        public static T[][][] Allocate<T>(int dim0, int dim1, int dim2)
        {
            var array = new T[dim0][][];

            for (int i = 0; i < dim0; i++)
                array[i] = Allocate<T>(dim1, dim2);

            return array;
        }
    }
}