using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using MySql.Data.MySqlClient;


namespace Kurs
{
    class MainClass
    {
        public static void AwaitInput()
        {
            Console.WriteLine("Нажмите на любую клавишу...\n");
            Console.ReadKey();
            Console.Clear();
        }

        public static void DeleteString()
        {
            string connectionString =
                "Server=127.0.0.1;" +
                "Database=kurs;" +
                "User ID=admin;" +
                "Password=455685;" +
                "Pooling=false";
            IDbConnection con;
            con = new MySqlConnection(connectionString);
            while (true)
            {

                Console.WriteLine("\nЧто вы хотитие сделать?: \n0.Выход\n1.Удалить запись");
                string delAnswer = Console.ReadLine();
                int delAnsw = 0;
                bool delAnswExc = false;
                bool answExc = false;
                string _tabname;
                string _number;
                IDbCommand cmd0 = con.CreateCommand();
                try
                {
                    delAnsw = Int32.Parse(delAnswer);
                } catch(FormatException delExc)
                {
                    Console.WriteLine(delExc.Message);
                    delAnswExc = true;
                }
                if (!delAnswExc)
                {
                    if (delAnsw == 1)
                    {
                        con.Open();
                        cmd0.CommandText = "use kurs;";
                        cmd0.ExecuteNonQuery();
                        con.Close();
                        con.Open();
                        cmd0.CommandText = "show tables;";
                        IDataReader read = cmd0.ExecuteReader();
                        Console.WriteLine("\nСписок таблиц: ");
                        int red = 0;
                        while (read.Read())
                        {
                            Console.WriteLine(" " + red + ". " + read.GetString(0));
                            red++;
                        }
                        read.Close();
                        con.Close();
                        bool dlExc = false;
                        while (true)
                        {
                            Console.WriteLine("\nВведите название таблицы: ");
                            _tabname = Console.ReadLine();
                            Console.WriteLine($"\nВведено значение -- {_tabname} \nОно верно?(1.Да 2.Нет");
                            string _dlAns = Console.ReadLine();
                            int dlAns = 0;

                            try
                            {
                                dlAns = Int32.Parse(_dlAns);
                            } catch(FormatException dlFExc)
                            {
                                Console.WriteLine(dlFExc.Message);
                                dlExc = true;
                                Console.WriteLine("\nОшибка ввода");
                            }
                            if (!dlExc)
                            {
                                if (dlAns == 1)
                                {
                                    break;
                                } else if (dlAns == 2)
                                {
                                    Console.WriteLine("\nВведите новое название: ");
                                    _tabname = Console.ReadLine();
                                }
                                else
                                {
                                    Console.WriteLine("\nОшибка ввода: неверное значение");
                                }
                            }
                        }
                        if (!dlExc)
                        {
                            Console.WriteLine("\nВведите номер договора: ");
                            _number = Console.ReadLine();
                            while (true)
                            {
                                Console.WriteLine($"\nВведено значение: {_number}");
                                Console.WriteLine("\nОно правильное(1.Да 2.Нет)");
                                string _answer = Console.ReadLine();
                                int answer = 0;

                                try
                                {
                                    answer = Int32.Parse(_answer);
                                } catch(FormatException answFexc)
                                {
                                    Console.WriteLine(answFexc.Message);
                                    answExc = true;
                                }
                                if (!answExc)
                                {
                                    if (answer == 1)
                                    {
                                        break;
                                    } else if (answer == 2)
                                    {
                                        Console.WriteLine("\nВведите новое значение: ");
                                        _number = Console.ReadLine();
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nОшибка ввода: неверное значение");
                                    }
                                }
                            }

                            if (!answExc)
                            {
                                IDbCommand cmd01 = con.CreateCommand();
                                cmd01.CommandText = $"delete from {_tabname} where Number = '{_number}'";
                                con.Open();
                                bool sqlExc = false;
                                try
                                {
                                    cmd01.ExecuteScalar();
                                } catch(MySqlException delMexc)
                                {
                                    Console.WriteLine(delMexc.Message);
                                    sqlExc = true;
                                }
                                con.Close();
                                if (!sqlExc)
                                {
                                    Console.WriteLine("\nЗапись успешно удалена");
                                }
                            }
                        }
                        break;
                    } else if (delAnsw == 0)
                    {
                        break;
                    }
                    else
                    {

                    }
                }
            }        
        }

        public static void ShowTables()
        {
            string connectionString =
                "Server=127.0.0.1;" +
                "Database=kurs;" +
                "User ID=admin;" +
                "Password=455685;" +
                "Pooling=false";
            IDbConnection dbcon;
            dbcon = new MySqlConnection(connectionString);
            dbcon.Open();
            IDbCommand cmd3 = dbcon.CreateCommand();
            cmd3.CommandText = "show tables in kurs;";
            IDataReader rr = cmd3.ExecuteReader();

            int ii = 0;
            while (rr.Read())
            {
                Console.WriteLine(" " + ii + ". " + rr.GetString(0));
                ii++;
            }
            rr.Close();
            dbcon.Close();
        }

