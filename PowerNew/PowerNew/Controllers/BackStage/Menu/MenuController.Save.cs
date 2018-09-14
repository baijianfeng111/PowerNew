using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using NPOI.HSSF.UserModel;
using PowerNew.Bll;
using PowerNew.Common;
using PowerNew.Model;

namespace PowerNew.Controllers
{
    public partial class MenuController
    {
        [HttpPost]
        public ActionResult SubmitSave(bjf_menu submitItem)
        {
            try
            {
                var item = MenuManager.GetInstance().SelectOne(m => m.menuname == submitItem.menuname && m.isdelete == false);

                if (submitItem.id == 0 && item != null || (submitItem.id != 0 && item != null && submitItem.id != item.id))
                {
                    return Json(new { state = 1, msg = "对不起,该菜单名称已存在不能重复添加." });
                }
                MenuManager.GetInstance().SaveItem(submitItem);
                return Json(new { state = 0, msg = "保存成功." });
            }
            catch (Exception e)
            {
                LogHelper.log.Error(e.Message);
                return Json(new { state = 1, msg = e.Message });
            }
        }


        /// <summary>
        /// 到处Excel
        /// </summary>
        /// <returns></returns>
        public ActionResult Export()
        {
            var list = new List<int>();
            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet() as HSSFSheet;

            //设置列宽
            sheet.SetColumnWidth(0, 2000);
            sheet.SetColumnWidth(1, 6500);
            sheet.SetColumnWidth(2, 6500);
            sheet.SetColumnWidth(3, 6500);
            sheet.SetColumnWidth(4, 6500);
            sheet.SetColumnWidth(5, 6500);
            sheet.SetColumnWidth(6, 6500);
            sheet.SetColumnWidth(7, 6500);
            sheet.SetColumnWidth(8, 6500);
            sheet.SetColumnWidth(9, 6500);
            sheet.SetColumnWidth(10, 6500);


            var cs = (HSSFCellStyle)workbook.CreateCellStyle();
            var font1 = (HSSFFont)workbook.CreateFont();
            font1.FontHeightInPoints = 11;
            font1.FontName = "宋体";
            cs.SetFont(font1);


            //填充表头  
            var dataRow = sheet.CreateRow(0) as HSSFRow;
            dataRow.CreateCell(0).SetCellValue("序号");
            dataRow.CreateCell(1).SetCellValue("意向书编号");
            dataRow.CreateCell(2).SetCellValue("省份");
            dataRow.CreateCell(3).SetCellValue("城市");
            dataRow.CreateCell(4).SetCellValue("地区");
            dataRow.CreateCell(5).SetCellValue("地址");
            dataRow.CreateCell(6).SetCellValue("应付金额");
            dataRow.CreateCell(7).SetCellValue("签约人");
            dataRow.CreateCell(8).SetCellValue("签约时间");
            dataRow.CreateCell(9).SetCellValue("备注");
            dataRow.CreateCell(10).SetCellValue("最后更新时间");

            foreach (var cell in dataRow.Cells)
            {
                cell.CellStyle = cs;
            }
            for (int i = 0; i < list.Count; i++)
            {

                dataRow = sheet.CreateRow(i + 1) as HSSFRow;
                dataRow.CreateCell(0).SetCellValue(i + 1);
                dataRow.CreateCell(1).SetCellValue(i);
                dataRow.CreateCell(2).SetCellValue(i);
                dataRow.CreateCell(3).SetCellValue(i);
                dataRow.CreateCell(4).SetCellValue(i);
                dataRow.CreateCell(5).SetCellValue(i);
                dataRow.CreateCell(6).SetCellValue(i);
                dataRow.CreateCell(7).SetCellValue(i);
                dataRow.CreateCell(8).SetCellValue(i);
                dataRow.CreateCell(9).SetCellValue(i);
                dataRow.CreateCell(10).SetCellValue(i);
                foreach (var cell in dataRow.Cells)
                {
                    cell.CellStyle = cs;
                }
            }


            var ms = new MemoryStream();
            workbook.Write(ms);
            ms.Position = 0;
            var newfilename = "到处Excel名字";
            var filename = newfilename + (DateTime.Now.ToString("yyyy-MM-dd")) + ".xls";
            string curBrowser = HttpContext.Request.Browser.Type.ToLower();
            if (curBrowser.Contains("internetexplorer"))       //ie浏览器
            {
                filename = HttpUtility.UrlEncode(filename, Encoding.UTF8);
            }
            else  //其他浏览器
            {
                filename = HttpUtility.UrlDecode(filename, Encoding.UTF8); ;
            }
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
            Response.ContentType = "application/ms-excel";
            Response.Charset = "GB2312";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            Response.BinaryWrite(ms.ToArray());
            return null;
        }

    }
}