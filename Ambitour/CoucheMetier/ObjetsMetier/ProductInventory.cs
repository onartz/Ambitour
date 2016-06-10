using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ambitour
{
    public partial class ProductInventory
    {
        public event EventHandler ModelChanged;
        EventHandler handler;

        public static int LOCATION_ID = 23;
        public enum inout { INPUT, OUTPUT };
        private string name;
        private string aid;
        inout type;

        /// <summary>
        /// Jade agent AID
        /// </summary>
        public string Aid
        {
            get { return aid; }
            set { aid = value; }
        }

        /// <summary>
        /// Input/Output type
        /// </summary>
        public inout Type
        {
            get { return type; }
            set { type = value; }
        }


        public void Update(int quantity, int capacity){
            
            if (this.Quantity != quantity || this.Capacity != capacity)
            {
                if (handler != null)
                    handler(this, EventArgs.Empty);
            }

        }

        //public ProductInventory()
        //{
        //    //productId = -1;
        //    //quantity = 0;
        //    //locationId = LOCATION_ID;
        //    //capacity = UInt16.MaxValue;
        //    type = inout.INPUT;
        //    handler = ModelChanged;
        //}

        public ProductInventory(int productId, int locationId)
        {
            _ProductID = productId;
            _LocationID = locationId;
            _Quantity = 0;
            _Capacity = int.MaxValue;
            type = inout.INPUT;
            name = String.Format("invTBI540-{0}", productId);
            aid = String.Format("{0}@{1}:{2}/JADE", name, Ambitour.CoucheMetier.GlobalSettings.Default.jadeServerAddress,
                Ambitour.CoucheMetier.GlobalSettings.Default.jadePort);


        }

        public bool Modify(short quantity)
        {
            bool res = false;
            if ((_Quantity + quantity) <= _Capacity)
            {
                _Quantity += quantity;
                res = true;
            }
            return res;
        }

      

    }
}