        public static string CreateQuery(int selector, string _tablename, string _value, string _id)
        {
            bool answerExc = false;
            bool selExc = false;
            string value = "";
            string tablename = "";
            string what = "";
            int id = 0;

         //  string query = $"update {tablename} set {what} = '{value} where id = '{id}''";


            while (true)
            {
                Console.WriteLine("\nВведно название таблицы: " + _tablename + "\nОно верно?(1.Да 2.Нет)");
                string _answer = Console.ReadLine();
                int answer = 0;
                bool answerEx = false;
                try
                {
                    answer = Int32.Parse(_answer);
                } catch(FormatException formExc)
                {
                    Console.WriteLine(formExc.Message);
                    answerEx = true;
                    Console.WriteLine("\nОшибка ввода");
                }
                if (!answerEx)
                {
                    if (answer == 1)
                    {
                        tablename = _tablename;
                        break;
                    }
                    else if (answer == 2)
                    {
                        Console.WriteLine("\nВведите новое название: ");
                        _tablename = Console.ReadLine();
                    }
                    else { Console.WriteLine("\nОшибка ввода"); }
                }
            }

           // int id = 0;
            while (true)
            {
                Console.WriteLine("\nВведён номер строки: "+ _id+ "\nЭто правильный номер строки?(1.Да 2.Нет): ");
                string _idAnsw = Console.ReadLine();
                int idAnsw = 0;
                bool idExc = false;
                try
                {
                    idAnsw = Int32.Parse(_idAnsw);
                } catch(FormatException idFormExc)
                {
                    Console.WriteLine(idFormExc.Message);
                    idExc = true;
                    Console.WriteLine("\nОшибка ввода\n");
                }
                if (!idExc)
                {
                    if(idAnsw == 1)
                    {
                        try
                        {
                            id = Int32.Parse(_id);
                        } catch(FormatException _idExc)
                        {
                            Console.WriteLine(_idExc.Message);
                        }
                        break;
                    } else if(idAnsw == 2)
                    {
                        Console.WriteLine("\nВведите новое значение: ");
                        _id = Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("\nНеверное значение");
                        continue;
                    }
                }
            }

           // string what = "";
            if (!selExc)
            {
                switch (selector)
                {
                    case 1: //Init
                        what = "Init";
                        break;

                    case 2: //Number
                        what = "Number";
                        break;

                    case 3: //Adress
                        what = "Adress";
                        break;

                    case 4: //Count
                        what = "Count";
                        break;

                    case 5: //Month
                        what = "Month";
                        break;
                }

               /* while (true)
                {
                    if (selector == 1)
                    {
                        Console.WriteLine
                    } else if (selector == 2)
                    {

                    } else if (selector == 3)
                    {

                    } else if (selector == 4)
                    {

                    } else if (selector == 5)
                    {

                    }*/


                while (true)
                {
                    Console.WriteLine($"\nВведено значение поля {what} -- {_value}");
                    Console.WriteLine("\nОно верно(1.Да 2.Нет): ");
                    string _valStr = Console.ReadLine();
                    int valSw = 0;
                    bool valex = false;
                    try
                    {
                        valSw = Int32.Parse(_valStr);
                    } catch(FormatException valExc)
                    {
                        Console.WriteLine(valExc.Message);
                        valex = true;
                        Console.WriteLine("\nОшибка ввода");
                    }
                    if (!valex)
                    {
                        if (valSw == 1)
                        {
                            value = _value;
                            break;
                        } else if (valSw == 2)
                        {
                            Console.WriteLine("\nВведите новое значение: ");
                            _value = Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("\nНеверное значение");
                        }
                    }
                }



            }
            else
            {

            }
            string query = $"update {tablename} set {what} = '{value}' where id = '{id}'";
            return query;
        }

        public static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = SHA256.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        public static bool CheckString(string Input)
        {
            while (true)
            {
                Console.WriteLine($"\nВведено значение - {Input}, оно верно?(1.Да 2.Нет 3.Выйти)");
                string _answer = Console.ReadLine();
                int answer = 0;
                bool answExc = false;
                try
                {
                    answer = Int32.Parse(_answer);
                } catch(FormatException FormErr)
                {
                    Console.WriteLine($"\n\n{FormErr.Message}");
                    answExc = true;
                }
                if (!answExc)
                {
                    if( answer == 1)
                    {
                        return true;
                    }
                    else if(answer == 2)
                    {
                        Console.WriteLine("\nВведите новое значение: ");
                        Input = Console.ReadLine();
                    }
                    else if(answer == 3)
                    {
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("\nНеверный ответ");
                        continue;
                    }
                }
            }
        }

