using Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.Shop.Composite
{
    public abstract class ItemProperty
    {
        public int valueInt { get; set; }
        public string valueString { get; set; }

        public List<ItemProperty> Properties { get; set; }

        public ItemProperty(string value)
        {
            this.valueString = value;
        }

        public ItemProperty(int value)
        {
            this.valueInt = value;
        }


        public ItemProperty()
        {
        }

        public void AddProperty(ItemProperty item)
        {
            Properties.Add(item);
        }


        public T GetPropertyValue <T>(Type property)
        {
            foreach (ItemProperty itemProperty in Properties)
            {
                if (itemProperty.GetType().ToString() == property.FullName)
                {
                    if (typeof(T) == typeof(string))
                    {
                        return (T)Convert.ChangeType(itemProperty.valueString, typeof(T));
                    }
                    else if (typeof(T) == typeof(int))
                    {
                        return (T)Convert.ChangeType(itemProperty.valueInt, typeof(T));
                    }
                }
            }
            return default;
        }

        public ItemProperty GetProperty(Type property)
        {
            foreach (ItemProperty itemProperty in Properties)
            {
                if (itemProperty.GetType().ToString() == property.FullName)
                {
                    return itemProperty;
                }
            }
            return null;
        }


    }
}
