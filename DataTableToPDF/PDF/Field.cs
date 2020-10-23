using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTableToPDF.PDF
{
    /// <summary>
    /// Data type of a field
    /// </summary>
    public enum DataType
    {
        String,     //default
    }

    /// <summary>
    /// Stores information about a field
    /// </summary>
    public struct Field
    {
        public string Title;
        public DataType Type;
        public double Width; //relative width???

        public Field(string title, DataType type, double width)
        {
            Title = title;
            Type = type;
            Width = width;     //Relative width; can be used for display, print, PDF, ...
        }
    }
}
