using System;
using System.Collections.Generic;
using System.Text;

namespace WebLab.DAL.Entities
{
    public class Flower
    {
        public int FlowerId { get; set; } // id цветка
        public string FlowerName { get; set; } // название цветка
        public string FlowerDescription { get; set; } // описание
        public int FlowerPrice { get; set; } // Стоимость
        public string FlowerImage { get; set; } // имя файла изображения
                                          // Навигационные свойства
        /// <summary>
        /// группа
        /// </summary>
        public int FlowerGroupId { get; set; }
        public FlowerGroup Group { get; set; }
    }
    public class FlowerGroup
    {
        public int FlowerGroupId { get; set; }
        public string GroupName { get; set; }
        /// <summary>
        /// Навигационное свойство 1-ко-многим
        /// </summary>
        public List<Flower> Flowers { get; set; }
    }
}
