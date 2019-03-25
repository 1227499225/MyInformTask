using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MuZiYangNote.RewriteControlClass
{

    /*
    * ============================================================
    * 函数名：重写DataGridView单元格
    * 作者：木子杨
    * 版本：1.0
    * 日期： 
    * 描述： 
    * ============================================================
    */
    public class TextAndImageColumn: DataGridViewTextBoxColumn
    {
            private Image imageValue;
            private Size imageSize;

            public TextAndImageColumn()
            {
                this.CellTemplate = new TextAndImageCell();
            }

            public override object Clone()
            {
                TextAndImageColumn c = base.Clone() as TextAndImageColumn;
                c.imageValue = this.imageValue;
                c.imageSize = this.imageSize;
                return c;
            }

            public Image Image
            {
                get { return this.imageValue; }
                set
                {
                    if (this.Image != value)
                    {
                        this.imageValue = value;
                        this.imageSize = value.Size;

                        if (this.InheritedStyle != null)
                        {
                            Padding inheritedPadding = this.InheritedStyle.Padding;
                            this.DefaultCellStyle.Padding = new Padding(imageSize.Width,
                        inheritedPadding.Top, inheritedPadding.Right,
                        inheritedPadding.Bottom);
                        }
                    }
                }
            }
            private TextAndImageCell TextAndImageCellTemplate
            {
                get { return this.CellTemplate as TextAndImageCell; }
            }
            internal Size ImageSize
            {
                get { return imageSize; }
            }
        }

        public class TextAndImageCell : DataGridViewTextBoxCell
        {
            private Image imageValue;
            private Size imageSize;

            public override object Clone()
            {
                TextAndImageCell c = base.Clone() as TextAndImageCell;
                c.imageValue = this.imageValue;
                c.imageSize = this.imageSize;
                return c;
            }

            public Image Image
            {
                get
                {
                    if (this.OwningColumn == null ||
               this.OwningTextAndImageColumn == null)
                    {

                        return imageValue;
                    }
                    else if (this.imageValue != null)
                    {
                        return this.imageValue;
                    }
                    else
                    {
                        return this.OwningTextAndImageColumn.Image;
                    }
                }
                set
                {
                    if (this.imageValue != value)
                    {
                        this.imageValue = value;
                        this.imageSize = value.Size;

                        Padding inheritedPadding = this.InheritedStyle.Padding;
                        this.Style.Padding = new Padding(imageSize.Width,
                       inheritedPadding.Top, inheritedPadding.Right,
                       inheritedPadding.Bottom);
                    }
                }
            }
            protected override void Paint(Graphics graphics, Rectangle clipBounds,
           Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState,
           object value, object formattedValue, string errorText,
           DataGridViewCellStyle cellStyle,
           DataGridViewAdvancedBorderStyle advancedBorderStyle,
           DataGridViewPaintParts paintParts)
            {
                // Paint the base content
                base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState,
                  value, formattedValue, errorText, cellStyle,
                  advancedBorderStyle, paintParts);

                if (this.Image != null)
                {
                    // Draw the image clipped to the cell.
                    System.Drawing.Drawing2D.GraphicsContainer container =
                   graphics.BeginContainer();

                    graphics.SetClip(cellBounds);
                    graphics.DrawImageUnscaled(this.Image, cellBounds.Location);

                    graphics.EndContainer(container);
                }
            }

            private TextAndImageColumn OwningTextAndImageColumn
            {
                get { return this.OwningColumn as TextAndImageColumn; }
            }
        }
}



 //33             this.dataGridView1.AutoGenerateColumns = false;
 //34             TextAndImageColumn ColumnRoleName = new TextAndImageColumn();
 //35             ColumnRoleName.DataPropertyName = "RoleName";
 //36             ColumnRoleName.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
 //37             ColumnRoleName.Name = "RoleName";
 //38             ColumnRoleName.HeaderText = "权限名称";
 //39             ColumnRoleName.Width = 100;
 //40             this.dataGridView1.Columns.Add(ColumnRoleName);

//     66         private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
// 67         {
// 68             #region 第二列
// 69             if (e.ColumnIndex == 1)
// 70             {
// 71                 TextAndImageCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] as TextAndImageCell;
// 72                 if (cell != null && e.Value != null)
// 73                 {
// 74                     try
// 75                     {
// 76                         string ajzt = cell.Value.ToString();
// 77                         string path = imagePath;
// 78                         switch (ajzt)
// 79                         {
// 80                             case "发布者":
// 81                                 path += "1.png";
// 82                                 break;
// 83                             case "浏览者":
// 84                                 path += "2.png";
// 85                                 break;                            
// 86                             default:
// 87                                 path += "3.png";
// 88                                 break;
// 89                         }
// 90                         cell.Image = GetImage(path);
// 91                     }
// 92                     catch (Exception ex)
// 93                     {
// 94 
// 95                     }
// 96                 }
// 97             }
// 98             #endregion
// 99         }
//100 
//101         public System.Drawing.Image GetImage(string path)
//102         {
//103             return System.Drawing.Image.FromFile(path);
//104         }