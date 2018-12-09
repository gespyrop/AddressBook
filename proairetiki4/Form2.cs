using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proairetiki4
{
    public partial class Form2 : Form
    {
        List<Contact> contacts;
        Contact contact;
        string initialCredentials;
        public Form2()
        {
            InitializeComponent();
        }

        public Form2(Contact contact, List<Contact> contacts) {
            InitializeComponent();
            this.contacts = contacts;
            this.contact = contact;
        }
    
        private void Form2_Load(object sender, EventArgs e)
        {
            initialCredentials = contact.ToString();
            this.Text = initialCredentials;
            textBox1.Text = contact.GetName();
            textBox2.Text = contact.GetSurname();
            textBox3.Text = contact.GetTelephone();
            textBox4.Text = contact.GetEmail();
            textBox5.Text = contact.GetAddress();
            string[] birthday = contact.GetBirthday().Split(' ');
            numericUpDown1.Value = int.Parse(birthday[0]);
            numericUpDown2.Value = int.Parse(birthday[1]);
            numericUpDown3.Value = int.Parse(birthday[2]);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            contacts.Remove(contact);
            BinaryFormatter bf = new BinaryFormatter();
            Stream st = new FileStream("contacts.txt", FileMode.OpenOrCreate);
            bf.Serialize(st, contacts);
            st.Close();
            this.Hide();
            new Form1().ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Form1().ShowDialog();
            this.Close();
        }
    }
}
