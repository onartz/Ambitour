using System;
using System.Collections.Generic;
using System.Text;

namespace Ambitour.CoucheMetier.ObjetsMetier
{
    class OF
    {
        public enum status_type{New, Started, Closed, Cancel};

        private int productId;

        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }

        private int quantity;

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        private int resourceId;

        public int ResourceId
        {
            get { return resourceId; }
            set { resourceId = value; }
        }
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        private status_type status;

        public status_type Status
        {
            get { return status; }
            set { status = value; }
        }

      
        public OF(string id, int productId, int quantity, int resourceId)
        {
            this.productId = productId;
            this.id = id;
            this.quantity = quantity;
            this.resourceId = resourceId;
        }

    }
}
