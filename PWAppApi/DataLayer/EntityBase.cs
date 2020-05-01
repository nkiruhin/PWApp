using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PWAppApi.DataLayer
{
    /// <summary>
    /// Базовый класс для сущностей  БД
    /// </summary>
    /// <typeparam name="T">Тип идентификатора</typeparam>
    public class EntityBase<T> : IEntityBase<T>
    {
        [Key]
        [Display(Name = "Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public T Id { get; set; }
    }
}
