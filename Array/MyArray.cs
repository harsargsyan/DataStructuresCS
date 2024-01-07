namespace DataStructuresCS.Array
{
    class MyArray<T>
    {
        private T[] array;

        public MyArray(int size)
        {
            array = new T[size];
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= array.Length)
                {
                    throw new IndexOutOfRangeException();
                }
                return array[index];
            }
            set
            {
                if (index < 0 || index >= array.Length)
                {
                    throw new IndexOutOfRangeException();
                }
                array[index] = value;
            }
        }

        public int Length
        {
            get { return array.Length; }
        }
    }
}
