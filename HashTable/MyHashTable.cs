using static System.Array;

namespace DataStructuresCS.HashTable
{
    class MyHashTable<TKey, TValue>
    {
        // constant sets the initial size of the array
        private const int InitialCapacity = 10;
        
        // constant to determine when to resize the hash table
        private const double LoadFactor = 0.75;

        private Entry<TKey, TValue>[] table;
        private int size;

        private class Entry<TKey, TValue>
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public Entry<TKey, TValue> Next { get; set; }
        }

        public MyHashTable()
        {
            table = new Entry<TKey, TValue>[InitialCapacity];
            size = 0;
        }

        // converts keys into array indices, distributing the data evenly across the table
        private int GetIndex(TKey key)
        {
            int hashCode = key.GetHashCode();
            int index = Math.Abs(hashCode) % table.Length;
            return index;
        }

        /* handle collisions by using a chaining approach, where each index in the table
         contains a linked list of entries with the same hash code */
        private void AddOrUpdate(TKey key, TValue value)
        {
            // get the index using the hash function
            int index = GetIndex(key);
            var entry = table[index];

            while (entry != null)
            {
                if (entry.Key.Equals(key))
                {
                    entry.Value = value;
                    return;
                }
                entry = entry.Next;
            }

            Entry<TKey, TValue> newEntry = new Entry<TKey, TValue>
            {
                Key = key,
                Value = value,
                Next = table[index]
            };

            table[index] = newEntry;
            size++;

            if (size >= table.Length * LoadFactor)
                ResizeTable();
        }

        private void ResizeTable()
        {
            int newCapacity = table.Length * 2;
            Entry<TKey, TValue>[] newTable = new Entry<TKey, TValue>[newCapacity];
            // Copy existing entries to the new table without rehashing
            Copy(table, newTable, table.Length);
            table = newTable;
        }

        // retrieve the value associated with a given key from the hash table
        private bool TryGetValue(TKey key, out TValue value)
        {
            int index = GetIndex(key);
            var entry = table[index];

            while (entry != null)
            {
                if (entry.Key.Equals(key))
                {
                    value = entry.Value;
                    return true;
                }
                entry = entry.Next;
            }

            value = default(TValue);
            return false;
        }

        // finds the entry with the given key and removes it from the hash table
        private bool Remove(TKey key)
        {
            int index = GetIndex(key);
            var entry = table[index];
            Entry<TKey, TValue> prevEntry = null;

            while (entry != null)
            {
                if (entry.Key.Equals(key))
                {
                    if (prevEntry != null)
                        prevEntry.Next = entry.Next;
                    else
                        table[index] = entry.Next;

                    size--;
                    return true;
                }

                prevEntry = entry;
                entry = entry.Next;
            }

            return false;
        }
    }
}
