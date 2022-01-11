using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTYS_PROJE.Entities.Concrete;
using Oracle.ManagedDataAccess.Client;
using System.Reflection;
using VTYS_PROJE.Core.Entities;
using VTYS_PROJE.Core.LogManager;

namespace VTYS_PROJE.Core.Repository
{
    public class EntityRepository<TEntity> : IEntityRepository<TEntity>
    {
        OracleConnection con;
        private readonly ILogManager _logManager;
        private readonly string table;
        private readonly string joinQ = "";
        private readonly string parameterQ = "*";

        public EntityRepository(ILogManager logManager)
        {
            _logManager = logManager;

            con = new OracleConnection();
            con.ConnectionString = "User Id=VTYS_PROJE;Password=1234;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=localhost)(PORT=1521)))";

            Console.WriteLine(typeof(TEntity).ToString());


            if (typeof(TEntity) == typeof(User))
            {
                table = "Users";

                parameterQ = " Users.id, Users.USER_NAME, Users.NAME, Users.SURNAME, Users.USER_TYPE, Users.PHONE_NUMBER, Users.EMAIL,a.id AS \"address_id\",a.ADDRESS_DESCRIPTION, c.CITY_NAME, d.DISTRICT_NAME ";

                joinQ = " INNER JOIN ADDRESS a ON a.ID = Users.ADDRESS_ID " +
                        " INNER JOIN CITY c ON c.ID = a.CITY_ID " +
                        " INNER JOIN DISTRICT d ON d.ID = a.DISTRICT_ID ";
            }
            else if (typeof(TEntity) == typeof(UserType)) { table = "User_Type"; }
            else if (typeof(TEntity) == typeof(Address)) { table = "Address"; }
            else if (typeof(TEntity) == typeof(Category)) { table = "Category"; }
            else if (typeof(TEntity) == typeof(City)) { table = "City"; }
            else if (typeof(TEntity) == typeof(District)) { table = "District"; }
            else if (typeof(TEntity) == typeof(Permission)) { table = "Permissions"; }
            else if (typeof(TEntity) == typeof(Product))
            {
                table = "Product";

                parameterQ = " PRODUCT.*, c.CATEGORY_NAME, u.USER_NAME AS \"ownername\", u.PHONE_NUMBER,pt.TYPE_NAME ";

                joinQ = " INNER JOIN PRODUCT_TYPE pt ON PRODUCT.PRODUCT_TYPE_ID = pt.ID" +
                           " INNER JOIN CATEGORY c ON c.ID = pt.CATEGORY_ID" +
                           " INNER JOIN USERS u ON u.ID = PRODUCT.OWNER_ID";
            }
            else if (typeof(TEntity) == typeof(ProductType)) { table = "Product_Type"; }

            _logManager.logMessage("Geçerli Tablo --> " + table + "\n\n************************************");
        }

        public int Count(Expression<Func<TEntity, bool>> filter = null)
        {
            var translator = new LinqExpressionToSQL.LinqExpressionToSQL();
            string whereClause = (filter != null) ? " Where " + translator.Translate(filter) : "";

            try
            {
                con.Open();
                OracleCommand c = new OracleCommand("Select count(*) as c From " + table + joinQ + whereClause, con);
                OracleDataReader dr = c.ExecuteReader();
                if (dr.Read())
                {
                    int count = int.Parse(dr["c"].ToString());

                    con.Close();
                    _logManager.logMessage("############Count: " + count);
                    return count;
                }
            }
            catch (Exception e)
            {
                _logManager.logMessage(e.ToString());
            }

            con.Close();
            return -1;
        }

        public bool Delete(int id)
        {
            try
            {
                con.Open();
                OracleCommand c = new OracleCommand("Delete From " + table + " Where id=:id", con);
                c.Parameters.Add("id", id);
                c.ExecuteNonQuery();

                con.Close();
                _logManager.logMessage("----------------------> Silme işlemi başarlı");
                return true;
            }
            catch (Exception e)
            {
                _logManager.logMessage(e.ToString());
            }

            return false;
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {

            var translator = new LinqExpressionToSQL.LinqExpressionToSQL();
            string whereClause = (filter != null) ? " Where " + translator.Translate(filter) : "";


            string sql = "Select " + parameterQ + " From " + table + joinQ + whereClause.Replace("(id", "(" + table + ".id");
            List<TEntity> list = new List<TEntity>();

            try
            {
                con.Open();
                OracleCommand cmd = new OracleCommand(sql, con);
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var newClass = Assembly.GetAssembly(typeof(TEntity)).CreateInstance(typeof(TEntity).FullName);

                    for (int col = 0; col < dr.FieldCount; col++)
                    {
                        object data = (dr[col].GetType() == typeof(Decimal)) ? int.Parse(dr[col].ToString()) :
                            (
                                (dr[col].GetType() == typeof(DateTime)) ? DateTime.Parse(dr[col].ToString()) : dr[col].ToString()
                            );

                        string column = dr.GetName(col).ToString().ToLower().Replace("ı", "i");

                        var prop = newClass.GetType().GetProperty(column);
                        prop.SetValue(newClass, data, null);
                    }
                    list.Add((TEntity)newClass);
                }

                _logManager.logList<TEntity>(list, "İşlem Başarılı! Veriler: \n", "*************************************");


                con.Close();

                return list;
            }
            catch (Exception e)
            {
                _logManager.logMessage(e.ToString());
            }

            return null;
        }

