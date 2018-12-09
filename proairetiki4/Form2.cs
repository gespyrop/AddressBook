using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
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

        private bool isContact(string c)
        {
            foreach (Contact contact in contacts)
                if (contact.ToString() == c) return true;
            return false;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            numericUpDown1.Maximum = DateTime.Now.Year;
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
            if (isContact(textBox1.Text.Trim() + " " + textBox2.Text.Trim()) && textBox1.Text.Trim() + " " + textBox2.Text.Trim() != contact.ToString())
                MessageBox.Show("Contact already exists!");
            else {
                if (textBox1.Text.Trim() == "" && textBox2.Text.Trim() == "")
                {
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
                    contacts.Remove(contact);
                    contact = new Contact()
                        .AddName(textBox1.Text)
                        .AddSurname(textBox2.Text)
                        .AddTelephone(textBox3.Text)
                        .AddEmail(textBox4.Text)
                        .AddAddress(textBox5.Text)
                        .AddBirthday(numericUpDown1.Value.ToString() + " " + numericUpDown2.Value.ToString() + " " + numericUpDown3.Value.ToString());
                    contacts.Add(contact);
                    BinaryFormatter bf = new BinaryFormatter();
                    Stream st = new FileStream("contacts.txt", FileMode.OpenOrCreate);
                    bf.Serialize(st, contacts);
                    st.Close();
                    this.Hide();
                    new Form1().ShowDialog();
                    this.Close();
                }
            }
            
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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.ForeColor = Regex.IsMatch(textBox3.Text.Trim(), "[0-9]+$") ? Color.Black : Color.Red;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            textBox4.ForeColor = Regex.IsMatch(textBox4.Text.Trim(), @"[0-9A-Za-z]+@[A-Za-z]+\.[A-Za-z]{2,3}$") ? Color.Black : Color.Red;
        }
    }
}
