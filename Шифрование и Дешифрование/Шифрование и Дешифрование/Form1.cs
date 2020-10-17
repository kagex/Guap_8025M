using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Шифрование_и_Дешифрование
{
    public partial class Form1 : Form
    {
        string alphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ ,.!?1234567890";
        string defaultMessage = "Однажды я гулял по песку, было классно";
        string defaultKey = "Солнце";
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }
        public char[] alphabetGenerator(string alphabetLetters = null) //Генератор алфавита
        {
            if ((alphabetLetters.Length == 0) || (alphabetLetters == null))
            {
                alphabetLetters = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ ,.!?1234567890";
            }
            char[] alphabet = alphabetLetters.ToCharArray();
            return alphabet;
        }
        private string Encrypt(string message, string key, char[] alphabet)
        {
            message = message.ToUpper();
            key = key.ToUpper();

            int alphabetLength = alphabet.Length;
            string result = "";
            int key_index = 0;

            foreach (char element in message)
            {
                int c = (Array.IndexOf(alphabet, element) + Array.IndexOf(alphabet, key[key_index])) % alphabetLength;
                result += alphabet[c];
                key_index++;
                if ((key_index + 1) == key.Length)
                    key_index = 0;
            }
            return result;
        }
        private string Decrypt(string message, string key, char[] alphabet)
        {
            message = message.ToUpper();
            key = key.ToUpper();

            int alphabetLength = alphabet.Length;
            string result = "";
            int key_index = 0;

            foreach (char element in message)
            { 
                int p = (Array.IndexOf(alphabet, element) + alphabetLength - Array.IndexOf(alphabet, key[key_index])) % alphabetLength;
                result += alphabet[p];
                key_index++;
                if ((key_index + 1) == key.Length)
                    key_index = 0;
            }
            return result;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "") || (textBox1.Text == "")) {
                textBox1.Text = defaultMessage;
                textBox2.Text = defaultKey;
                label4.Visible = true;
            } else
            {
                label4.Visible = false;
            }
            if (radioButton1.Checked == true) {
                textBox3.Text = Encrypt(textBox1.Text, textBox2.Text, alphabetGenerator(alphabet)).ToString() ;
            }
            if (radioButton2.Checked == true)
            {
                textBox3.Text = Decrypt(textBox1.Text, textBox2.Text, alphabetGenerator(alphabet)).ToString();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                alphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ ,.!?1234567890";
            } else
            {
                alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ ,.!?1234567890";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
