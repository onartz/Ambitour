using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ambitour.CoucheMetier.ObjetsMetier
{
    public class Inventory
    {
        public enum inout { INPUT, OUTPUT };

        int productId;
        UInt16 quantity;
        UInt16 capacity;
        inout type;

        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }
        public UInt16 Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        public UInt16 Capacity
        {
            get { return capacity; }
            set { capacity = value; }
        }
        public inout Type
        {
            get { return type; }
            set { type = value; }
        }

        public Inventory()
        {
            productId = -1;
            quantity = 0;
            capacity = UInt16.MaxValue;
            type = inout.INPUT;
        }

        public bool Add(UInt16 quantity)
        {
            bool res = false;
            if ((this.quantity + quantity) <= capacity)
            {
                this.quantity += quantity;
                res = true;
            }
            return res;
        }

        public bool Remove(UInt16 quantity)
        {
            bool res = false;
            if (quantity >= this.quantity)
            {
                this.quantity -= quantity;
                res = true;
            }
            return res;
        }

    }
}
