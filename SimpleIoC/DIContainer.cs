using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleIoC
{
    class DIContainer
    {
        //Dictionary de chua cac interface va module tuong ung
        private static readonly Dictionary<Type, object>
            RegisteredModules = new Dictionary<Type, object>();
        //2 ham co ban, chuyen <T> thanh dang Type trong c# de de viet code
        public static void SetModule<TInterface, TModule>()
        {
            SetModule(typeof(TInterface), typeof(TModule));
        }
        public static T GetModule<T>()
        {
            return (T)GetModule(typeof(T));
        }

        private static void SetModule(Type interfaceType, Type moduleType)
        {
            //Kiem tra module da implement interface chua
            if (!interfaceType.IsAssignableFrom(moduleType))
            {
                throw new Exception("ERR......");
            }
            //Tim constructor dau tien
            var firstConstructor = moduleType.GetConstructors()[0];
            object module = null;
            //neu nhu khong co tham so
            if (!firstConstructor.GetParameters().Any())
            {
                //Khoi tao module
                module = firstConstructor.Invoke(null);//new Database(), new Logger()
            }
            else
            {
                //lay cac tham so cua constructor
                var constructorParameters = firstConstructor.GetParameters(); //IDatabase, ILogger..
                var moduleDependecies = new List<object>();
                foreach (var parameter in constructorParameters)
                {
                    var dependency = GetModule(parameter.ParameterType);//lay module tuong ung tu IDContainer
                    moduleDependecies.Add(dependency);
                }
                //Inject cac dependency vao constructor cua module
                module = firstConstructor.Invoke(moduleDependecies.ToArray());
            }
            //Luu tru module va interface tuong ung
            RegisteredModules.Add(interfaceType, module);
        }
        private static object GetModule(Type interfaceType)// giup khoi tao cac doi tuong 
        {
            if (RegisteredModules.ContainsKey(interfaceType))
            {
                return RegisteredModules[interfaceType];
            }
            throw new Exception("Module not register");
        }
        //      [moi SetModule deu duoc luu trong Dictionary(RegisteredModules)]
        //   DIContainer.SetModule<IDatabase, Database>();
        //      => vi class Database khong co tham so => di vao ham if => new Database => trong dic(RegisteredModules) 
        //              se chua interface va instance Database
        //
        //   DIContainer.SetModule<Cart, Cart>();
        //        => vi class Cart implement chinh no, bo qua ham kiem tra, chay vao else, goi lai ham GetModule
        //               de instance cac parameter trong dic(RegisteredModules)
        //
    }   //   var myCart = DIContainer.GetModule<Cart>();
        //        => Lay tu dic(RegisteredModules) => return lai instance implement interface
}
