using System;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;

namespace KetabKhaneh
{
    public partial class Form1 : Form
    {
        List<Book> book=new List<Book>();
        List<Place> place = new List<Place>();

        public Form1()
        {
            InitializeComponent();
            LoadBookList();
            LoadPlaceList();
            btnclose.FlatAppearance.MouseOverBackColor = Color.DarkSalmon;
           
        }
        private void LoadBookList()
        {
            book = SqliteDataAccess.LoadBook();
        }
        private void LoadPlaceList()
        {
            place = SqliteDataAccess.LoadPlace();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToString();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            Book B1 = new Book()
            {
                Title = txtname.Text,
                Bookmarker = txtBookmarker.Text,
                Year = txtYear.Text
            };
            int bookId = SqliteDataAccess.SaveBook(B1);
            Place P1 = new Place();
            P1.Column =Convert.ToInt32(txtColumn.Text);
            P1.Line = Convert.ToInt32(txtLine.Text);
            P1.BookId = bookId;
            Detail D1 = new Detail();
            D1.B1 = B1;
            D1.P1 = P1;
            SqliteDataAccess.SaveBook(B1);
            int lastBookId = SqliteDataAccess.GetLastBookId();
            SqliteDataAccess.savePlace(P1);
            SqliteDataAccess.SavePlace(P1, lastBookId);
            txtname.Text = "";
            txtBookmarker.Text = "";
            txtYear.Text = "";
            txtColumn.Text = "";
            txtLine.Text = "";
        }
        private void txtname_TextChanged(object sender, EventArgs e)
        {

        }

        

        private void txtBookmarker_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtYear_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtColumn_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtLine_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadBookList();
            LoadPlaceList();
            string keyword = txtnameSearch.Text.Trim();
            List<Detail> results = SqliteDataAccess.SearchBooks(keyword);
           
            if (results.Count == 0)
            {
                lbSearch.Items.Add("هیچ نتیجه‌ای یافت نشد.");
                return;
            }
            txtnameSearch.Clear();
            foreach (var item in results)
            {
               lbSearch.Items.Add(item.print());
            }
            txtnameSearch.Clear();
        }

        private void txtnameSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void lbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblDate_Click(object sender, EventArgs e)
        {

        }
    }
}


