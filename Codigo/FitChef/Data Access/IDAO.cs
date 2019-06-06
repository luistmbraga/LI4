using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace FitChef.Data_Access
{
    public interface IDAO<T>
    {
       
        //Insert new obj
        //bool Insert(T obj);

        //FindById
        T FindById(int key);

        //ListAll
        Collection<T> FindAll();

    }
}