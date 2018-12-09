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
using System.Text.RegularExpressions;

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

        private void FilterContacts() {
            listBox1.Items.Clear();
            if (radioButton1.Checked)
            {
                foreach (Contact contact in contacts)
                    if (contact.GetName().ToLower().StartsWith(textBox6.Text.ToLower())) listBox1.Items.Add(contact);
            }
            else if (radioButton2.Checked)
            {
                foreach (Contact contact in contacts)
                    if (contact.GetSurname().ToLower().StartsWith(textBox6.Text.ToLower())) listBox1.Items.Add(contact);
            }
            else if (radioButton3.Checked)
            {
                foreach (Contact contact in contacts)
                    if (contact.GetTelephone().ToLower().StartsWith(textBox6.Text.ToLower())) listBox1.Items.Add(contact);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            numericUpDown1.Maximum = DateTime.Now.Year;
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
            if (isContact(textBox1.Text.Trim() + " " + textBox2.Text.Trim()))
                MessageBox.Show("Contact already exists!");
            else
            {
                if (textBox1.Text.Trim() == "" && textBox2.Text.Trim() == "") {
                    MessageBox.Show("Name and surname cannot both be empty!", "Empty name and surname!");
                }
                else if (!Regex.IsMatch(textBox3.Text.Trim(), @"[0-9]+$"))
                {
                    MessageBox.Show("Telephone must contain only numbers", "Incorrect telephone number!");
                }
                else if (!Regex.IsMatch(textBox4.Text.Trim(), @"[0-9A-Za-z]+@[A-Za-z]+\.[A-Za-z]{2,3}$") && textBox4.Text.ToString().Trim() != "")
                {
                    MessageBox.Show("Your email is incorrect!", "Incorrect email!");
                }
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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            numericUpDown1.Value = 1;
            numericUpDown2.Value = 1;
            numericUpDown3.Value = 2000;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Hide();
            new Form2(FindContact(listBox1.SelectedItem.ToString()), contacts).ShowDialog();
            this.Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.ForeColor = Regex.IsMatch(textBox3.Text.Trim(), "[0-9]+$") ? Color.Black : Color.Red;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            textBox4.ForeColor = Regex.IsMatch(textBox4.Text.Trim(), @"[0-9A-Za-z]+@[A-Za-z]+\.[A-Za-z]{2,3}$") ? Color.Black : Color.Red;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            FilterContacts();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            FilterContacts();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            FilterContacts();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            FilterContacts();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
