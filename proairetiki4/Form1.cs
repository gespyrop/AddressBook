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
using System.Runtime.Serialization;

namespace proairetiki4
{
    public partial class Form1 : Form
    {
        List<Contact> contacts;

        public Form1()
        {
            InitializeComponent();
        }

        private bool isContact(string c) {
            foreach (Contact contact in contacts)
                if (contact.ToString() == c) return true;
            return false;
        }

        private Contact FindContact(string c)
        {
            foreach (Contact contact in contacts)
                if (contact.ToString() == c) return contact;
            return new Contact();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BinaryFormatter bf = new BinaryFormatter();
            Stream st = new FileStream("contacts.txt", FileMode.OpenOrCreate);
            try
            {
                contacts = (List<Contact>)bf.Deserialize(st);
                st.Close();
            }
            catch (SerializationException E)
            {
                contacts = new List<Contact>();
            }
            finally
            {
                st.Close();
            }
            foreach (Contact c in contacts) listBox1.Items.Add(c);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isContact(textBox1.Text.Trim() + " " + textBox2.Text.Trim())) MessageBox.Show("Contact already exists!");
            else
            {
                Contact c = new Contact()
                    .AddName(textBox1.Text.Trim())
                    .AddSurname(textBox2.Text.Trim())
                    .AddTelephone(textBox3.Text.Trim())
                    .AddEmail(textBox4.Text.Trim())
                    .AddAddress(textBox5.Text.Trim())
                    .AddBirthday(numericUpDown1.Value.ToString() + " " + numericUpDown2.Value.ToString() + " " + numericUpDown3.Value.ToString());

                contacts.Add(c);
                BinaryFormatter bf = new BinaryFormatter();
                Stream st = new FileStream("contacts.txt", FileMode.OpenOrCreate);
                bf.Serialize(st, contacts);
                st.Close();
                listBox1.Items.Add(c);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
