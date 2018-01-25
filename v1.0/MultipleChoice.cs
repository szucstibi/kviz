using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MultipleChoice
{
    public partial class MultipleChoice : Form
    {
        
        private Students student;
       
        private string[,] resultsAndMemo;
        private string[] question1;
        private string[] question2;
        private string[] question3;
        private string[] question4;
        private int number = 0;
        private int totalNumber = 0;
        private string studentMark = "";
        private int studentNo = 0;
        private int numberOpen = 0;
        private string line = "";
        private int num = 0;
        private int count = 0;
        private bool check =false;
        private string username = "", password = "", firstname = "", surname = "", dateOf = "", email = "";
        private StreamWriter write;
        private StreamReader sr;
        private StreamReader read;

        public MultipleChoice()
        {
            InitializeComponent();
            
            setUserID();
          
            timerUpdateTime.Start();
            
            lblTime.Visible = true;


        }
        
        private void btnSignIn_Click(object sender, EventArgs e)
        {
            panelLogin.Visible = false;
        }
        
        private void btnStudentCenter_Click(object sender, EventArgs e)
        {
            panelStudentCenter.Visible = false;
            lblRegister.Visible = true;
            btnRegsiter.Visible = true;

        }
        
        private void btnSignOut_Click(object sender, EventArgs e)
        {
            loginPanel.Visible = true;
        }
        
        private void lblSignIn_Click(object sender, EventArgs e)
        {
           
            signIn();
        }
        
        private void btnSignIn_Click_1(object sender, EventArgs e)
        {
            
            signIn();
        }

        
        private void btnSignOut_Click_1(object sender, EventArgs e)
        {


            txtUserId.Clear();
            txtName.Clear();
            txtSurname.Clear();

            txtUserId.Text = student.StudentID().ToString();
            panelLogin.Visible = true;
            lblNameSign.Visible = false;
            btnSignOut.Visible = false;
            btnPastPaper.Visible = false;
            lblSignedIn.Visible = false;
            
            txtUserAnswer1.Visible = false;
            txtUserAnswer2.Visible = false;
            txtUserAnswer3.Visible = false;
            txtUserAnswer4.Visible = false;
            
            lblQuestion1.Text = "";
            lblQuestion2.Text = "";
            lblQuestion3.Text = "";
            lblQuestion4.Text = "";
            lblQuestion1Possible.Text = "";
            lblQuestion2Possible.Text = "";
            lblQuestion3Possible.Text = "";
            lblQuestion4Possible.Text = "";
            txtUserAnswer1.Clear();
            txtUserAnswer2.Clear();
            txtUserAnswer3.Clear();
            txtUserAnswer4.Clear();
            numberOpen = 0;
            panelLecture.Visible = true;


        }

        
        public void setUserID()
        {

            student = new Students();
            txtUserId.Text = student.StudentID().ToString();
        }

        
        private void timerUpdateTime_Tick(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
            lblTime.Text = time.ToLocalTime().ToString();

        }
        
        private void btnHideLecture_Click(object sender, EventArgs e)
        {
            
            if (numberOpen == 1)
            {
                panelLecture.Visible = false;
                btnPastPaper.Visible = true;
            }
               
            else
            {
                MessageBox.Show("Kérjük kattintson a Tanulóra, jelentkezzen be, majd térjen vissza ide!", "Bejelentkezés szükséges!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //creating action for onlick
        private void btnSetTest_Click(object sender, EventArgs e)
        {
            //calling my possible anser method
            possibleAnswersAndQuestion();

        }
        //populating my arrays
        public void possibleAnswersAndQuestion()
        {
            question1 = new string[5]
               
                {
                txtQuestion1.Text,
                txtQuestion1Possible1.Text,
                txtQuestion1Possible2.Text,
                txtQuestion1Possible3.Text,
                txtQuestion1Possible4.Text
                };


            //adding text into array for question 1
            question1 = new string[5]
               
                {
                txtQuestion1.Text,
                txtQuestion1Possible1.Text,
                txtQuestion1Possible2.Text,
                txtQuestion1Possible3.Text,
                txtQuestion1Possible4.Text
                };

            //adding text into array for question 2
            question2 = new string[5]
                {
                txtQuestion2.Text,
                txtQuestion2Possible1.Text,
                txtQuestion2Possible2.Text,
                txtQuestion2Possible3.Text,
                txtQuestion2Possible4.Text
                };

            //adding text into array for question 3
            question3 = new string[5]
                {
                txtQuestion3.Text,
                txtQuestion3Possible1.Text,
                txtQuestion3Possible2.Text,
                txtQuestion3Possible3.Text,
                txtQuestion3Possible4.Text
                };
            //adding text into array for question 4
            question4 = new string[5]
                {
                txtQuestion4.Text,
                txtQuestion4Possible1.Text,
                txtQuestion4Possible2.Text,
                txtQuestion4Possible3.Text,
                txtQuestion4Possible4.Text
                };

            //Setting questions and possible answers
            lblQuestion1.Text = question1[0];
            lblQuestion1Possible.Text = "";
            //populationg Array
            for (int x = 1; x < question1.Length; x++)
            {
                lblQuestion1Possible.Text += question1[x] + "\n";
            }

            //Setting questions and possible answers
            lblQuestion2.Text = question2[0];
            lblQuestion2Possible.Text = "";
            //populationg Array
            for (int x = 1; x < question2.Length; x++)
            {
                lblQuestion2Possible.Text += question2[x] + "\n";
            }

            //Setting questions and possible answers
            lblQuestion3.Text = question3[0];
            lblQuestion3Possible.Text = "";
            //populationg Array 
            for (int x = 1; x < question3.Length; x++)
            {
                lblQuestion3Possible.Text += question3[x] + "\n";
            }

            //Setting questions and possible answers
            lblQuestion4.Text = question4[0];
            lblQuestion4Possible.Text = "";
            //populationg Array
            for (int x = 1; x < question4.Length; x++)
            {
                lblQuestion4Possible.Text += question4[x] + "\n";
            }

            //calling my method here.
            checkQuestions();
        }

        //enabling user input textboxes.
        public void checkQuestions()
        {
            if (txtQuestion1.Text != "" & txtQuestion2.Text != "" & txtQuestion3.Text != "" & txtQuestion4.Text != "")
            {
                //Enabling user's text box
                txtUserAnswer1.Visible = true;
                txtUserAnswer2.Visible = true;
                txtUserAnswer3.Visible = true;
                txtUserAnswer4.Visible = true;
                check = true;

                try
                {
                    
                    string pastPaper = "";

                    pastPaper = "\tPast Paper" + "\n\n1. " + txtQuestion1.Text
                        + "\n  " + txtQuestion1Possible1.Text
                        + "\n  " + txtQuestion1Possible2.Text
                        + "\n  " + txtQuestion1Possible3.Text
                        + "\n  " + txtQuestion1Possible4.Text
                        + "\n\n  " + txtQuestion1Correct1.Text + " ✔"
                        + "\n\n2. " + txtQuestion2.Text
                        + "\n  " + txtQuestion2Possible1.Text
                        + "\n  " + txtQuestion2Possible2.Text
                        + "\n  " + txtQuestion2Possible3.Text
                        + "\n  " + txtQuestion2Possible4.Text
                        + "\n\n  " + txtQuestion2Correct2.Text + " ✔"
                        + "\n\n3. " + txtQuestion3.Text
                        + "\n  " + txtQuestion3Possible1.Text
                        + "\n  " + txtQuestion3Possible2.Text
                        + "\n  " + txtQuestion3Possible3.Text
                        + "\n  " + txtQuestion3Possible4.Text
                        + "\n\n  " + txtQuestion3Correct3.Text + " ✔"
                        + "\n\n4. " + txtQuestion4.Text
                        + "\n  " + txtQuestion4Possible1.Text
                        + "\n  " + txtQuestion4Possible2.Text
                        + "\n  " + txtQuestion4Possible3.Text
                        + "\n  " + txtQuestion4Possible4.Text
                        + "\n\n  " + txtQuestion4Correct4.Text + " ✔";

                    

                    write = new StreamWriter("Past Paper.txt");
                    write.WriteLine(pastPaper);
                    write.Close();
                }
                    
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                MessageBox.Show("A teszt sikeresen beállítva és mentve.", "A beállítás elkészült", MessageBoxButtons.OK, MessageBoxIcon.Information);
                panelLecture.Visible = true;
                btnPastPaper.Visible = false;
            }
            else
            {
                MessageBox.Show("Kérjük töltse ki az összes mezőt", "Hopppá", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        
        private void btnMemo_Click(object sender, EventArgs e)
        {
            try
            {

                
                read = new StreamReader("Test Results.txt");
                MessageBox.Show(read.ReadToEnd());
                read.Close();

            } 
            catch (System.IO.PathTooLongException path)
            {
                MessageBox.Show(path.Message);

            }
            catch (IOException io)
            {
                MessageBox.Show(io.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnTakeTest_Click(object sender, EventArgs e)
        {
            
            if (check == true)
            {
                try
                {

                    studentNo++;

                    
                    number = 0;
                    totalNumber = 0;

                    
                    resultsAndMemo = new string[2, 4]
            {
               
            {
                
             txtQuestion1Correct1.Text, 
             txtQuestion2Correct2.Text,
             txtQuestion3Correct3.Text, 
             txtQuestion4Correct4.Text,
            
            },

            {
                
                txtUserAnswer1.Text,
                txtUserAnswer2.Text,
                txtUserAnswer3.Text,
                txtUserAnswer4.Text
            }
            };

                    

                    for (int x = 0; x < 1; x++)
                    {

                        for (int y = 0; y <= 3; y++)
                        {
                            number++;

                            if (resultsAndMemo[0, y].ToLower() == resultsAndMemo[1, y].ToLower())
                            {

                                studentMark += number.ToString() + ". ✔\n";
                                totalNumber++;
                            }
                            else
                            {
                                studentMark += number.ToString() + ". ✖\n";
                            }
                        }
                    }


                    if (studentMark != "")
                    {

                        
                        string outcomes = "";
                        outcomes = "Keresztnév: " + firstname + " \nVezetéknév: " + surname + "\n\nVálaszai \t\tMemo\n"
                               + "1. " + txtUserAnswer1.Text.ToUpper() + "\t\t\t1. " + txtQuestion1Correct1.Text.ToUpper() + "\n"
                               + "2. " + txtUserAnswer2.Text.ToUpper() + "\t\t\t2. " + txtQuestion2Correct2.Text.ToUpper() + "\n"
                               + "3. " + txtUserAnswer3.Text.ToUpper() + "\t\t\t3. " + txtQuestion3Correct3.Text.ToUpper() + "\n"
                               + "4. " + txtUserAnswer4.Text.ToUpper() + "\t\t\t4. " + txtQuestion4Correct4.Text.ToUpper() + "\n\n"
                               + "Jel\n" + studentMark +
                               "\n\n" + "_____________________________________________________________________\n" +
                            "\n Összesen: " + totalNumber + "/4\n"
                            + "______________________________________________________________________";

                        string clasList = "ID: " + student.getStudentID() + "\nKeresztnév: " + firstname + "\nVezetéknév: " + surname + "\nJel: " + totalNumber + "/4\n_________________________________________________________________\n";
                        MessageBox.Show("A teszt kész, jelölés kész, kérjük ellenőrizze az eredményeket és a memo-t. A teszt és a memo sikeresen mentve.", "A teszt kész", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        
                        write = new StreamWriter("Class List.txt", true);
                        write.Write(clasList);
                        write.Close();

                        
                        write = new StreamWriter("Test Results.txt");
                        write.Write(outcomes);
                        write.Close();
                        studentMark = null;

                    }
                    else
                    {

                        MessageBox.Show("Kérjük előbb válaszoljon a kérdésre!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                
                catch (System.IO.PathTooLongException path)
                {
                    MessageBox.Show(path.Message);

                }
                catch (IOException io)
                {
                    MessageBox.Show(io.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else 
            {
                MessageBox.Show("Kérjük várjon ","Kérjük várjon.....",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void lblSignedIn_Click(object sender, EventArgs e)
        {

        }

        private void btnViewClass_Click(object sender, EventArgs e)
        {
            try
            {
                read = new StreamReader("Class List.txt");
                MessageBox.Show("\t\t__Osztály lista__\n\n" + read.ReadToEnd());
                read.Close();
            }//catching all exceptions
            catch (System.IO.PathTooLongException path)
            {
                MessageBox.Show(path.Message);

            }
            catch (IOException io)
            {
                MessageBox.Show(io.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lblRegister_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.ShowDialog();
        }

        private void btnRegsiter_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.ShowDialog();
        }
        public void signIn()
        {
            StreamReader read;
            string details = "";


            
            try
            {

                student = new Students();
                student.setStudenID(txtUserId.Text);
                student.setFirstName(txtName.Text.ToLower());
                student.setLastName(txtSurname.Text);


                //reading name from file
                read = new StreamReader("studentDetails.txt");

                while ((line = read.ReadLine()) != null)
                {
                    if (line.Contains(student.getFirstName()) && line.Contains(student.getLastName()))
                    {
                        num = 1;
                        details = line;

                    }
                }
                
                string[] words = details.Split(',');
                foreach (string word in words)
                {
                    if (count == 0)
                    {
                        username = word;
                    }
                    else if (count == 1)
                    {
                        password = word;
                    }
                    else if (count == 2)
                    {
                        firstname = word;
                    }
                    else if (count == 3)
                    {
                        surname = word;
                    }
                    else if (count == 4)
                    {
                        dateOf = word;
                    }
                    else if (count == 5)
                    {
                        email = word;
                        count = -1;
                        break;
                    }
                    count++;
                }
                
                if (num == 1)
                {

                    count = 0;
                    num = 0;
                    if (username == student.getFirstName() && password == student.getLastName())
                    {
                        
                        panelLogin.Visible = false;
                        lblNameSign.Visible = true;
                        lblNameSign.Text = email.ToLower();
                        lblSignedIn.Visible = true;
                        btnSignOut.Visible = true;

                        numberOpen = 1;
                    }
                    else
                    {
                        MessageBox.Show("Helytelen felhasználónév/jelszó", "A belépés sikerltelen", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                }
                else
                {
                    MessageBox.Show("Helytelen felhasználónév/jelszó", "A belépés sikerltelen", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                read.Close();


            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("A fájl nem található, a belépés előtt kérjük regisztráljon", "A fájl nem található", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnPastPaper_Click(object sender, EventArgs e)
        {
            try
            {
                
                read = new StreamReader("Past Paper.txt");
                MessageBox.Show(read.ReadToEnd());
                read.Close();

            } 
            catch (System.IO.PathTooLongException path)
            {
                MessageBox.Show(path.Message);

            }
            catch (IOException io)
            {
                MessageBox.Show(io.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
       
        
    }
}
