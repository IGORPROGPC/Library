using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Library1
{
    public partial class FMain : Form
    {
        public FMain()
        {
            InitializeComponent();
        ConnectionString = "Data Source=DESKTOP-2RG8IR8;" +
            "Initial Catalog=MyDB_Zyablov;Integrated Security=True;Connect Timeout=30;" +
            "Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        
            Conn(ConnectionString, Ganre, dataGridView1);
            Conn(ConnectionString, Book, dataGridView2);
            Conn2(ConnectionString, Ganre, cbGenreSearch, "Жанр", "ID");
            Conn2(ConnectionString, Author, cbAuthorSearch, "Автор", "ID");
            
        }

        string ConnectionString = "";
        string Ganre = "SELECT ID_Genre as ID, Name_G AS Жанр FROM Genre";
        string Author = "SELECT ID_Author AS ID, Name_A AS Автор FROM Author";
        string Book = "SELECT Book.ID_Book AS ID, Book.Name_B AS Название, Author.Name_A AS Автор, Genre.Name_G AS Жанр, Book.Count AS Количество," +
                " Book.Price AS Цена, Book.Publisher AS Издательство, Book.YearOfPub AS Год_издания FROM Author INNER JOIN" +
                " Book ON Author.ID_Author = Book.ID_Author INNER JOIN Genre ON Book.ID_Genre = Genre.ID_Genre";

        public void Conn(string CS, string cmdT, DataGridView dgv)
        {
            SqlDataAdapter Adapter = new SqlDataAdapter(cmdT, CS);
            DataSet ds = new DataSet();
            Adapter.Fill(ds, "Table");
            dgv.DataSource = ds.Tables["Table"].DefaultView;
        }

        public void Conn2(string CS, string cmdT, ComboBox CB, string field1, string field2)
        {
            SqlDataAdapter Adapter = new SqlDataAdapter(cmdT, CS);
            DataSet ds = new DataSet();
            Adapter.Fill(ds, "Table");
            CB.DataSource = ds.Tables["Table"];
            CB.DisplayMember = field1;
            CB.ValueMember = field2;
        }
        private void FMain_Load(object sender, EventArgs e)
        {
            lbDate.Text = "Сегодня " + DateTime.Now.ToShortDateString();
            lbTime.Text = "Время мск " + DateTime.Now.ToLongTimeString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbTime.Text = "Время мск " + DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        private void cbSpravoch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSpravoch.SelectedIndex==0)
            {
                Conn(ConnectionString, Ganre, dataGridView1);
            }
            if (cbSpravoch.SelectedIndex==1)
            {
                Conn(ConnectionString, Author, dataGridView1);
            }
        }

        private void cbGenreSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "";
            if (Convert.ToString(cbGenreSearch.SelectedValue)!="System.Data.DataRowView")
            {
                sql = Book + " WHERE Genre.ID_Genre=" + cbGenreSearch.SelectedValue;
            }
            else
            {
                sql = Book;
            }
            Conn(ConnectionString, sql, dataGridView2);
        }

        private void cbAuthorSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "";
            if (Convert.ToString(cbAuthorSearch.SelectedValue) != "System.Data.DataRowView")
            {
                sql = Book + " WHERE Author.ID_Author=" + cbAuthorSearch.SelectedValue;
            }
            else
            {
                sql = Book;
            }
            Conn(ConnectionString, sql, dataGridView2);
        }
    }
}