        public static void Main(string[] args)
        {
            string hash = "cbcadece52e47b05ab38102231e65db9de6126648bfbe617ac92e331757df58a";
            string connectionString =
                "Server=127.0.0.1;" +
                "Database=kurs;" +
                "User ID=admin;" +
                "Password=455685;" +
                "Pooling=false";
                IDbConnection dbcon;
              dbcon = new MySqlConnection(connectionString);

            while (true)
            {
                Console.WriteLine("Введите пароль: ");
                string pass = Console.ReadLine();
                string hash2 = GetHashString(pass);
                hash2 = hash2.ToLower();
                if(String.Equals(hash2, hash))
                {
                    Console.WriteLine("\nВерно\n");
                    break;
                }
                else
                {
                    Console.WriteLine("Пароль неверный\n");
                }
            }

            bool whl = true;
            string number;
            int checkout = 0;
            string tabn;
            bool tabnamecheck = true;
            string tabcheck;
            int tabch;
            while (whl)
            { 
                Console.WriteLine("Выберите действие\n(\n0.Выход \n1.Создать таблицу \n2.Добавить запись \n3.Поиск по фамилии \n4.Редактировать запись \n5.Удалить запись \n6.Вывод по сумме \n7.Вывод по договору \n8.Показать таблицы \n9.Удалить таблицу \n10.Показать все записи):");
                number = Console.ReadLine();
                if (number != "")
                {
                    try
                    {
                        checkout = Int32.Parse(number);
                    }
                    catch (FormatException) { Console.WriteLine("Неверно введено число\n"); }
                } else if(number == "")
                {
                    Console.WriteLine("Введено неправильное число(пустая строка)\n");
                    continue;
                }
                switch (checkout)
                {
                    case 0:
                        Console.WriteLine("Выход....");
                        whl = false;
                        break;

                    case 1:
                        Console.WriteLine("Создание таблицы...\n");
                        Console.WriteLine("Введите название таблицы: ");
                        tabn = Console.ReadLine();
                        while (tabnamecheck)
                        {
                            Console.WriteLine("Введённое название таблицы: " + tabn);
                            Console.WriteLine("\nНазвание верно?(1.Да 2.Нет)");
                            tabcheck = Console.ReadLine();
                            try
                            {
                                tabch = Int32.Parse(tabcheck);
                            } catch (FormatException) {
                                Console.WriteLine("\nНеверно введено число");
                                continue;
                            }

                            if (tabch == 1)
                            {
                                break;
                            } else if (tabch == 2)
                            {
                                Console.WriteLine("\nВведите новое название: ");
                                tabn = Console.ReadLine();

                            } else { Console.WriteLine("\nНеверно введено число"); }
                        }
                        bool exc = false;
                        try
                        {
                            dbcon.Open();
                        }
                        catch (MySqlException)
                        {
                            exc = true;
                            Console.WriteLine("Ошибка подключения к базе.");
                        }
                        if (!exc)
                        {
                            IDbCommand cmd = dbcon.CreateCommand();
                            cmd.CommandText = "use kurs;";
                            cmd.ExecuteNonQuery();
                            bool crcheck = false;
                            try
                            {
                                cmd.CommandText = "create table " + tabn
                                    + "(id int auto_increment, Init varchar(70), Number int, Adress varchar(70), Count int, Month int, primary key(id, Init));";
                                cmd.ExecuteNonQuery();
                            }
                            catch (Exception except) { Console.WriteLine(except.Message); crcheck = true; }
                            if (!crcheck)
                            {
                                Console.WriteLine("Создана таблица, с названием: " + tabn);
                                dbcon.Close();
                                AwaitInput();
                            }
                            else { }
                        }
                        else { }


                        break;

                    case 2:
                        Console.Clear();
                        Console.WriteLine("Добавление записи...\n");
                        dbcon.Open();
                        IDbCommand cmd4 = dbcon.CreateCommand();
                        cmd4.CommandText = "show tables in kurs;";
                        IDataReader reader = cmd4.ExecuteReader();

                        int coun = 0;
                        while (reader.Read())
                        {
                            Console.WriteLine(" " + coun + ". " + reader.GetString(0));
                            coun++;
                        }
                        reader.Close();
                        dbcon.Close();
                        Console.WriteLine("Куда добавлять запись?:\n");

                        string addtable = Console.ReadLine();
                        bool check = true;
                        string tCheckstr;
                        int tNcheck = 0;
                        bool formExc = false;
                        int Number, Count, Month;
                        Number = 0; Count = 0; Month = 0;
                        while (check)
                        {
                            Console.WriteLine("Введено название: " + addtable);
                            Console.WriteLine("\nОно верно?(1.Да 2.Нет)");
                            tCheckstr = Console.ReadLine();
                            try
                            {
                                tNcheck = Int32.Parse(tCheckstr);
                            } catch (FormatException err)
                            {
                                Console.WriteLine(err.Message);
                                formExc = true;
                            }
                            if (!formExc)
                            {
                                if (tNcheck == 1)
                                {
                                    break;
                                } else if (tNcheck == 2)
                                {
                                    Console.WriteLine("\nВведите новое имя: ");
                                    addtable = Console.ReadLine();
                                }
                            }
                            else { }

                        }
                        string Init, Adress;
                        string _Number, _Count, _Month;

                        Console.Clear();
                        Console.WriteLine("Добавление в таблицу: " + addtable);
                        Console.WriteLine("\n\nВведите Инициалы: ");
                        Init = Console.ReadLine();
                        Console.WriteLine("\n\nВведите номер договора: ");
                        _Number = Console.ReadLine();
                        Console.WriteLine("\n\nВведите Адресс: ");
                        Adress = Console.ReadLine();
                        Console.WriteLine("\n\nВведите Сумму взноса: ");
                        _Count = Console.ReadLine();
                        Console.WriteLine("\n\nВведите кол-во месяцев участия: ");
                        _Month = Console.ReadLine();

                        bool cicheck = true;
                        while (cicheck)
                        {
                            bool _NumError = false;
                            bool _CountError = false;
                            bool _MonthError = false;
                            try
                            {
                                Number = Int32.Parse(_Number);
                            }
                            catch (FormatException numerror) { Console.WriteLine(numerror.Message); _NumError = true; }

                            try
                            {
                                Count = Int32.Parse(_Count);
                            }
                            catch (FormatException counterr) { Console.WriteLine(counterr.Message); _CountError = true; }

                            try
                            {
                                Month = Int32.Parse(_Month);
                            }
                            catch (FormatException montherr) { Console.WriteLine(montherr.Message); _MonthError = true; }

                            if (_NumError)
                            {
                                Console.WriteLine("\nОшибка в вводе номера договора");
                                Console.WriteLine("\nВведите корректный номер договора: ");
                                _Number = Console.ReadLine();
                            }
                            else if (_CountError)
                            {
                                Console.WriteLine("\nОшибка в вводе суммы вклада");
                                Console.WriteLine("\nВведите корректную сумму: ");
                                _Count = Console.ReadLine();
                            }
                            else if (_MonthError)
                            {
                                Console.WriteLine("\nОшибка в вводе кол-ва месяцев");
                                Console.WriteLine("\nВведите кооректное число месяцев: ");
                                _Month = Console.ReadLine();
                            }
                            else { cicheck = false; break; }
                        }
                            if (!cicheck)
                            {
                            dbcon.Open();
                                IDbCommand cmd = dbcon.CreateCommand();
                                bool insertcheck = false;
                                try
                                {
                                    cmd.CommandText =  "insert into " + addtable + "(Init, Number, Adress, Count, Month) values('"+ Init + "', " + Number + ", '" + Adress + "', " + Count + ", " + Month + ")";
                                    cmd.ExecuteNonQuery();
                                dbcon.Close();
                                } catch(Exception insex) { Console.WriteLine(insex.Message);  insertcheck = true; }
                                if (!insertcheck)
                                {
                                    Console.WriteLine("\n\nЗапись успешно добавлена");
                                }
                            AwaitInput();

                        }

                        
                        break;

                    case 3:
                        Console.Clear();
                        Console.WriteLine("Поиск...");

                        dbcon.Open();
                        IDbCommand cmd5 = dbcon.CreateCommand();
                        cmd5.CommandText = "show tables in kurs;";
                        IDataReader seaR = cmd5.ExecuteReader();

                        int ss = 0;
                        while (seaR.Read())
                        {
                            Console.WriteLine(" " + ss + ". " + seaR.GetString(0));
                            ss++;
                        }
                        seaR.Close();
                        dbcon.Close();

                        Console.WriteLine("\n\nВведите название таблицы: ");
                        string sTab = Console.ReadLine();

                        while (true)
                        {
                            Console.WriteLine("\nВведено название: " + sTab);
                            Console.WriteLine("\nОно верно?(1.Да 2.Нет)");
                            string varis = Console.ReadLine();
                            int  v = -100;
                            bool eExc = false;
                            try
                            {
                                v = Int32.Parse(varis);
                            }
                            catch (FormatException ansexc)
                            {
                                Console.WriteLine(ansexc.Message);
                                eExc = true;
                            }
                            if (!eExc)
                            {
                                if(v == 1)
                                {
                                    break;
                                }   else if (v == 2)
                                {
                                    Console.WriteLine("Введите название: ");
                                    sTab = Console.ReadLine();
                                }
                            }

                        }
                        Console.WriteLine("\n\nВведите Инициалы для поиска: ");
                        string init = Console.ReadLine();
                        while (true)
                        {
                            Console.WriteLine("\nВведены инициалы: " + init);
                            Console.WriteLine("\nВерно?(1.Да 2.Нет)");
                            string _ccheck = Console.ReadLine();
                            int ccheck = 0;
                            bool fexcep = false;
                            try
                            {
                                ccheck = Int32.Parse(_ccheck);
                            } catch(FormatException fexc)
                            {
                                Console.WriteLine(fexc.Message);
                                fexcep = true;
                            }
                            if (!fexcep)
                            {
                                if (ccheck == 2) {
                                    Console.WriteLine("\nВведите Инициалы: ");
                                    init = Console.ReadLine();
                                } else if(ccheck == 1)
                                {
                                    break;
                                }
                            }
                        }
                        cmd5.CommandText = "SELECT * from " + sTab + " where Init in ('" + init + "');";
                        dbcon.Open();
                        try
                        {
                            IDataReader search = cmd5.ExecuteReader();
                            while (search.Read())
                            {
                                Console.WriteLine($"{search.GetString(0)}:  Инициалы: {search.GetString(1)}  Номер договора: {search.GetString(2)}  Адрес: {search.GetString(3)}  Вклад: {search.GetString(4)}  Срок участия: {search.GetString(5)}");

                            }
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine(error.Message);
                        }
                        dbcon.Close();
                        AwaitInput();
                        break;

                    case 4:
                        Console.Clear();
                        Console.WriteLine("Редактирование записи...");

                        dbcon.Open();
                        IDbCommand cmd6 = dbcon.CreateCommand();
                        cmd6.CommandText = "show tables in kurs;";
                        IDataReader redR = cmd6.ExecuteReader();

                        int red = 0;
                        while (redR.Read())
                        {
                            Console.WriteLine(" " + red + ". " + redR.GetString(0));
                            red++;
                        }
                        redR.Close();
                        dbcon.Close();
                        Console.WriteLine("Введите название таблицы: ");
                        string redn = Console.ReadLine();
                        while (true)
                        {
                            Console.WriteLine("\nВведено название: " + redn);
                            Console.WriteLine("\nОно верно?(1.Да 2.Нет)");
                            string Answ = Console.ReadLine();
                            int Ans = -100;
                            bool AnsExc = false;
                            try
                            {
                                Ans = Int32.Parse(Answ);
                            }
                            catch (FormatException ansexc)
                            {
                                Console.WriteLine(ansexc.Message);
                                AnsExc = true;
                            }
                            if (!AnsExc)
                            {
                                if (Ans == 2)
                                {
                                    Console.WriteLine("\n\nВведите название: ");
                                    redn = Console.ReadLine();
                                }
                                if (Ans == 1)
                                {
                                    break;
                                }
                            }
                            else if (AnsExc)
                            {
                                continue;
                            }
                        }
                        while (true)
                        {
                            Console.WriteLine("\n\nЧто вы хотите сделать?(\n1.Отобразить все записи \n2.Отредактировать запись \n3.Выход)");
                            string _select = Console.ReadLine();
                            bool selectExc = false;
                            int select = 0;
                            try
                            {
                                select = Int32.Parse(_select);

                            } catch(FormatException selExc)
                            {
                                Console.WriteLine(selExc.Message);
                                selectExc = true;
                            }
                            if (!selectExc)
                            {
                                if(select == 1)
                                {
                                    IDbCommand cmd7 = dbcon.CreateCommand();
                                    cmd7.CommandText = "select * from " + redn;
                                    bool readExc = false;
                                   
                                    dbcon.Open();
                                    try
                                    {
                                        IDataReader selectreader = cmd7.ExecuteReader();
                                        Console.WriteLine("\n\n");
                                        Console.WriteLine("Id");
                                        while (selectreader.Read())
                                        {
                                            Console.WriteLine($"{selectreader.GetString(0)}:  Инициалы: {selectreader.GetString(1)}  Номер договора: {selectreader.GetString(2)}  Адрес: {selectreader.GetString(3)}  Вклад: {selectreader.GetString(4)}  Срок участия: {selectreader.GetString(5)}");

                                        }
                                        Console.WriteLine("==========================");
                                    }
                                    catch (Exception insertExc)
                                    {
                                        Console.WriteLine(insertExc.Message);
                                        readExc= true;
                                    }
                                    dbcon.Close();
                                }

                                else if(select == 2)
                                {
                                    Console.WriteLine("\nВведите номер записи(id): ");
                                    string _id = Console.ReadLine();
                                    int id = 0;
                                    bool idExc = false;
                                    try
                                    {
                                        id = Int32.Parse(_id);

                                    } catch(FormatException idF)
                                    {
                                        Console.WriteLine(idF.Message);
                                        idExc = true;
                                    }
                                    if (!idExc)
                                    {
                                        while (true)
                                        {
                                            Console.WriteLine("\nЧто вы хотите изменить?(\n1.Ничего \n2.Инициалы \n3.Номер договора \n4.Адрес \n5.Сумму взноса \n6.Кол-во месяцев участия");
                                            string _selector = Console.ReadLine();
                                            int selector = 0;
                                            bool selec = false;
                                            try
                                            {
                                                selector = Int32.Parse(_selector);
                                            } catch(FormatException selEx)
                                            {
                                                Console.WriteLine(selEx.Message);
                                                selec = true;
                                            }
                                            if (!selec)
                                            {
                                                if (selector == 1) { break; }
                                                switch (selector)
                                                {

                                                    case 2:
                                                        Console.WriteLine("\nВведите инициалы: ");
                                                        string _CInit = Console.ReadLine();
                                                        while (true)
                                                        {
                                                            Console.WriteLine("\nВведены инициалы: " + _CInit + "\nОни верны?(1.Да 2.Нет):");
                                                            string _redInit = Console.ReadLine();
                                                            int redInit = 0;
                                                            bool redInExc = false;
                                                            try
                                                            {
                                                                redInit = Int32.Parse(_redInit);
                                                            } catch(FormatException redForm)
                                                            {
                                                                Console.WriteLine(redForm.Message);
                                                                redInExc = true;

                                                            }
                                                            if (!redInExc)
                                                            {
                                                                if(redInit == 1)
                                                                {
                                                                    break;
                                                                } else if(redInit == 2)
                                                                {
                                                                    Console.WriteLine("\nВведите новые инициалы: ");
                                                                    _CInit = Console.ReadLine();
                                                                }
                                                                else
                                                                {
                                                                    Console.WriteLine("\n\nВведена неверная цифра");
                                                                }
                                                            }
                                                            else { }
                                                        }
                                                        IDbCommand cmd13 = dbcon.CreateCommand();
                                                        cmd13.CommandText = CreateQuery(1,redn,_CInit,id.ToString());
                                                     //   cmd13.CommandText = "update " + redn + " set Init='" + _CInit +"' where id='"+ id+"';";
                                                        bool excep = false;
                                                        try
                                                        {
                                                            dbcon.Open();
                                                            cmd13.ExecuteNonQuery();
                                                            dbcon.Close();
                                                        } catch(MySqlException initExc)
                                                        {
                                                            Console.WriteLine(initExc.Message);
                                                            excep = true;
                                                        }
                                                        if (!excep)
                                                        {
                                                            Console.WriteLine("\nЗапись изменена");
                                                        }
                                                        else { Console.WriteLine("\nОшибка во время редактирования"); }
                                                        Console.WriteLine("\nНажмите на любую клавишу...\n");
                                                        Console.ReadKey();
                                                        Console.Clear();
                                                        break;

                                                    case 3:
                                                        Console.WriteLine("\nВведите номер договора: ");
                                                        string _CNumber = Console.ReadLine();
                                                        while (true)
                                                        {
                                                            Console.WriteLine("\nВведён номер: " + _CNumber + "\nОн правильный?(1.Да 2.Нет):");
                                                            string _redNumber = Console.ReadLine();
                                                            int redNumber = 0;
                                                            bool redNumExc = false;
                                                            try
                                                            {
                                                                redNumber = Int32.Parse(_redNumber);
                                                            }
                                                            catch (FormatException redF)
                                                            {
                                                                Console.WriteLine(redF.Message);
                                                                redNumExc = true;

                                                            }
                                                            if (!redNumExc)
                                                            {
                                                                if (redNumber== 1)
                                                                {
                                                                    break;
                                                                }
                                                                else if (redNumber == 2)
                                                                {
                                                                    Console.WriteLine("\nВведите новый номер: ");
                                                                    _CNumber = Console.ReadLine();
                                                                }
                                                                else
                                                                {
                                                                    Console.WriteLine("\n\nВведена неверная цифра");
                                                                }
                                                            }
                                                            else { }
                                                        }
                                                        IDbCommand cmd14 = dbcon.CreateCommand();
                                                        cmd14.CommandText = "update " + redn + " set Number='" + _CNumber + "' where id='" + id + "';";
                                                        bool exception = false;
                                                        try
                                                        {
                                                            dbcon.Open();
                                                            cmd14.ExecuteNonQuery();
                                                            dbcon.Close();
                                                        }
                                                        catch (MySqlException initException)
                                                        {
                                                            Console.WriteLine(initException.Message);
                                                            exception = true;
                                                        }
                                                        if (!exception)
                                                        {
                                                            Console.WriteLine("\nЗапись изменена");
                                                        }
                                                        else { Console.WriteLine("\nОшибка во время редактирования"); }
                                                        Console.WriteLine("\nНажмите на любую клавишу...\n");
                                                        Console.ReadKey();
                                                        Console.Clear();
                                                        break;


                                                    case 4:
                                                        Console.WriteLine("\nВведите новый Адрес: ");
                                                        string _Adress = Console.ReadLine();
                                                        IDbCommand cmd21 = dbcon.CreateCommand();
                                                        cmd21.CommandText = CreateQuery(3, redn, _Adress, id.ToString());
                                                        bool excepti = false;
                                                        try
                                                        {
                                                            dbcon.Open();
                                                            cmd21.ExecuteScalar();
                                                            dbcon.Close();
                                                        }
                                                        catch (Exception initExc)
                                                        {
                                                            Console.WriteLine(initExc.Message);
                                                            excep = true;
                                                        }
                                                        if (!excepti)
                                                        {
                                                            Console.WriteLine("\nЗапись изменена");
                                                        }
                                                        else { Console.WriteLine("\nОшибка во время редактирования"); }
                                                        Console.WriteLine("\nНажмите на любую клавишу...\n");
                                                        Console.ReadKey();
                                                        Console.Clear();
                                                        break;

                                                    case 5:
                                                        Console.Clear();//redn id
                                                        Console.WriteLine("\nВведите новую сумму взноса: ");
                                                        string redAdd = Console.ReadLine();
                                                        IDbCommand cmd22 = dbcon.CreateCommand();
                                                        dbcon.Open();
                                                        cmd22.CommandText = CreateQuery(4, redn, redAdd, id.ToString());
                                                        try
                                                        {
                                                            cmd22.ExecuteScalar();
                                                                } catch(MySqlException redExc)
                                                        {
                                                            Console.WriteLine(redExc.Message);
                                                        }
                                                        dbcon.Close();
                                                        Console.WriteLine("\nНажмите на любую клавишу...\n");
                                                        Console.ReadKey();
                                                        Console.Clear();
                                                        break;

                                                    case 6:
                                                        Console.Clear();
                                                        Console.WriteLine("\n\nВведите новое кол-во месяцев участия: ");
                                                        string newAddress = Console.ReadLine();
                                                        IDbCommand cmd25 = dbcon.CreateCommand();
                                                        cmd25.CommandText = CreateQuery(5, redn, newAddress, id.ToString());
                                                        try
                                                        {
                                                            dbcon.Open();
                                                            cmd25.ExecuteScalar();
                                                            dbcon.Close();
                                                        } catch(MySqlException newAddrexc)
                                                        {
                                                            Console.WriteLine(newAddrexc.Message);
                                                        }
                                                        break;

                                                    default:
                                                        Console.WriteLine("\n\nНеверное число");
                                                        break;
                                                }
                                            }

                                        }
                                    }
                                }
                                else if(select == 3)
                                {
                                    break;
                                }
                            }
                        }


                        break;

                    case 5: //delete
                        DeleteString();
                        break;

                    case 6: //show from Count
                        Console.Clear();
                        ShowTables();
                        Console.WriteLine("\n\nВведите название таблицы: ");
                        string sumTab = Console.ReadLine();
                        bool checkResult;
                        Console.WriteLine("\n\nВведите сумму взноса: ");
                        string _Summ = Console.ReadLine();
                        checkResult = CheckString(_Summ);
                        if (checkResult)
                        {
                            dbcon.Close();
                            dbcon.Open();
                            IDbCommand cmd33 = dbcon.CreateCommand();
                            cmd33.CommandText = $"select * from {sumTab} where Count = '{_Summ}'";
                            IDataReader summRead = cmd33.ExecuteReader();

                            int sumC = 0;
                            Console.WriteLine("---------------------------");
                            while (summRead.Read())
                            {
                                Console.WriteLine($"  {sumC}. {summRead.GetString(0)} {summRead.GetString(1)} {summRead.GetString(2)} {summRead.GetString(3)} {summRead.GetString(4)}");
                                Console.WriteLine("---------------------------");
                                sumC++;
                            }
                            summRead.Close();
                            dbcon.Close();

                            AwaitInput();

                        }
                        else { }

                        break;

                    case 7: //show from Number
                        Console.Clear();
                        ShowTables();
                        Console.WriteLine("\n\nВведите название таблицы: ");
                        string NumTab = Console.ReadLine();
                        bool checkkResult;
                        Console.WriteLine("\n\nВведите номер договора: ");
                        string _NumberS = Console.ReadLine();
                        checkkResult = CheckString(_NumberS);
                        if (checkkResult)
                        {
                            dbcon.Close();
                            dbcon.Open();
                            IDbCommand cmd34 = dbcon.CreateCommand();
                            cmd34.CommandText = $"select * from {NumTab} where Number = '{_NumberS}'";
                            IDataReader NumRead = cmd34.ExecuteReader();

                            int NumC = 0;
                            Console.WriteLine("---------------------------");
                            while (NumRead.Read())
                            {
                                Console.WriteLine($"  {NumC}. {NumRead.GetString(0)} {NumRead.GetString(1)} {NumRead.GetString(2)} {NumRead.GetString(3)} {NumRead.GetString(4)}");
                                Console.WriteLine("---------------------------");
                                NumC++;
                            }
                            NumRead.Close();
                            dbcon.Close();

                            AwaitInput();

                        }
                        else { }
                        break;

                    case 8: 
                        dbcon.Open();
                        IDbCommand cmd2 = dbcon.CreateCommand();
                        cmd2.CommandText = "show tables in kurs;";
                        IDataReader r = cmd2.ExecuteReader();

                        int i = 0;
                        while (r.Read())
                        {
                            Console.WriteLine(" " + i + ". " + r.GetString(0));
                            i++;
                        }
                        r.Close();
                        dbcon.Close();
                        AwaitInput();
                        break;

                    case 9:
                        dbcon.Open();
                        IDbCommand cmd3 = dbcon.CreateCommand();
                        cmd3.CommandText = "show tables in kurs;";
                        IDataReader rr = cmd3.ExecuteReader();

                        int ii = 0;
                        while (rr.Read())
                        {
                            Console.WriteLine(" " + ii + ". " + rr.GetString(0));
                            ii++;
                        }
                        rr.Close();
                        dbcon.Close();
                        Console.WriteLine("\n\nВведите навзание таблицы:");
                        string deltable = Console.ReadLine();
                        deltable = deltable.Trim();
                        dbcon.Open();

                        cmd3.CommandText = "drop table " + deltable;
                        bool excdeltable = false;
                        try
                        {
                            cmd3.ExecuteNonQuery();
                        } catch(Exception delexcpet)
                        {
                            Console.WriteLine(delexcpet.Message);
                            excdeltable = true;
                        }
                        dbcon.Close();
                        if (!excdeltable)
                        {
                            Console.WriteLine("\n\nУдалена таблица: " + deltable);
                        }
                        else { }
                        AwaitInput();
                        break;

                    case 10:
                        Console.Clear();
                        Console.WriteLine("Вывод записей...");

                        dbcon.Open();
                        IDbCommand cmd10 = dbcon.CreateCommand();
                        cmd10.CommandText = "show tables in kurs;";
                        IDataReader outreader = cmd10.ExecuteReader();

                        int cc = 0;
                        while (outreader.Read())
                        {
                            Console.WriteLine(" " + cc + ". " + outreader.GetString(0));
                            cc++;
                        }
                        outreader.Close();
                        dbcon.Close();

                        Console.WriteLine("\n\nВведите название таблицы: ");
                        string outname = Console.ReadLine();
                        while (true)
                        {
                            Console.WriteLine("\nВведено название: " + outname);
                            Console.WriteLine("\nОно верно?(1.Да 2.Нет)");
                            string answ = Console.ReadLine();
                            int numAns = -100;
                            bool ansExc = false;
                            try
                            {
                                numAns = Int32.Parse(answ);
                            } catch(FormatException ansexc)
                            {
                                Console.WriteLine(ansexc.Message);
                                ansExc = true;
                            }
                            if (!ansExc)
                            {
                                if (numAns == 2)
                                {
                                    Console.WriteLine("\n\nВведите название: ");
                                    outname = Console.ReadLine();
                                }
                                if (numAns == 1)
                                {
                                    break;
                                }
                            } else if (ansExc)
                            {
                                continue;
                            }
                        }
                        cmd10.CommandText = "select * from " + outname;
                        bool InsertExcp = false;

                        dbcon.Open();
                        try
                        {
                            IDataReader selreader = cmd10.ExecuteReader();
                            Console.WriteLine("\n\n");
                            Console.WriteLine("Id");
                            while(selreader.Read()){
                                Console.WriteLine($"{selreader.GetString(0)}:  Инициалы: {selreader.GetString(1)}  Номер договора: {selreader.GetString(2)}  Адрес: {selreader.GetString(3)}  Вклад: {selreader.GetString(4)}  Срок участия: {selreader.GetString(5)}");

                            }
                        } catch(Exception insertExc)
                        {
                            Console.WriteLine(insertExc.Message);
                            InsertExcp = true;
                        }
                        dbcon.Close();
                        AwaitInput();

                        break;

                    default:
                        Console.WriteLine("\nНеверно введено число");
                        AwaitInput();
                        break;
                }
            }

        }
    }
}