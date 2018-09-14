using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using LinqKit;
using MySql.Data.MySqlClient;
using PowerNew.Common;
using PowerNew.Model;

namespace PowerNew.Bll
{

    public partial class AccountManager
    {
        public bjf_account GetItem(int id)
        {
            return this.SelectOne(m => m.isdelete == false && m.id == id);
        }

        public bjf_account GetByMobile(string mobile)
        {
            var param = EncryAndDecryptHelper.Encryption(mobile);
            return this.SelectOne(m => m.isdelete == false && m.mobile == param);
        }


        public void SaveItem(bjf_account submitItem)
        {
            if (submitItem.id == 0)
            {
                submitItem.createid = Convert.ToInt32(SessionHelper.GetSession("userid"));
                submitItem.updateid = Convert.ToInt32(SessionHelper.GetSession("userid"));
                submitItem.createtime = DateTime.Now;
                submitItem.updatetime = DateTime.Now;

                submitItem.mobile = EncryAndDecryptHelper.Encryption(submitItem.mobile);
                submitItem.dbpassword = EncryAndDecryptHelper.Encryption(submitItem.dbpassword);
                submitItem.domainname = string.Format("{0}.login.cn", submitItem.domainname);
                submitItem.state = (int)AccountState.未创建;

                this.Add(submitItem);
            }
            else
            {
                var item = this.GetItem(submitItem.id);
                item.name = submitItem.name;
                item.mobile = EncryAndDecryptHelper.Encryption(submitItem.mobile);
                item.domainshortname = submitItem.domainshortname;
                item.domainname = string.Format("{0}.login.cn", submitItem.domainname);
                item.dbname = submitItem.dbname;
                item.dbserver = submitItem.dbserver;
                item.dbuserid = submitItem.dbuserid;
                item.dbpassword = EncryAndDecryptHelper.Encryption(submitItem.dbpassword);
                item.comment = submitItem.comment;

                item.updateid = Convert.ToInt32(SessionHelper.GetSession("userid"));
                item.updatetime = DateTime.Now;
                this.Update(item);
            }
            this.Save();
        }

        /// <summary>
        /// 创建数据库
        /// </summary>
        public void CreateDataBase(bjf_account submitItem)
        {
            try
            {

                submitItem.state = (int)AccountState.创建中;
                this.Update(submitItem);

                var createSql = string.Format("CREATE DATABASE {0};", submitItem.dbname);
                //创建数据库
                this.ExcuteSql(createSql);
                //初始化数据库
                Task.Factory.StartNew(() => InitDataBase(submitItem));

            }
            catch (Exception ex)
            {
                var msg = string.Format("创建数据库出错：{0}", ex.Message);
                LogHelper.log.Error(msg);
            }
        }
        /// <summary>
        /// 初始化数据库
        /// </summary>
        public void InitDataBase(bjf_account submitItem)
        {
            try
            {
                string path = HttpRuntime.AppDomainAppPath.ToString();

                if (!path.EndsWith(@"\"))
                {
                    path += @"\";
                }
                path += @"asset\sql\powerbjf.sql"; //获取脚本位置
                if (File.Exists(path))
                {
                    FileInfo file = new FileInfo(path);
                    var sqlScript = file.OpenText().ReadToEnd();
                    if (!string.IsNullOrWhiteSpace(sqlScript))      //脚本不能为空
                    {
                        var connection = this.GetDbConnectionString(submitItem); //获取mysql数据库连接字符串
                        using (var conn = new MySqlConnection(connection))
                        {
                            using (var cmd = new MySqlCommand(sqlScript, conn))
                            {
                                try
                                {
                                    conn.Open();
                                    cmd.ExecuteNonQuery();
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                    throw;
                                }
                            }
                            //创建完毕更改创建状态
                            UpdateCreateState(submitItem);
                        }
                    }
                    else
                    {
                        var msg = string.Format("读取数据库脚本出错：{0}", path);
                        LogHelper.log.Error(msg);
                        throw new Exception(msg);
                    }
                }
                else
                {
                    var msg = string.Format("找不到数据库脚本：{0}", path);
                    LogHelper.log.Error(msg);
                    throw new Exception(msg);
                }
            }
            catch (Exception ex)
            {
                var msg = string.Format("初始化数据库出错：{0}", ex.Message);
                LogHelper.log.Error(msg);
                throw new Exception(msg);
            }


        }

        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="item"></param>
        public void UpdateCreateState(bjf_account item)
        {
            item.state = (int)AccountState.已创建;
            this.Update(item);
            this.Save();
        }


        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string GetDbConnectionString(bjf_account item)
        {
            return string.Format(
                "server={0};database={1};user id={2};password={3};Convert Zero Datetime=True;Allow Zero Datetime=True;Connect Timeout=21600;", item.dbserver, item.dbname, item.dbuserid, EncryAndDecryptHelper.Decryption(item.dbpassword));

        }
    }
}
