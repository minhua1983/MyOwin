﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOwin.Core.MyContainer
{
    public interface IContainer
    {
        //将TClass类以TInterface接口形式注册到当前容器实例中
        void Register<TClass, TInterface>() where TClass : class, TInterface;
        //将TClass类注册到当前容器实例中
        void Register<TClass>() where TClass : class;
        //直接将type注册到当前容器实例中
        void Register(Type type);
        //将TClass类以TInterface接口形式注入到容器
        //T Resolve<T>(params object[] parameters);
        //以无参数构造获取T接口类型的实例
        T Resolve<T>();
        //用于动态获取对象实例
        dynamic Resolve(Type type);
    }
}
