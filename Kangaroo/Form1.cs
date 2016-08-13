using System;
using System.Windows.Forms;

namespace Kangaroo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string alphabet = "";
        private string alphabet0 = "АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        private string alphabet1 = "АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЬЫЪЭЮЯ";
        private string abv =        "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        private string vba =        "ЯЮЭЬЫЪЩШЧЦХФУТСРПОНМЛКЙИЗЖЁЕДГВБА";
        private string vba0 = "ЯЮЭЬЫЪЩШЧЦХФУТСРПОНМЛКЙИЗЖЕДГВБА";
        private string qwertyRU =   "ЙЦУКЕНГШЩЗХЪЭЖДЛОРПАВЫФЯЧСМИТЬБЮЁ";
        private bool exit = false;
        
        //Загрузка формы
        private void Form1_Load(object sender, EventArgs e)
        {
            alphabet = abv;
            
            comboBoxZamEncKey.Items.Add("Я-А");
            comboBoxZamEncKey.Items.Add("ЙЦУКЕН");
            comboBoxZamEncKey.Items.Add("");

            comboBoxZamDecKey.Items.Add("Я-А");
            comboBoxZamDecKey.Items.Add("ЙЦУКЕН");

            comboBoxZamEncKey.SelectedIndex = 0;
            comboBoxZamDecKey.SelectedIndex = 0;
        }

        //Отключаем закрывалку        
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            if (exit == true)
            {
                e.Cancel = false;
            }
        }

        //Кнопка выхода
        private void buttonExit_Click(object sender, EventArgs e)
        {
            exit = true;
            this.Close();
        }

        //Поверка на некорректные символы
        private bool CheckLetter(string str)
        {
            string checkLetter = "";

            for (int i = 0; i < str.Length; i++)
            {
                for (int j = 0; j < alphabet.Length; j++)
                {
                    if (str[i].Equals(alphabet[j]))
                    {
                        checkLetter += str[i];
                        break;
                    }
                }
            }

            if (str.Length == checkLetter.Length)
            {
                return true;
            }

            else return false;
        }

        //Справка симметричный алгоритм шифрования
        private void buttonSimmQ_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Симметричный алгоритм шифрования - это метод шифрования, в котором для шифрования и дешифровывания применяется один и тот же криптографический ключ."
            + "\nШифрование производится по формуле: An + Cn = (mod N), 0 <= Bn <= N - 1"
            + "\nГде An - порядковый номер символа исходного сообщения в алфавите,"
            + "\nа Cn - порядковый номер соответствующего символа криптографического ключа в алфавите.");
        }

        //Перекинуть Симметричное шифрование >>> Симметричная дешифровка
        private void buttonSimmR_Click(object sender, EventArgs e)
        {
            textBoxSimmDecKey.Text = textBoxSimmEncKey.Text;
            textBoxSimmDecWord.Text = textBoxSimmEncResult.Text;
        }

        //Перекинуть Симметричное шифрование <<< Симметричная дешифровка
        private void buttonSimmL_Click(object sender, EventArgs e)
        {
            textBoxSimmEncWord.Text = textBoxSimmDecResult.Text;
            textBoxSimmEncKey.Text = textBoxSimmDecKey.Text;
        }
        
        //Симметричное шифрование
        private void buttonSimmEncOK_Click(object sender, EventArgs e)
        {            
            if (textBoxSimmEncWord.Text != "" && textBoxSimmEncKey.Text != "")
            {
                string sourceWord = textBoxSimmEncWord.Text.ToUpper();
                string key = textBoxSimmEncKey.Text.ToUpper();

                if (CheckLetter(key) == true)
                {
                    if (key.Length < sourceWord.Length)
                    {
                        int sk = sourceWord.Length - key.Length;

                        for (int i = 0; i < sk; i++)
                        {
                            key += key[i];
                        }
                    }

                    textBoxSimmEncWord.Text = textBoxSimmEncWord.Text.ToUpper();
                    textBoxSimmEncKey.Text = key.ToUpper();
                    string enc = "";

                    for (int i = 0; i < sourceWord.Length; i++)
                    {
                        int a = Array.IndexOf(alphabet1.ToCharArray(), sourceWord[i]);
                        int c = Array.IndexOf(alphabet1.ToCharArray(), key[i]);
                        int b = (a + c) % alphabet1.Length + 1;

                        if (0 <= b & b <= alphabet1.Length)
                        {
                            if (b >= alphabet1.Length)
                            {
                                b -= alphabet1.Length;
                            }

                            enc += alphabet1[b];
                        }
                    }

                    textBoxSimmEncResult.Text = enc;
                    labelSimmEncResult.Visible = false;
                }

                else MessageBox.Show("Используются некорректные символы");
            }

            else MessageBox.Show("Введите данные");
        }

        //Симметричная дешифровка
        private void buttonSimmDecOK_Click(object sender, EventArgs e)
        {
            if (textBoxSimmDecWord.Text != "" && textBoxSimmDecKey.Text != "")
            {
                string enc = textBoxSimmDecWord.Text.ToUpper();
                string key = textBoxSimmDecKey.Text.ToUpper();

                if (CheckLetter(enc) == true && CheckLetter(key) == true)
                {
                    if (key.Length < enc.Length)
                    {
                        int sk = enc.Length - key.Length;

                        for (int i = 0; i < sk; i++)
                        {
                            key += key[i];
                        }
                    }

                    textBoxSimmDecWord.Text = textBoxSimmDecWord.Text.ToUpper();
                    textBoxSimmDecKey.Text = key.ToUpper();
                    string dec = "";

                    for (int i = 0; i < enc.Length; i++)
                    {
                        int b = Array.IndexOf(alphabet1.ToCharArray(), enc[i]);
                        int c = Array.IndexOf(alphabet1.ToCharArray(), key[i]);
                        int a = (b - c) % alphabet1.Length - 1;

                        if (0 <= a & a <= alphabet1.Length)
                        {
                            dec += alphabet1[a];
                        }

                        if (0 > a & a > -alphabet1.Length)
                        {
                            a += alphabet1.Length;
                            dec += alphabet1[a];
                        }
                    }

                    textBoxSimmDecResult.Text = dec;
                }

                else MessageBox.Show("Используются некорректные символы");
            }

            else MessageBox.Show("Введите данные");
        }

        //Очистить поля симметричное шифрование
        private void buttonSimmEncClean_Click(object sender, EventArgs e)
        {
            textBoxSimmEncWord.Text = "";
            textBoxSimmEncKey.Text = "";
            textBoxSimmEncResult.Text = "";
        }

        //Очистить поля симметричная дешифровка
        private void buttonSimmDecClean_Click(object sender, EventArgs e)
        {
            textBoxSimmDecWord.Text = "";
            textBoxSimmDecKey.Text = "";
            textBoxSimmDecResult.Text = "";
        }

        //Пример симметричный алгоритм шифрования
        private void buttonSimmEx_Click(object sender, EventArgs e)
        {
            textBoxSimmEncWord.Text = "ОПЕРАЦИЯНАЧИНАЕТСЯВВОСКРЕСЕНЬЕ";
            textBoxSimmEncKey.Text = "ВОЛОГДА";
            textBoxSimmDecWord.Text = "СЯСАДЫЙВЭМЖМТБЗВЮОЁЖПФЪЭФХЙОЯФ";
            textBoxSimmDecKey.Text = "ВОЛОГДА";
        }

        //Справка Шифрование заменой
        private void buttonZamQ_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Шифрование заменой - это метод шифрования, который состоит в замене каждого символа открытого текста на соответствующий ему символ измененного алфавита, полученного путем случайной перестановки символов");
        }

        //Перекинуть Шифрование заменой <<< Дешифровка заменой
        private void buttonZamL_Click(object sender, EventArgs e)
        {
            textBoxZamEncWord.Text = textBoxZamDecResult.Text;
            comboBoxZamEncKey.SelectedIndex = comboBoxZamDecKey.SelectedIndex;
        }

        //Перекинуть Шифрование заменой >>> Дешифровка заменой
        private void buttonZamR_Click(object sender, EventArgs e)
        {
            comboBoxZamDecKey.SelectedIndex = comboBoxZamEncKey.SelectedIndex;
            textBoxZamDecWord.Text = textBoxZamEncResult.Text;
        }

        //Шифрование заменой
        private void buttonZamEncOK_Click(object sender, EventArgs e)
        {
            if (textBoxZamEncWord.Text != "" && comboBoxZamEncKey.SelectedIndex != -1)
            {
                string sourceWord = textBoxZamEncWord.Text.ToUpper();

                if (CheckLetter(sourceWord) == true)
                {
                    textBoxZamEncWord.Text = textBoxZamEncWord.Text.ToUpper();
                    string nabc = "";

                    switch (comboBoxZamEncKey.SelectedIndex)
                    {
                        case 0: nabc = vba0;
                            break;

                        case 1: nabc = qwertyRU;
                            break;

                        default:
                            break;
                    }

                    string enc = "";

                    for (int i = 0; i < sourceWord.Length; i++)
                    {
                        int s = Array.IndexOf(alphabet0.ToCharArray(), sourceWord[i]);
                        enc += nabc[s];
                    }

                    textBoxZamEncResult.Text = enc;
                }

                else MessageBox.Show("Используются некорректные символы");
            }

            else MessageBox.Show("Введите данные");
        }

        //Дешифровка заменой
        private void buttonZamDecOK_Click(object sender, EventArgs e)
        {
            if (textBoxZamDecWord.Text != "" && comboBoxZamEncKey.SelectedIndex != -1)
            {
                string enc = textBoxZamDecWord.Text.ToUpper();

                if (CheckLetter(enc) == true)
                {
                    textBoxZamDecWord.Text = textBoxZamDecWord.Text.ToUpper();
                    string nabc = "";

                    switch (comboBoxZamEncKey.SelectedIndex)
                    {
                        case 0: nabc = vba0;
                            break;

                        case 1: nabc = qwertyRU;
                            break;

                        default:
                            break;
                    }

                    string dec = "";

                    for (int i = 0; i < enc.Length; i++)
                    {
                        for (int j = 0; j < alphabet0.Length; j++)
                        {
                            if (enc[i].Equals(alphabet0[j]))
                            {
                                int s = Array.IndexOf(nabc.ToCharArray(), enc[i]);
                                dec += alphabet0[s];
                                break;
                            }
                        }
                    }

                    textBoxZamDecResult.Text = dec;
                }

                else MessageBox.Show("Используются некорректные символы");
            }

            else MessageBox.Show("Введите данные");
        } 

        //Очистить поля Шифрование заменой
        private void buttonZamEncClean_Click(object sender, EventArgs e)
        {
            textBoxZamEncWord.Text = "";
            comboBoxZamEncKey.SelectedIndex = 0;
            textBoxZamEncResult.Text = "";
        }

        //Очистить поля Дешифровка заменой
        private void buttonZamDecClean_Click(object sender, EventArgs e)
        {
            textBoxZamDecWord.Text = "";
            comboBoxZamDecKey.SelectedIndex = 0;
            textBoxZamDecResult.Text = "";
        }

        //Пример шифрование заменой
        private void buttonZamEx_Click(object sender, EventArgs e)
        {
            textBoxZamEncWord.Text = "ПРАВДА";
            comboBoxZamEncKey.SelectedIndex = 0;
            textBoxZamDecWord.Text = "ПОЯЭЫЯ";
            comboBoxZamDecKey.SelectedIndex = 0;
        }

        //Справка Шифрование методом Виженера
        private void buttonVizhQ_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Шифрование методом Виженера - это метод полиалфавитного шифрования буквенного текста с использованием ключевого слова."
            + "\nДля зашифровывания может использоваться таблица алфавитов, называемая tabula recta или квадрат (таблица) Виженера."
            + "\nПрименительно к латинскому алфавиту таблица Виженера составляется из строк по 26 символов, причём каждая следующая строка сдвигается на несколько позиций."
            + "\nЕсли буквы A-Z соответствуют числам 0-25, то шифрование Виженера можно в виде формулы: Ci = (Pi + Ki) mod 26"
            + "\nДешифровка: Pi = (Ci - Ki + 26) mod 26.");
        }

        //Перекинуть Шифрование методом Виженера <<< Дешифровка методом Виженера
        private void buttonVizhL_Click(object sender, EventArgs e)
        {
            textBoxVizhEncWord.Text = textBoxVizhDecResult.Text;
            textBoxVizhEncKey.Text = textBoxVizhDecKey.Text;
        }

        //Перекинуть Шифрование методом Виженера >>> Дешифровка методом Виженера
        private void buttonVizhR_Click(object sender, EventArgs e)
        {
            textBoxVizhDecKey.Text = textBoxVizhEncKey.Text;
            textBoxVizhDecWord.Text = textBoxVizhEncResult.Text;
        }

        //Пример Шифрование методом Виженера
        private void buttonVizhEx_Click(object sender, EventArgs e)
        {
            textBoxVizhEncWord.Text = "ПРИВЕТ";
            textBoxVizhEncKey.Text = "КЛЮЧ";
            textBoxVizhDecWord.Text = "ЪЬЖЩПЮ";
            textBoxVizhDecKey.Text = "КЛЮЧ";

            textBoxSimmEncWord.Text = "ОПЕРАЦИЯНАЧИНАЕТСЯВВОСКРЕСЕНЬЕ";
            textBoxSimmEncKey.Text = "ВОЛОГДА";
            textBoxSimmDecWord.Text = "СЯСАДЫЙВЭМЖМТБЗВЮОЁЖПФЪЭФХЙОЯФ";
            textBoxSimmDecKey.Text = "ВОЛОГДА";
        }

        //Очистить поля Шифрование методом Виженера
        private void buttonVizhEncClean_Click(object sender, EventArgs e)
        {
            textBoxVizhEncWord.Text = "";
            textBoxVizhEncKey.Text = "";
            textBoxVizhEncResult.Text = "";

            groupBoxVizhEnc.Visible = false;
        }

        //Очистить поля Дешифровка методом Виженера
        private void buttonVizhDecClean_Click(object sender, EventArgs e)
        {
            textBoxVizhDecWord.Text = "";
            textBoxVizhDecKey.Text = "";
            textBoxVizhDecResult.Text = "";
        }

        //Шифрование методом Виженера
        private void buttonVizhEncOK_Click(object sender, EventArgs e)
        {
            if (textBoxVizhEncWord.Text != "" && textBoxVizhEncKey.Text != "")
            {
                string sourceWord = textBoxVizhEncWord.Text.ToUpper();
                string key = textBoxVizhEncKey.Text.ToUpper();

                if (CheckLetter(sourceWord) == true && CheckLetter(key) == true)
                {
                    if (key.Length < sourceWord.Length)
                    {
                        int sk = sourceWord.Length - key.Length;

                        for (int i = 0; i < sk; i++)
                        {
                            key += key[i];
                        }
                    }

                    textBoxVizhEncWord.Text = textBoxVizhEncWord.Text.ToUpper();
                    textBoxVizhEncKey.Text = key.ToUpper();
                    int c = 0;
                    string enc = "";

                    for (int i = 0; i < sourceWord.Length; i++)
                    {
                        for (int j = 0; j < alphabet.Length; j++)
                        {
                            if (alphabet[j].Equals(sourceWord[i]))
                            {
                                for (int k = 0; k < alphabet.Length; k++)
                                {
                                    if (alphabet[k].Equals(key[i]))
                                    {
                                        c = (j + k) % alphabet.Length;
                                        enc += alphabet[c];
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    textBoxVizhEncResult.Text = enc;
                }

                else MessageBox.Show("Используются некорректные символы");
            }

            else MessageBox.Show("Введите данные");
        }

        //Дешифровка методом Виженера
        private void buttonVizhDecOK_Click(object sender, EventArgs e)
        {
            if (textBoxVizhDecWord.Text != "" && textBoxVizhDecKey.Text != "")
            {
                string enc = textBoxVizhDecWord.Text.ToUpper();
                string key = textBoxVizhDecKey.Text.ToUpper();

                if (CheckLetter(enc) == true && CheckLetter(key) == true)
                {
                    if (key.Length < enc.Length)
                    {
                        int sk = enc.Length - key.Length;

                        for (int i = 0; i < sk; i++)
                        {
                            key += key[i];
                        }
                    }

                    textBoxVizhDecWord.Text = textBoxVizhDecWord.Text.ToUpper();
                    textBoxVizhDecKey.Text = key.ToUpper();
                    int p = 0;
                    string dec = "";

                    for (int i = 0; i < enc.Length; i++)
                    {
                        for (int j = 0; j < alphabet.Length; j++)
                        {
                            if (alphabet[j].Equals(enc[i]))
                            {
                                for (int k = 0; k < alphabet.Length; k++)
                                {
                                    if (alphabet[k].Equals(key[i]))
                                    {
                                        p = (j - k + alphabet.Length) % alphabet.Length;
                                        dec += alphabet[p];
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    textBoxVizhDecResult.Text = dec;
                }

                else MessageBox.Show("Используются некорректные символы");
            }

            else MessageBox.Show("Введите данные");
        }

        //Очистить все поля
        private void buttonCleanAll_Click(object sender, EventArgs e)
        {
            buttonSimmEncClean_Click(sender, e);
            buttonSimmDecClean_Click(sender, e);

            buttonZamEncClean_Click(sender, e);
            buttonZamDecClean_Click(sender, e);
        }

        //Дабл клик для пасхалки
        private void labelVizhEncWord_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show("There are no Easter Eggs up here. Go away.");
            System.Diagnostics.Process.Start("iexplore.exe", "https://www.google.ru/search?q=кенгуру&tbm=isch");
        }
    }
}
