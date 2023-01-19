using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Proiect
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        SqlConnection myCon = new SqlConnection();
        private SqlCommandBuilder cursBuilder;

        

        [WebMethod]
        public void AddPersonalData(string cnp, string name, string surname, string age,  string phoneNo)
        {
            myCon.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\source\repos\Proiect\Proiect\App_Data\Database1.mdf;Integrated Security=True";

            myCon.Open();

            //SqlDataAdapter = contine un set de comenzi de date si conexiunea la db si este folosit pentru a interschimba date intre datatset si db

            SqlDataAdapter cursAdapter = new SqlDataAdapter("SELECT * FROM PersonalData ORDER BY cnp", myCon);

            //SqlCommandBuilder poate genera instructiuni CRUD folosind proprietatea SelectCommand

            SqlCommandBuilder cursBuilder = new SqlCommandBuilder(cursAdapter);

            //DataSet - manipularea datelor
            DataSet newQueryPersonalData = new DataSet();

            // dataAdapterul -proprietatea Fill, face o copie a datelor din  db in dataSet
            cursAdapter.Fill(newQueryPersonalData, "PersonalData");

            //se creaza un rand nou de tipul PersonalData
            DataRow newRow = newQueryPersonalData.Tables["PersonalData"].NewRow();

            //populam noul rand creat
            newRow["cnp"] = cnp;
            newRow["name"] = name;
            newRow["surname"] = surname;
            newRow["age"] = age;
            newRow["phoneNo"] =phoneNo;

            //Facem adaugarea in dataSet    
            newQueryPersonalData.Tables["PersonalData"].Rows.Add(newRow);

            //facem update cu noul dataSet in db
            cursAdapter.Update(newQueryPersonalData, "PersonalData");
            myCon.Close();
        }

        [WebMethod]
        public void AddAppointment(int id_a, string cnp, string date, string hour,string instructor)
        {   myCon.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\source\repos\Proiect\Proiect\App_Data\Database1.mdf;Integrated Security=True";

            myCon.Open();
            SqlDataAdapter cursAdapter = new SqlDataAdapter("SELECT * FROM Appointments ORDER BY Id", myCon);
           
            SqlCommandBuilder cursBuilder = new SqlCommandBuilder(cursAdapter);

            //vom crea un nou dataSet
            DataSet newQueryAppointment = new DataSet();

            //folosind dataAdapterul si proprietatea Fill, vom face o copie a datelor din db in dataSet
            cursAdapter.Fill(newQueryAppointment, "Appointments");

            //se creaza un rand nou de tipul Appointments
            DataRow newRow = newQueryAppointment.Tables["Appointments"].NewRow();

            //populam noul rand creat
            newRow["id"] = id_a;
            newRow["cnp"] = cnp;
            newRow["date"] = date;
            newRow["hour"] = hour;
            newRow["instructor"] = instructor;

            //Facem adaugarea in dataSet
            newQueryAppointment.Tables["Appointments"].Rows.Add(newRow);

            //facem update cu noul dataSet in db
            cursAdapter.Update(newQueryAppointment, "Appointments");
            myCon.Close();
        }


        [WebMethod]
        public void AddReviews(int id_r, int id_a, char stars, string description)
        {   myCon.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\source\repos\Proiect\Proiect\App_Data\Database1.mdf;Integrated Security=True";

            myCon.Open();
            SqlDataAdapter cursAdapter = new SqlDataAdapter("SELECT * FROM Reviews ORDER BY Id_r", myCon);
           
            SqlCommandBuilder cursBuilder = new SqlCommandBuilder(cursAdapter);
            
            //folosim un nou dataset pentru a face adaugarea
             DataSet newQueryReviews = new DataSet();

            //facem o copie a datelor din db in dataSet
            cursAdapter.Fill(newQueryReviews, "Reviews");

            //se creaza un rand nou de tipul Recenzii
            DataRow newRow = newQueryReviews.Tables["Reviews"].NewRow();

            //populam noul rand creat
            newRow["id_r"] = id_r;
            newRow["id"] = id_a;
            newRow["stars"] = stars;
            newRow["description"] = description;

            //Facem adaugarea in dataSet
            newQueryReviews.Tables["Reviews"].Rows.Add(newRow);

            //facem update cu noul dataSet in db
            cursAdapter.Update(newQueryReviews, "Reviews");
            myCon.Close();
        }

        [WebMethod]
        public void DeletePersonalData(string cnp)
        {  myCon.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\source\repos\Proiect\Proiect\App_Data\Database1.mdf;Integrated Security=True";
            myCon.Open();

            SqlDataAdapter cursAdapter = new SqlDataAdapter("SELECT * FROM PersonalData ORDER BY cnp", myCon); 

            //facem querry la db           
            SqlCommandBuilder cursBuilder = new SqlCommandBuilder(cursAdapter);

            DataSet deletePersonalData = new DataSet(); //folosim dataSetul pentru manipularea datelor

            cursAdapter.Fill(deletePersonalData, "PersonalData"); //in dataSet facem o copie a tabelei

             DataColumn[] pk = new DataColumn[1]; //definim un array de ob DataColumn cu 1 elemenet
             pk[0] = deletePersonalData.Tables["PersonalData"].Columns["cnp"]; //pe primul elem din array punem coloana cnp care este de tipul DataColumn

            deletePersonalData.Tables["PersonalData"].PrimaryKey = pk; //setam pe dataSet ca si cheie primara cnp pe care l-am definit anterior

            //findam Row-ul corespunzator cnp-ului primit pe metoda
            DataRow find = null;
            while (find == null)
            { find = deletePersonalData.Tables["PersonalData"].Rows.Find(cnp); //caut dupa cnp
            }
            find.Delete(); //sterge Row gasit din dataSet
            cursAdapter.Update(deletePersonalData, "PersonalData"); //updateaza tabelul din db cu dataSetul
             myCon.Close();
        }

        [WebMethod]
        public void DeleteAppointment(string cnp)
        {   myCon.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\source\repos\Proiect\Proiect\App_Data\Database1.mdf;Integrated Security=True";

            myCon.Open();
            SqlDataAdapter cursAdapter = new SqlDataAdapter("SELECT * FROM Appointments ORDER BY cnp", myCon);
           
            SqlCommandBuilder cursBuilder = new SqlCommandBuilder(cursAdapter);

            DataSet deleteAppointment = new DataSet();

            cursAdapter.Fill(deleteAppointment, "Appointments"); //in dataSet facem o copie a  tabelei

             DataColumn[] pk = new DataColumn[1];//definim un array de ob DataColumn cu 1 elemenet
             pk[0] = deleteAppointment.Tables["Appointments"].Columns["cnp"];//pe primul elem din array punem coloana id care este de tipul DataColumn

            deleteAppointment.Tables["Appointments"].PrimaryKey = pk;//setam pe dataSet ca si cheie primara id pe care l-am definit anterior

            //findam Row-ul corespunzator cnp-ului 
            DataRow find = null;
            while (find == null)
            { find = deleteAppointment.Tables["Appointments"].Rows.Find(cnp); //caut dupa cnp
            }
            find.Delete(); //sterge randul gasit in dataSet
            cursAdapter.Update(deleteAppointment, "Appointments");//updateaza tabela din db cu dataSetul
        myCon.Close();
        }

        [WebMethod]
        public void ChangeAppointment(int id_a, string date, string hour, string instructor)
        {  myCon.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\source\repos\Proiect\Proiect\App_Data\Database1.mdf;Integrated Security=True";
            myCon.Open();

            SqlDataAdapter cursAdapter = new SqlDataAdapter("SELECT * FROM Appointments ORDER BY Id", myCon);
           
            SqlCommandBuilder cursBuilder = new SqlCommandBuilder(cursAdapter);

            DataSet updateAppointment = new DataSet();

            cursAdapter.Fill(updateAppointment, "Appointments"); //in dataSet facem o copie a tabelei

             DataColumn[] pk = new DataColumn[1]; //definim un array de ob DataColumn cu 1 element
             pk[0] = updateAppointment.Tables["Appointments"].Columns["Id"]; //pe primul elem din array punem coloana Id
            updateAppointment.Tables["Appointments"].PrimaryKey = pk; //setam pe dataSet ca si cheie primara id pe care l-am definit anterior

            //findam Row-ul corespunzator primit pe metoda
            DataRow find = null;
            while (find == null)
            {   find = updateAppointment.Tables["Appointments"].Rows.Find(id_a); //caut dupa id_a
            }
            //populam randul creat
            find["date"] = date;
            find["hour"] = hour;
            find["instructor"] = instructor;

            //facem update cu noul dataSet in db
            cursAdapter.Update(updateAppointment, "Appointments");
            myCon.Close();
        }

        [WebMethod]
        public void ChangePersonalData(string cnp, string name, string surname, string age, string phoneNo)
        {  myCon.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\source\repos\Proiect\Proiect\App_Data\Database1.mdf;Integrated Security=True";
            myCon.Open();

            SqlDataAdapter cursAdapter = new SqlDataAdapter("SELECT * FROM PersonalData ORDER BY cnp", myCon);
           
            SqlCommandBuilder cursBuilder = new SqlCommandBuilder(cursAdapter);

            DataSet updatePersonalData = new DataSet();

            cursAdapter.Fill(updatePersonalData, "PersonalData"); //in dataSet facem o copie a tabelei
             DataColumn[] pk = new DataColumn[1];//definim un array de ob DataColumn cu 1 element
             pk[0] = updatePersonalData.Tables["PersonalData"].Columns["cnp"];//pe primul elemdin array punem coloana cnp

            updatePersonalData.Tables["PersonalData"].PrimaryKey = pk;////setam pe dataSet ca si cheie primara id pe care l-am definit anterior

             //findam Row-ul corespunzator primit pe metoda
             DataRow find = null;
            while (find == null)
            {  find = updatePersonalData.Tables["PersonalData"].Rows.Find(cnp);//caut dupa cnp
            }

            //populam randul creat

            find["name"] = name;
            find["surname"] = surname;
            find["age"] = age;
            find["phoneNo"] = phoneNo;

            //facem update cu noul dataSet in db
            cursAdapter.Update(updatePersonalData, "PersonalData");
            myCon.Close();
        }


        [WebMethod]
        public void DeleteReview(int id_r)
        {
            myCon.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\source\repos\Proiect\Proiect\App_Data\Database1.mdf;Integrated Security=True";

            myCon.Open();
            SqlDataAdapter cursAdapter = new SqlDataAdapter("SELECT * FROM Reviews ORDER BY Id_r", myCon);

            SqlCommandBuilder cursBuilder = new SqlCommandBuilder(cursAdapter);

            DataSet deleteReview = new DataSet();

            cursAdapter.Fill(deleteReview, "Reviews"); //in dataSet facem o copie a  tabelei

            DataColumn[] pk = new DataColumn[1];//definim un array de ob DataColumn cu 1 elemenet
            pk[0] = deleteReview.Tables["Reviews"].Columns["id_r"];//pe primul elem din array punem coloana id care este de tipul DataColumn

            deleteReview.Tables["Reviews"].PrimaryKey = pk;//setam pe dataSet ca si cheie primara id pe care l-am definit anterior

            //cautam Row-ul corespunzator id_r-ului primit pe metoda
            DataRow find = null;
            while (find == null)
            {
                find = deleteReview.Tables["Reviews"].Rows.Find(id_r); //caut dupa id_r
            }
            find.Delete(); //sterge randul gasit in dataSet
            cursAdapter.Update(deleteReview, "Reviews");//updateaza tabela din db cu dataSetul
            myCon.Close();
        }

        [WebMethod]
        public void ChangeReview(int id_r, int id_a, char stars, string description)
        {
            myCon.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\source\repos\Proiect\Proiect\App_Data\Database1.mdf;Integrated Security=True";
            myCon.Open();

            SqlDataAdapter cursAdapter = new SqlDataAdapter("SELECT * FROM Reviews ORDER BY Id_r", myCon);

            SqlCommandBuilder cursBuilder = new SqlCommandBuilder(cursAdapter);

            DataSet updateReview = new DataSet();

            cursAdapter.Fill(updateReview, "Reviews"); //in dataSet facem o copie a tabelei

            DataColumn[] pk = new DataColumn[1]; //definim un array de ob DataColumn cu 1 element
            pk[0] = updateReview.Tables["Reviews"].Columns["Id_r"]; //pe primul elem din array punem coloana Id
            updateReview.Tables["Reviews"].PrimaryKey = pk; //setam pe dataSet ca si cheie primara id pe care l-am definit anterior

            //findam Row-ul corespunzator primit pe metoda
            DataRow find = null;
            while (find == null)
            {
                find = updateReview.Tables["Reviews"].Rows.Find(id_r); //caut dupa id_r
            }
            //populam randul creat
            find["id_r"] = id_r;
            find["id"] = id_a;
            find["stars"] = stars;
            find["description"] = description;

            //facem update cu noul dataSet in db
            cursAdapter.Update(updateReview, "Reviews");
            myCon.Close();
        }

    }
}




