using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 贪吃蛇.UI
{
    /// <summary>
    /// 非常值得学习的UI单例模式基类，在完全了解之前少用
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseUI<T> where T : BaseUI<T>, new()
    {
        private static class Holder
        {
            internal static readonly T Value = new T();
        }

        public static T Instance => Holder.Value;

        protected BaseUI() { }
    }
}
