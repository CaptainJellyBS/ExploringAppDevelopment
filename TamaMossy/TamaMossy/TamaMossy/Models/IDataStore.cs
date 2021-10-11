using System;
using System.Collections.Generic;
using System.Text;

namespace TamaMossy.Models
{
    interface IDataStore<T>
    {
        bool CreateItem(T item);

        T ReadItem();

        bool UpdateItem(T item);

        bool DeleteItem(T item);
    }
}
