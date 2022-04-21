using System;
using System.Collections;
using System.Collections.Generic;

public class Vector<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IEnumerator
{
    T[] array;
    int size;
    int count;
    int currentIndex;
    public T this[int index]
    {
        get
        {
            if (count <= index) return default(T);
            return array[index];
        }
        set
        {
            if (count <= index) return;
            array[index] = value;
        }
    }
    public int Count => count;

    public bool IsReadOnly => false;

    public object Current => array[currentIndex];

    public Vector()
    {
        size = 1;
        array = new T[size];
        count = 0;
        currentIndex = -1;
    }

    public Vector(int capacity)
    {
        size = capacity;
        array = new T[size];
        count = 0;
        currentIndex = -1;
    }

    public Vector(Vector<T> rhs)
    {
        size = rhs.size;
        count = rhs.count;
        currentIndex = rhs.currentIndex;
        array = new T[size];
        rhs.CopyTo(array, 0);
    }

    public void Add(T item)
    {
        if (count + 1 >= size)
        {
            size = size * 2;
            T[] newArray = new T[size];
            CopyTo(newArray, 0);
            array = newArray;
        }

        array[count] = item;

        count++;
    }

    public void Clear()
    {
        count = 0;
    }

    public bool Contains(T item)
    {
        if (array == null) return false;
        for (int i = 0; i < count; i++)
        {
            if (array[i].Equals(item) == false) continue;
            return true;
        }
        return false;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        if (array == null)
        {
            throw new System.ArgumentNullException();
        }
        if (arrayIndex < 0)
        {
            throw new System.ArgumentOutOfRangeException();
        }
        if (array.Length < count + arrayIndex)
        {
            throw new System.ArgumentException("대상 배열의 길이가 짧습니다. destIndex, length 및 배열의 하한을 확인하십시오.");
        }
        for (int i = 0, j = arrayIndex; i < count; i++, j++)
        {
            array[j] = this.array[i];
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < count; i++)
        {
            yield return array[i];
        }
    }

    public int IndexOf(T item)
    {
        if (array == null) return -1;
        for (int i = 0; i < count; i++)
        {
            if (array[i].Equals(item) == false) continue;
            return i;
        }
        return -1;
    }

    public void Insert(int index, T item)
    {
        this[index] = item;
    }

    public bool Remove(T item)
    {
        if (array == null) return false;
        int removeIndex = IndexOf(item);
        if (removeIndex == -1) return false;

        if (removeIndex == count - 1)
        {
            count--;
        }
        else if (removeIndex == 0)
        {
            T[] newArray = new T[size];
            BlockCopy(array, 1, newArray, 0, count - 1);
            array = newArray;
        }
        else
        {

            T[] newArray = new T[size];
            BlockCopy(array, 0, newArray, 0, removeIndex);
            BlockCopy(array, removeIndex + 1, newArray, removeIndex, count - removeIndex - 1);
            array = newArray;
        }

        count--;

        return true;
    }

    public void RemoveAt(int index)
    {
        if (array == null) return;
        if (count == 0) return;
        int removeIndex = index;
        if (removeIndex < 0 || removeIndex >= count) return;


        if (removeIndex == count - 1)
        {
            count--;
        }
        else if (removeIndex == 0)
        {
            T[] newArray = new T[size];
            BlockCopy(array, 1, newArray, 0, count - 1);
            array = newArray;
        }
        else
        {

            T[] newArray = new T[size];
            BlockCopy(array, 0, newArray, 0, removeIndex);
            BlockCopy(array, removeIndex + 1, newArray, removeIndex, count - removeIndex - 1);
            array = newArray;
        }

        count--;

        return;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this;
    }

    public bool MoveNext()
    {
        if (array == null)
        {
            Reset();
            return false;
        }
        currentIndex++;
        if (currentIndex >= count)
        {
            Reset();
            return false;
        }
        return true;
    }

    public void Reset()
    {
        currentIndex = -1;
    }

    public static void BlockCopy<T>(T[] src, int srcOffset, T[] dst, int dstOffset, int count)
    {
        for (int i = 0, s = srcOffset, d = dstOffset; i < count; i++, s++, d++)
        {
            dst[d] = src[s];
        }
    }

    public void Sort<Tkey>(Func<T, Tkey> keySelector, bool descending = false) where Tkey : IComparable
    {
        if (descending == false)
        {
            for (int i = count - 1; i > 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    if (keySelector.Invoke(array[j]).CompareTo(keySelector.Invoke(array[j + 1])) > 0)
                    {
                        Swap(j, j + 1);
                    }
                }
            }
        }
        else
        {
            for (int i = count - 1; i > 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    if (keySelector.Invoke(array[j]).CompareTo(keySelector.Invoke(array[j + 1])) < 0)
                    {
                        Swap(j, j + 1);
                    }
                }
            }
        }

    }

    public void Swap(int a, int b)
    {
        T temp = array[a];
        array[a] = array[b];
        array[b] = temp;
    }


}
