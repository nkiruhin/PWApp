using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAppApi.DataLayer
{
    /// <summary>
    /// Базовый интерфейс для сущностей  БД
    /// </summary>
    public interface IEntityBase<T>
    {
        public T Id { get; set; }
    }
}
