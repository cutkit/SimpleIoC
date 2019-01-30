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
            RegisteredModeles = new Dictionary<Type, object>();
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
            }
        }
    }
}
