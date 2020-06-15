using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebLab.DAL.Entities;

namespace LR1Project.Models
{
    public class Cart
    {
        public Dictionary<int, CartItem> Items { get; set; }
        public Cart()
        {
            Items = new Dictionary<int, CartItem>();
        }
        /// <summary>
        /// Количество объектов в корзине
        /// </summary>
        public int Count
        {
            get
            {
                return Items.Sum(item => item.Value.Quantity);
            }
        }
        /// <summary>
        /// Стоимость
        /// </summary>
        public int Price
        {
            get
            {
                return Items.Sum(item => item.Value.Quantity * item.Value.Flower.FlowerPrice);
            }
        }
        /// <summary>
        /// Добавление в корзину
        /// </summary>
        /// <param name="flower">добавляемый объект</param>
        public virtual void AddToCart(Flower flower)
        {
            // если объект есть в корзине
            // то увеличить количество
            if (Items.ContainsKey(flower.FlowerId))
                Items[flower.FlowerId].Quantity++;
            // иначе - добавить объект в корзину
            else
                Items.Add(flower.FlowerId, new CartItem
                {
                    Flower = flower,
                    Quantity = 1
                });
        }
        /// <summary>
        /// Удалить объект из корзины
        /// </summary>
        /// <param name="id">id удаляемого объекта</param>
        public virtual void RemoveFromCart(int id)
        {
            Items.Remove(id);
        }
        /// <summary>
        /// Очистить корзину
        /// </summary>
        public virtual void ClearAll()
        {
            Items.Clear();
        }
    }
    /// <summary>
    /// Клас описывает одну позицию в корзине
    /// </summary>
    public class CartItem
    {
        public Flower Flower { get; set; }
        public int Quantity { get; set; }
    }
}
