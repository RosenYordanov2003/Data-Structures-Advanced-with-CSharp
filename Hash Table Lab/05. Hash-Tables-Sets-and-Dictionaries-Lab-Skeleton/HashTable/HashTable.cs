namespace HashTable
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class HashTable<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
    {
        private LinkedList<KeyValue<TKey, TValue>>[] slots;
        private const int DefaultCapacity = 16;
        private decimal fillFactor = 0.75m;
        public int Count { get; private set; }

        public int Capacity => this.slots.Length;

        public HashTable() : this(DefaultCapacity)
        {

        }

        public HashTable(int capacity)
        {
            this.slots = new LinkedList<KeyValue<TKey, TValue>>[capacity];
        }

        public void Add(TKey key, TValue value)
        {
            bool growResult = (decimal)(this.Count + 1) / this.slots.Length >= fillFactor;
            if (growResult)
            {
                ResizeSlots();
            }
            int index = FindIndex(key);
            if (slots[index] == null)
            {
                slots[index] = new LinkedList<KeyValue<TKey, TValue>>();
            }
            else
            {
                foreach (KeyValue<TKey, TValue> kvp in slots[index])
                {
                    if (kvp.Key.Equals(key))
                    {
                        throw new ArgumentException("Key already exists" + key);
                    }
                }

            }
            slots[index].AddLast(new KeyValue<TKey, TValue>(key, value));
            this.Count++;
        }



        public bool AddOrReplace(TKey key, TValue value)
        {
            try
            {
                this.Add(key, value);
                return true;
            }
            catch (ArgumentException)
            {
                int index = FindIndex(key);
                foreach (KeyValue<TKey, TValue> kvp in slots[index])
                {
                    if (kvp.Key.Equals(key))
                    {
                        kvp.Value = value;
                        return true;
                    }
                }
                return false;
            }
        }

        public TValue Get(TKey key)
        {
            return this[key];
        }

        public TValue this[TKey key]
        {
            get
            {
                int index = FindIndex(key);
                if (this.slots[index] == null)
                {
                    throw new KeyNotFoundException();
                }
                KeyValue<TKey, TValue> valueToFind = this.slots[index].FirstOrDefault(kvp => kvp.Key.Equals(key));
                if (valueToFind == null)
                {
                    throw new KeyNotFoundException();
                }
                return valueToFind.Value;
            }
            set
            {
                this.AddOrReplace(key, value);
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            KeyValue<TKey, TValue> element = this.Find(key);

            if (element != null)
            {
                value = element.Value;
                return true;
            }

            value = default;
            return false;
        }

        public KeyValue<TKey, TValue> Find(TKey key)
        {
            int index = this.FindIndex(key);
            if (this.slots[index] == null)
            {
                return null;
            }
            return this.slots[index].FirstOrDefault(kvp => kvp.Key.Equals(key));
        }

        public bool ContainsKey(TKey key)
        {
            return this.Find(key) != null;
        }

        public bool Remove(TKey key)
        {
            int index = FindIndex(key);
            if (this.slots[index] == null)
            {
                return false;
            }
            LinkedList<KeyValue<TKey, TValue>> kvpElements = this.slots[index];
            LinkedListNode<KeyValue<TKey, TValue>> current = kvpElements.First;

            while (current != null)
            {
                if (current.Value.Key.Equals(key))
                {
                    kvpElements.Remove(current);
                    this.Count--;
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public void Clear()
        {
            this.slots = new LinkedList<KeyValue<TKey, TValue>>[DefaultCapacity];
            this.Count = 0;
        }

        public IEnumerable<TKey> Keys => this.Select(kvp => kvp.Key);

        public IEnumerable<TValue> Values => this.Select(kvp => kvp.Value);

        public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
        {
            foreach (LinkedList<KeyValue<TKey, TValue>> linkedList in this.slots)
            {
                if (linkedList != null)
                {
                    foreach (KeyValue<TKey, TValue> kvp in linkedList)
                    {
                        yield return kvp;
                    }
                }
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
        private int FindIndex(TKey key) => Math.Abs(key.GetHashCode()) % this.slots.Length;

        private void ResizeSlots()
        {
            HashTable<TKey, TValue> newHashTable = new HashTable<TKey, TValue>(this.Capacity * 2);

            foreach (LinkedList<KeyValue<TKey, TValue>> linkedList in this.slots)
            {
                if (linkedList != null)
                {
                    foreach (KeyValue<TKey, TValue> kvp in linkedList)
                    {
                        newHashTable.Add(kvp.Key, kvp.Value);
                    }
                }
            }
            this.slots = newHashTable.slots;
        }

    }
}