using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace coffe
{
    public partial class Form1 : Form
    {
        dbCoffeeShopEntities context = new dbCoffeeShopEntities();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            menuBindingSource.DataSource = context.Menus.ToList();
            var result = context.TypeCoffees.OrderBy(T => T.Name).Select(T => new { T.idtype, T.Name});
            foreach(var item in result)
            {
                comboBox1.Items.Add(new ComboBoxMyItems(item.idtype, item.Name));
                dropdowAdd.Items.Add(new ComboBoxMyItems(item.idtype, item.Name));
                comboBox2.Items.Add(new ComboBoxMyItems(item.idtype, item.Name));
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textId.Text);
            var delete = context.Menus.Where(M => M.idmenu == id).First();
            context.Menus.Remove(delete);
            int current = context.SaveChanges();
            if(current > 0)
            {
                MessageBox.Show("Delete LawJaaaa");
                menuBindingSource.DataSource = context.Menus.ToList();
            }
        }

        private void edit_Click(object sender, EventArgs e)
        {
            menuBindingSource.EndEdit();
            int id = int.Parse(textId.Text);
            int current = context.SaveChanges();
            if (current > 0)
            {
                MessageBox.Show("Edit LawJaaaa");
                menuBindingSource.DataSource = context.Menus.ToList();
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public byte[] ImageToByteArray(System.Drawing.Image image)
        {
            var ms = new MemoryStream();
            image.Save(ms, image.RawFormat);
            return ms.ToArray();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = System.Drawing.Image.FromFile(openFileDialog1.FileName);
            }
        }

        private void Addmenu_Click(object sender, EventArgs e)
        {
            Menu newMenu = new Menu();
            newMenu.name = textName.Text;
            newMenu.price = int.Parse(textPrice.Text);
            newMenu.image = ImageToByteArray(pictureBox2.Image);
            newMenu.idtype = ((ComboBoxMyItems)(dropdowAdd.SelectedItem)).value;

            context.Menus.Add(newMenu);
            
            int current = context.SaveChanges();
            if (current > 0) {
                MessageBox.Show("add LawJaaa");
                menuBindingSource.DataSource = context.Menus.ToList();
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image = System.Drawing.Image.FromFile(openFileDialog1.FileName);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int typeId =((ComboBoxMyItems)(comboBox2.SelectedItem)).value;
            menuBindingSource.DataSource = context.Menus.Where(m => m.idtype == typeId).ToList();

            
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string id = dataGridView3.SelectedRows[0].Cells[0].Value.ToString();
            string name = dataGridView3.SelectedRows[0].Cells[1].Value.ToString();
            string price = dataGridView3.SelectedRows[0].Cells[2].Value.ToString();
            string amount = numericUpDown1.Value.ToString();
            int total = int.Parse(price) * int.Parse(amount);
            string[] item = {id, name, price, amount, total.ToString() };
            ListViewItem lvitem = new ListViewItem(item);
            listView1.Items.Add(lvitem);
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count > 0) {
                int select = listView1.SelectedIndices[0];
                listView1.Items.RemoveAt(select);
            }
          
        }

        private void button7_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                Order order = new Order();
                

                order.idmenu = int.Parse(item.SubItems[0].Text);
                order.number = int.Parse(item.SubItems[3].Text);
                order.priceamount = int.Parse(item.SubItems[4].Text);
                order.date = DateTime.Now;

                context.Orders.Add(order);
            }
           int change =   context.SaveChanges();
            if (change > 0) {
                MessageBox.Show("Save ok");
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime dt = dateTimePicker1.Value;
            Console.WriteLine(dateTimePicker1.Value);
            orderBindingSource1.DataSource = context.Orders.Where(o => o.date.Value.Year == dt.Year && o.date.Value.Month == dt.Month && o.date.Value.Day == dt.Day).Select(s=>new { s.oid, s.Menu.name, s.Menu.price, s.number, s.priceamount, s.date}). ToList();
        }
    }
}
