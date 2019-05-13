using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicHelper
{
    class ListTCustomHelper<T> : IEnumerable where T : IComparable
    {
        List<T> list;
        public ListTCustomHelper()//初始化集合
        {
            list = new List<T>();
        }
        public int Capacity//封装属性
        {
            get { return list.Count; }
        }
        public void Add(T value)//添加元素
        {
            list.Add(value);
        }
        public void Remove(T value)//删除指定元素
        {
            list.Remove(value);
        }
        public void RemoveAt(int index)//删除指定索引的元素
        {
            list.RemoveAt(index);
        }
        public void Insert(int index, T value)//插入元素
        {
            list.Insert(index, value);
        }
        public IEnumerator GetEnumerator()//为foreach遍历泛型时提供便利
        {
            return list.GetEnumerator();
        }
        public void SystemSort()//系统排序
        {
            list.Sort();//升序
            list.Reverse();//降序
            foreach (var v in list)
            {
                Console.WriteLine(v);
            }
        }
        public void QuickSort(int l, int r)//快速排序
        {
            if (l >= r)
            {
                return;
            }
            int i = l; int j = r; T key = list[l];//选择第一个数为key
            while (i < j)
            {
                while (i < j && list[j].CompareTo(key) > 0)//从右向左找第一个小于key的值
                {
                    j--;
                    if (i < j)
                    {
                        list[i] = list[j];
                        i++;
                    }
                }
                while (i < j && list[i].CompareTo(key) < 0)//从左向右找第一个大于key的值
                {
                    i++;
                    if (i < j)
                    {
                        list[j] = list[i];
                        j--;
                    }
                }
            }
            list[i] = key;
            QuickSort(l, i - 1);//递归调用
            QuickSort(i + 1, r);//递归调用
            foreach (var v in list)
            {
                Console.WriteLine(v);
            }
        }
        public void InsertSort()//插入排序
        {
            T temp1;
            for (int i = 0; i < list.Count; i++)
            {
                temp1 = list[i];//从待插入组取出第一个元素 
                int j = i - 1; //有序组最后一个元素的下标   
                while (j >= 0 && temp1.CompareTo(list[j]) < 0)//边界限制和插入判断条件   
                {
                    list[j + 1] = list[j];//若不是合适位置，有序组元素向后移动   
                    j--;
                }
                list[j + 1] = temp1;//找到合适的位置，将元素插入。
            }
            T temp2;
            for (int i = 0; i < list.Count - 1; i++)
            {
                for (int j = i + 1; j > 0; j--)
                {
                    if (list[j].CompareTo(list[j - 1]) < 0)
                    {
                        temp2 = list[j - 1];
                        list[j - 1] = list[j];
                        list[j] = temp2;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            foreach (var v in list)
            {
                Console.WriteLine(v);
            }
        }
        public void ChooseSort()//选择排序
        {
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = i + 1; j <= list.Count - 1; j++)
                {
                    if (list[i].CompareTo(list[j]) < 1)
                    {
                        var temp = list[i];
                        list[i] = list[j];
                        list[j] = temp;
                    }
                }
                Console.WriteLine(list[i]);
            }
        }
        public void BubbleSort()//冒泡排序
        {
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list.Count - i - 1; j++)
                {
                    if (list[j].CompareTo(list[j + 1]) < 1)
                    {
                        var temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                    }
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i]);
            }
        }
        public T this[int index]//索引器
        {
            get { return list[index]; }
            set { list[index] = value; }
        }
    }
}
