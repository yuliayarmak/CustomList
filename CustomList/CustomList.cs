using System;
using System.Collections;
using System.Collections.Generic;

namespace CustomList
{
    public class List<T> : IList<T> 
    {
        private T[] data;
        private int capacity = 8;
        private int count;

        public event EventHandler<ListEventArgs> Notify;

        public List()
        {
            data = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                CheckIndex(index); 
                return data[index];
            }
            set
            {
                CheckIndex(index); 
                data[index] = value;
            }
        }

        public int Count => count; 

        public bool IsReadOnly => false;

        public bool IsEmpty => count == 0;


        public void Add(T item)
        {
            if (count == capacity)
            {
                IncreaseCapacity();
            }
            data[count] = item;
            count++;

            Notify?.Invoke(this, new ListEventArgs("Element " + item + " added to the list."));
        }


        public void Clear()
        {
            capacity = 8;
            data = new T[capacity];
            count = 0;

            Notify?.Invoke(this, new ListEventArgs("List is empty"));
        }


        public bool Contains(T item)
        {
            for (int i = 0; i < count; i++)
            {
                T currentValue = data[i];
                if (currentValue.Equals(item))
                {
                    return true;
                }
            }
            return false;
        }


        public void CopyTo(T[] array, int arrayIndex)
        {
            if (arrayIndex < 0 || arrayIndex > array.Length - 1 || array.Length - arrayIndex < Count) 
            {
                throw new ArgumentOutOfRangeException(arrayIndex.ToString());
            }
            for (int i = 0; i < Count; i++)
            {
                array.SetValue(data[i], arrayIndex++);
            }
        }


        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < count; i++)
            {
                yield return data[i];
            }
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                T currentValue = data[i];
                if (currentValue.Equals(item))
                {
                    return i;
                }
            }
            return -1;
        }


        public void Insert(int index, T item)
        {
            CheckIndex(index);
            if (count == capacity)
            {
                IncreaseCapacity();
            }

            for (int i = count; i > index; i--)
            {
                data[i] = data[i - 1];
            }

            data[index] = item;
            count++;

            Notify?.Invoke(this, new ListEventArgs($"Element {item} has been inserted in list on index {index}"));
        }
        

        public void RemoveAt(int index)
        {
            CheckIndex(index);
            for (int i = index; i < count - 1; i++)
            {
                data[i] = data[i + 1];
            }

            count--;

            Notify?.Invoke(this, new ListEventArgs($"Element has been removed from list."));
        }

        public bool Remove(T item)
        {
            RemoveAt(IndexOf(item));
            return true;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.GetEnumerator();
        }

        private void CheckIndex(int index)
        {
            if (index < 0 || index > count - 1)
            {
                throw new ArgumentOutOfRangeException(index.ToString());
            }
        }
        private void IncreaseCapacity()
        {
            T[] resized = new T[capacity * 2];
            for (int i = 0; i < capacity; i++)
            {
                resized[i] = data[i];
            }
            data = resized;
            capacity *= 2;
        }

    }
}