        public TEntity GetById(int id)
        {
            try
            {
                con.Open();
                OracleCommand cmd = new OracleCommand("Select " + parameterQ + " From " + table + joinQ + " Where " + table + ".id = " + id, con);
                OracleDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    var newClass = Assembly.GetAssembly(typeof(TEntity)).CreateInstance(typeof(TEntity).FullName);
                    for (int col = 0; col < dr.FieldCount; col++)
                    {

                        object data = (dr[col].GetType() == typeof(Decimal)) ? int.Parse(dr[col].ToString()) :
                            (
                                (dr[col].GetType() == typeof(DateTime)) ? DateTime.Parse(dr[col].ToString()) : dr[col].ToString()
                            );

                        string column = dr.GetName(col).ToString().ToLower().Replace("ı", "i");

                        var prop = newClass.GetType().GetProperty(column);
                        prop.SetValue(newClass, data, null);

                    }
                    con.Close();

                    _logManager.logEntity<TEntity>((TEntity)newClass, "İşlem Başarılı! Veriler: \n", "*************************************");

                    return (TEntity)newClass;
                }

                con.Close();
            }
            catch (Exception e)
            {
                _logManager.logMessage(e.ToString());
            }

            return default(TEntity);
        }

        public bool Insert(TEntity entity)
        {
            try
            {
                var list = entity.GetType().GetProperties().Where(y => y.Name != "id" &&  y.Name != "base64" && y.GetValue(entity, null) != null).Select(x => x.Name).ToList();
                string insertColumns = string.Join(",", list);
                string insertColumnsParameter = string.Join(",:", list);
                string insertColumnsParameter2 = (":" + string.Join(",:", list));

                con.Open();
                OracleCommand c = new OracleCommand("Insert Into " + table + " (" + insertColumns + ") values (:" + insertColumnsParameter + ")", con);


                foreach (var prop in entity.GetType().GetProperties().Where(y => y.Name != "id"))
                {
                    object o = prop.GetValue(entity, null);
                    if (o == null) continue;
                    object val = o.ToString();

                    string name = prop.Name;
                    if (name == "base64") continue;

                    if (prop.PropertyType == typeof(DateTime))
                    {
                        DateTime dt = DateTime.Parse(val.ToString());
                        string dateTime = dt.ToString("yyyy-MM-dd hh:mm:ss");

                        c.Parameters.Add(dateTime, OracleDbType.TimeStamp).Value = DateTime.Parse(val.ToString());

                        insertColumnsParameter2 = insertColumnsParameter2.Replace((":" + name), dateTime);
                    }
                    else
                    {
                        c.Parameters.Add(prop.Name, val);

                        insertColumnsParameter2 = insertColumnsParameter2.Replace((":" + name), val.ToString());
                    }

                }
                c.ExecuteNonQuery();
                _logManager.logMessage("############Ekleme işlemi başarılı!");

                con.Close();


                return true;
            }
            catch (Exception e)
            {
                _logManager.logMessage(e.ToString());
            }

            return false;
        }

        public bool Update(TEntity entity)
        {
            try
            {
                string query = "Update " + table + " Set";
                string id = entity.GetType().GetProperties().Where(y => y.Name == "id").First().GetValue(entity, null).ToString();

                foreach (var prop in entity.GetType().GetProperties().Where(y => y.Name != "id" && y.GetValue(entity, null) != null))
                {

                    if (prop.PropertyType == typeof(DateTime) && DateTime.Parse(prop.GetValue(entity, null).ToString()) == DateTime.MinValue) continue;

                    string name = prop.Name;
                    query += " " + name + "=:" + name;

                    if (prop != entity.GetType().GetProperties().Where(y => y.Name != "id").Last())
                    {
                        query += ",";
                    }
                }

                char cc = query[query.Length - 1];
                if (cc == ',')
                    query = query.Remove(query.Length - 1);

                string q = query + " Where id=" + id;

                con.Open();
                OracleCommand c = new OracleCommand(query + " Where id=" + id, con);

                foreach (var prop in entity.GetType().GetProperties().Where(y => y.Name != "id"))
                {
                    object o = prop.GetValue(entity, null);
                    if (o == null) continue;
                    object val = o.ToString();

                    string name = prop.Name;

                    if (prop.PropertyType == typeof(DateTime))
                    {
                        if (DateTime.Parse(prop.GetValue(entity, null).ToString()) == DateTime.MinValue) continue;

                        DateTime dt = DateTime.Parse(val.ToString());
                        string dateTime = dt.ToString("yyyy-MM-dd hh:mm:ss");

                        c.Parameters.Add(dateTime, OracleDbType.TimeStamp).Value = DateTime.Parse(val.ToString());
                    }
                    else
                    {
                        c.Parameters.Add(prop.Name, val);
                        q = q.Replace((":" + name), val.ToString());
                    }

                }

                c.ExecuteNonQuery();

                _logManager.logMessage("############Güncelleme işlemi başarılı!");

                con.Close();

                return true;
            }
            catch (Exception e)
            {
                _logManager.logMessage(e.ToString());
            }

            return false;
        }
    }
}
