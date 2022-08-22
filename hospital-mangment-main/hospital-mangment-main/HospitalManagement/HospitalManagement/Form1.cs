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
namespace HospitalManagement
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void signup_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection(@"Data Source=DESKTOP-A9E7P23\ADAGN;Initial Catalog=hospital;Integrated Security=True");
            cn.Open();
            if (textBox1.Text != string.Empty && textBox2.Text != string.Empty)
            {

                SqlCommand cmd = new SqlCommand("select * from EmployeUser where UserName ='" + textBox1.Text + "' and Password='" + textBox2.Text + "' and Spaciality ='" + comboBox1.SelectedItem + "'", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                   
                    string firstVariable1= string.Empty;
                    string firstVariable2 = string.Empty;
                    string yes = string.Empty;
                    string no = string.Empty;




                    dr.Close();

                    SqlCommand command = new SqlCommand("eXEC EmployeIdFinder @mobile = '" + textBox1.Text + "';", cn);

                    SqlDataReader reader = command.ExecuteReader();


                    while (reader.Read())
                    {
                        firstVariable1 = reader[0].ToString();
                    }
                    reader.Close();

                    SqlCommand comand = new SqlCommand("exec AprovalCheck @emID=@id; ;", cn);
                    comand.Parameters.AddWithValue("id", firstVariable1);
                    comand.ExecuteNonQuery();
                    SqlDataReader reder = comand.ExecuteReader();


                    while (reder.Read())
                    {
                        firstVariable2 = reder[0].ToString();
                    }
                    
                    yes="yes";
                    no = "no";
                    if (firstVariable2 == yes)
                    {
                        string x = string.Empty;
                        x = comboBox1.SelectedItem.ToString();

                        if (x == "Admin")
                        {
                            dr.Close();
                            Admin admin = new Admin();
                            admin.ShowDialog();
                        }
                        if (x == "Registeral")
                        {
                            dr.Close();
                            Admin admin = new Admin();
                            admin.ShowDialog();
                        }

                        if (x == "TriageNurse")
                        {
                            dr.Close();
                            Admin a = new Admin();
                            a.ShowDialog();
                        }
                        if (x == "Doctor")
                        {
                            dr.Close();
                            Admin b = new Admin();
                            b.ShowDialog();
                        }
                    }
                    else if (firstVariable2 == no)
                    {
                        MessageBox.Show("your accaunt is not approved or disabled contact the admin", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    else
                    {
                        MessageBox.Show("your accaunt is not yet approved please wait we sent the requist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    reader.Close();
                }
                else
                {
                    dr.Close();
                    MessageBox.Show("No Account avilable with this username and password ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Please enter value in all field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void login_Click(object sender, EventArgs e)
        {

        }

        private void signbutton_Click(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection(@"Data Source=DESKTOP-A9E7P23\ADAGN;Initial Catalog=hospital;Integrated Security=True");

            cn.Open();

            if (textBox3.Text != string.Empty && textBox4.Text != string.Empty && textBox5.Text != string.Empty && textBox6.Text != string.Empty && textBox7.Text != string.Empty && textBox8.Text != string.Empty && textBox9.Text != string.Empty && textBox10.Text != string.Empty && textBox11.Text != string.Empty)
            {
                if (textBox6.Text == textBox7.Text)
                {
                    SqlCommand cmd = new SqlCommand("select * from EmployeUser where UserName='" + textBox3.Text + "'", cn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        dr.Close();
                        MessageBox.Show("Username Already exist please try another ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        dr.Close();
                        cmd = new SqlCommand("EXEC RigisterEmploye @FirstName = @fname," +
                            "@LastName = @lname,@Address = @adr, @City = @cty," +
                            " @salary = @slry, @mobile = @mbl, @UserName = @uname," +
                            "@Password = @pass, @Spaciality = @privilage; ", cn);
                        cmd.Parameters.AddWithValue("fname", textBox3.Text);
                        cmd.Parameters.AddWithValue("lname", textBox4.Text);
                        cmd.Parameters.AddWithValue("adr", textBox11.Text);
                        cmd.Parameters.AddWithValue("cty", textBox8.Text);
                        cmd.Parameters.AddWithValue("slry", textBox9.Text);
                        cmd.Parameters.AddWithValue("mbl", textBox10.Text);
                        cmd.Parameters.AddWithValue("uname", textBox5.Text);
                        cmd.Parameters.AddWithValue("pass", textBox6.Text);
                        cmd.Parameters.AddWithValue("privilage", comboBox2.SelectedItem);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("we will send your request to admin as soon as he approves you will can login.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter both password same ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter value in all field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
       

    


