using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NetworkController.Logic.Controller
{

    public class ReflectoMatic
    {
        public static List<TR> CreateObjects<TR, TA>(Assembly assembly, out List<Exception> exceptions) where TR : class
        {
            var result = new List<TR>();
            exceptions = new List<Exception>();
            try
            {
                var types = assembly.GetTypes();
                foreach (var t in types)
                {
                    if (!t.IsAbstract && t.IsClass)
                    {
                        var serverAttribProvider =
                            t.GetCustomAttributes(typeof (TA), true).FirstOrDefault();
                        if (serverAttribProvider != null)
                        {
                            try
                            {
                                var ctr = t.GetConstructor(new Type[] {});
                                if (ctr != null)
                                {
                                    var obj = ctr.Invoke(new object[] {}) as TR;
                                    if (obj != null)
                                    {
                                        result.Add(obj);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                exceptions.Add(
                                    new Exception(
                                        string.Format("Failed to construct a {0}.", t)
                                        , ex)
                                    );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exceptions.Add(
                    new Exception(
                        string.Format("Failed to reflect types from assembly <{0}>.", assembly.FullName)
                        , ex)
                    );
            }
            return result;
        }
    }
}
