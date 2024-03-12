using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FreeSqlPad
{
    public partial class frmMain : Form
    {
        IFreeSql db;
        FileInfo codeFileInfo = null;
        string dir = "DynamicCode";
        public frmMain()
        {
            InitializeComponent();
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            db = new FreeSql.FreeSqlBuilder()
            .UseConnectionString(FreeSql.DataType.Sqlite, @"Data Source=freedb.db")
            .UseAutoSyncStructure(false)
            .Build();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            run();
        }

        private void 执行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            run();
        }
        private void run()
        {


            rtbOutput.Clear();

            codeFileInfo = new FileInfo(Path.Combine(dir, DateTime.Now.ToOADate().ToString() + ".cs"));
            var t1 = rtbClass.Text;
            var t2 = rtbFreesql.Text.Trim();
            var ToSql = true;
            var codetext = @"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSqlPad.DynamicCode
{
    public class Template
    {
        " + t1 + @"
        public string sql(IFreeSql db)
        {
            return " + t2 + @";
        }
    }
}
";
            if (t2.EndsWith(".ToSql()")|| t2.EndsWith(".ToSql();"))
            {
                ToSql = true;
            }
            else
            {
                ToSql = false;
                codetext = @"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSqlPad.DynamicCode
{
    public class Template
    {
        " + t1 + @"
        public object data(IFreeSql db)
        {
            return (object) " + t2 + @";
        }
    }
}
";
            }






            //1.创建CSharpCodeProvider的实例 （系统自带编译器）
            CSharpCodeProvider objCSharpCodePrivoder = new CSharpCodeProvider();        //编译器
            //2.创建一个ICodeComplier对象
            ICodeCompiler objICodeCompiler = objCSharpCodePrivoder.CreateCompiler();

            //3.创建一个CompilerParameters的实例
            CompilerParameters objCompilerParameters = new CompilerParameters();         //编译参数
            objCompilerParameters.GenerateInMemory = true;                               //设定在内存中创建程序集
            objCompilerParameters.GenerateExecutable = false;                            //设定是否创建可执行文件,也就是exe文件或者dll文件
            objCompilerParameters.ReferencedAssemblies.Add("System.dll");                //此处代码是添加对应dll文件的引用
            objCompilerParameters.ReferencedAssemblies.Add("System.Core.dll");           //System.Linq存在于System.Core.dll文件中  
            objCompilerParameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            objCompilerParameters.ReferencedAssemblies.Add("BouncyCastle.Crypto.dll");
            objCompilerParameters.ReferencedAssemblies.Add("DmProvider.dll");
            objCompilerParameters.ReferencedAssemblies.Add("FreeSql.dll");
            objCompilerParameters.ReferencedAssemblies.Add("FreeSql.All.dll");
            objCompilerParameters.ReferencedAssemblies.Add("FreeSql.DbContext.dll");
            objCompilerParameters.ReferencedAssemblies.Add("FreeSql.Provider.Dameng.dll");
            objCompilerParameters.ReferencedAssemblies.Add("FreeSql.Provider.MsAccess.dll");
            objCompilerParameters.ReferencedAssemblies.Add("FreeSql.Provider.MySql.dll");
            objCompilerParameters.ReferencedAssemblies.Add("FreeSql.Provider.Odbc.dll");
            objCompilerParameters.ReferencedAssemblies.Add("FreeSql.Provider.Oracle.dll");
            objCompilerParameters.ReferencedAssemblies.Add("FreeSql.Provider.PostgreSQL.dll");
            objCompilerParameters.ReferencedAssemblies.Add("FreeSql.Provider.Sqlite.dll");
            objCompilerParameters.ReferencedAssemblies.Add("FreeSql.Provider.SqlServer.dll");
            objCompilerParameters.ReferencedAssemblies.Add("FreeSql.Repository.dll");
            objCompilerParameters.ReferencedAssemblies.Add("Google.Protobuf.dll");
            objCompilerParameters.ReferencedAssemblies.Add("K4os.Compression.LZ4.dll");
            objCompilerParameters.ReferencedAssemblies.Add("K4os.Compression.LZ4.Streams.dll");
            objCompilerParameters.ReferencedAssemblies.Add("K4os.Hash.xxHash.dll");
            objCompilerParameters.ReferencedAssemblies.Add("Microsoft.Bcl.AsyncInterfaces.dll");
            objCompilerParameters.ReferencedAssemblies.Add("MySql.Data.dll");
            objCompilerParameters.ReferencedAssemblies.Add("NetTopologySuite.dll");
            objCompilerParameters.ReferencedAssemblies.Add("NetTopologySuite.IO.PostGis.dll");
            objCompilerParameters.ReferencedAssemblies.Add("Newtonsoft.Json.dll");
            objCompilerParameters.ReferencedAssemblies.Add("Npgsql.dll");
            objCompilerParameters.ReferencedAssemblies.Add("Npgsql.LegacyPostgis.dll");
            objCompilerParameters.ReferencedAssemblies.Add("Npgsql.NetTopologySuite.dll");
            objCompilerParameters.ReferencedAssemblies.Add("Oracle.ManagedDataAccess.dll");
            objCompilerParameters.ReferencedAssemblies.Add("System.Buffers.dll");
            objCompilerParameters.ReferencedAssemblies.Add("System.Data.SqlClient.dll");
            objCompilerParameters.ReferencedAssemblies.Add("System.Data.SQLite.dll");
            objCompilerParameters.ReferencedAssemblies.Add("System.Memory.dll");
            objCompilerParameters.ReferencedAssemblies.Add("System.Numerics.Vectors.dll");
            objCompilerParameters.ReferencedAssemblies.Add("System.Runtime.CompilerServices.Unsafe.dll");
            objCompilerParameters.ReferencedAssemblies.Add("System.Text.Encodings.Web.dll");
            objCompilerParameters.ReferencedAssemblies.Add("System.Text.Json.dll");
            objCompilerParameters.ReferencedAssemblies.Add("System.Threading.Tasks.Extensions.dll");
            objCompilerParameters.ReferencedAssemblies.Add("System.ValueTuple.dll");
            objCompilerParameters.ReferencedAssemblies.Add("Ubiety.Dns.Core.dll");
            objCompilerParameters.ReferencedAssemblies.Add("ZstdNet.dll");

            string executingAssemblyLocation = Assembly.GetExecutingAssembly().Location;
            objCompilerParameters.ReferencedAssemblies.Add(executingAssemblyLocation);

            //4.创建CompilerResults的实例
            CompilerResults objCompilerResults = objICodeCompiler.CompileAssemblyFromSource(objCompilerParameters, codetext);

            if (objCompilerResults.Errors.HasErrors)
            {
                rtbOutput.AppendText("编译错误：1233");
                rtbOutput.AppendText(Environment.NewLine);
                foreach (CompilerError err in objCompilerResults.Errors)
                {
                    rtbOutput.AppendText(err.ErrorText);
                    rtbOutput.AppendText(Environment.NewLine);
                }

            }
            else// MessageBox.Show("ddd");
            {
                try
                {
                    //5.创建一个Assembly对象(通过反射，调用 CalculateSum 的实例)
                    Assembly objAssembly = objCompilerResults.CompiledAssembly;                       //动态编译程序集
                    object objCsCode = objAssembly.CreateInstance("FreeSqlPad.DynamicCode.Template"); //创建一个类实例对象
                    if (ToSql)
                    {
                        MethodInfo method = objCsCode.GetType().GetMethod("sql");                //获取对象的对应方法


                        string CalculateResult = (string)method.Invoke(objCsCode, new object[] { db });         //调用对象的方法
                        rtbOutput.AppendText(CalculateResult);
                        rtbOutput.AppendText(Environment.NewLine);
                    }
                    else
                    {
                        MethodInfo method = objCsCode.GetType().GetMethod("data");                //获取对象的对应方法


                        var CalculateResult = method.Invoke(objCsCode, new object[] { db });         //调用对象的方法
                        dataGridView1.DataSource = CalculateResult;
                        dataGridView1.Refresh();
                        rtbOutput.AppendText("查询完成");
                        rtbOutput.AppendText(Environment.NewLine);

                    }
                }
                catch (Exception ex)
                {
                    rtbOutput.AppendText(ex.Message);
                    rtbOutput.AppendText(Environment.NewLine);
                }
            }
        }

        private void cbDbType_SelectedValueChanged(object sender, EventArgs e)
        {
            var dbtypestr = (string)cbDbType.SelectedItem;

            if (dbtypestr == "sqlite")
            {
                tbConn.Text = "Data Source=testdb.db";
                tbConn.ReadOnly = true;
            }
            else
            {
                if(tbConn.Text == "Data Source=testdb.db")
                {
                    tbConn.Clear();
                }
                tbConn.ReadOnly = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dbtypestr = (string)cbDbType.SelectedItem;
            db.Dispose();
            if (dbtypestr == "sqlite")
            {
                db = new FreeSql.FreeSqlBuilder()
                .UseConnectionString(FreeSql.DataType.Sqlite, tbConn.Text.Trim())
                .UseAutoSyncStructure(false)
                .Build();
            }
            else
            {
                var dbtype = FreeSql.DataType.Sqlite;
                if (dbtypestr == "sqlserver")
                {
                    dbtype = FreeSql.DataType.SqlServer;
                }
                else if (dbtypestr == "oracle")
                {
                    dbtype = FreeSql.DataType.Oracle;
                }
                else if (dbtypestr == "mysql")
                {
                    dbtype = FreeSql.DataType.MySql;
                }
                else if (dbtypestr == "dm")
                {
                    dbtype = FreeSql.DataType.Dameng;
                }

                else if (dbtypestr == "postgresql")
                {
                    dbtype = FreeSql.DataType.PostgreSQL;
                }
                else
                {
                    MessageBox.Show("不支持的数据库");
                    toolStripStatusLabel1.Text = $"数据库类型：配置失败";
                    return;
                }
                db = new FreeSql.FreeSqlBuilder()
                .UseConnectionString(dbtype, tbConn.Text.Trim())
                .UseAutoSyncStructure(false)
                .Build();
            }
            toolStripStatusLabel1.Text = $"数据库类型：{dbtypestr}";
        }
    }
}
