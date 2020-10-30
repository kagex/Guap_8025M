﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Шифрование_и_Дешифрование
{
    public partial class Form1 : Form
    {
        string [] alphabet = {"АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ ,.!?1234567890", "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ ", 
                                            "ABCDEFGHIJKLMNOPQRSTUVWXYZ ,.!?1234567890", "ABCDEFGHIJKLMNOPQRSTUVWXYZ " };
        string [] mask = {"[а-яёА-Яё]", "[а-яёА-Яё]", "[a-zA-Z]", "[a-zA-Z]" };
        int id = 0;
        string defaultMessage = "Однажды я гулял по песку, было классно";
        string defaultKey = "Солнце";
        public Form1()
        {
            InitializeComponent();
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            comboBox1.SelectedIndex = 0;
        }
        public char[] alphabetGenerator(string alphabetLetters = null) //Генератор алфавита
        {
            char[] alphabet = alphabetLetters.ToCharArray();
            return alphabet;
        }

        private string sentenceGenerator(string line)
        {
            string result = "";
            char[] sym = line.ToCharArray();
            result += sym[0];
            for (int i = 1; i< sym.Length; i++)
            {
                if(!Regex.IsMatch(sym[i-1].ToString(),@"[.!?]"))
                {
                    result += (sym[i].ToString()).ToLower();
                } else
                {
                    result += sym[i];
                }
            }
            return result;
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
            result = sentenceGenerator(result);
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
            result = sentenceGenerator(result);
            return result;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            button4.Enabled = true;
            if ((textBox1.Text == "") || (textBox2.Text == "")) {
                textBox1.Text = defaultMessage;
                textBox2.Text = defaultKey;
                label4.Visible = true;
            } else
            {
                label4.Visible = false;
            }
            if (radioButton1.Checked == true) {
                textBox3.Text = Encrypt(textBox1.Text, textBox2.Text, alphabetGenerator(alphabet[id])).ToString() ;
            }
            if (radioButton2.Checked == true)
            {
                textBox3.Text = Decrypt(textBox1.Text, textBox2.Text, alphabetGenerator(alphabet[id])).ToString();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            id = comboBox1.SelectedIndex;
            textBox2.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if ((textBox2.Text != "") && Regex.IsMatch(textBox2.Text, @mask[id]))
            {
                button1.Enabled = true;
                label5.Visible = false;
            } else
            {
                button1.Enabled = false;
                label5.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(openFileDialog1.FileName);
                textBox1.Text = sentenceGenerator(File.ReadLines(openFileDialog1.FileName).First());
                sr.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filename = saveFileDialog1.FileName;
                StreamWriter sw = new StreamWriter(filename, false, Encoding.UTF8);
                sw.WriteLine(textBox3.Text);
                sw.Close();
            }
        }
    }
}
