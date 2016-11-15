using System.Collections.Generic;

namespace ConsoleUI
{
    /// <summary>
    /// Класс "таблица имен переменных" - содержит именованные значения
    /// различных типов. Позволяет извлекать типизированные значения 
    /// по имени переменной
    /// </summary>
    public class NameTable
    {
        Dictionary<string, object> vars = new Dictionary<string, object>();

        /// <summary>
        /// Установить значение переменной
        /// </summary>
        /// <param name="name">имя переменной</param>
        /// <param name="value">значение переменной</param>
        public void Set(string name, object value){
            vars[name] = value;
        }
        /// <summary>
        /// Удалить переменную
        /// </summary>
        /// <param name="name">имя переменной</param>
        public void Remove(string name)
        {
            vars.Remove(name);
        }
        
        /// <summary>
        /// Проверить, есть ли переменная в таблице имен
        /// </summary>
        /// <param name="name">имя переменной</param>
        /// <returns>true, если переменная есть в таблице, иначе false</returns>
        public bool Has(string name)
        {
            return vars.ContainsKey(name);
        }
        /// <summary>
        /// Возвратить значение переменной типа Т для ссылочных типов
        /// </summary>
        /// <typeparam name="T">тип возвращаемого значение</typeparam>
        /// <param name="name">имя переменной</param>
        /// <returns>Значение переменной. Если нет в таблице или не совпадает по типу, возвращаем null</returns>
        public T Resolve<T>(string name)
            where T:class
        {
            if (!vars.ContainsKey(name)){
                return null;
            }
            return vars[name] as T;
        }
        /// <summary>
        /// Возвратить значение переменной типа Т для ссылочных типов
        /// </summary>
        /// <typeparam name="T">тип возвращаемого значение</typeparam>
        /// <param name="name">имя переменной</param>
        /// <returns>Значение переменной. Если нет в таблице или не совпадает по типу, возвращаем null</returns>
        public T? ResolveValue<T>(string name)
            where T:struct
        {
            if (!vars.ContainsKey(name)){
                return null;
            }
            try{
                return (T)vars[name];
            } catch {
                return null;
            }
        }
    }
}
