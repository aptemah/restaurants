using Excel;
using ExifLib;
using Intouch.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Intouch.Restaurant
{
    [CORSActionFilter]
    public class BaseController : Controller
    {
        protected readonly CoreContext db = new CoreContext();

        protected string GetMd5Hash(MD5 md5Hash, string input)
        {
            var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sBuilder = new StringBuilder();
            foreach (var t in data)
            {
                sBuilder.Append(t.ToString("x2"));
            }
            return sBuilder.ToString();
        }
        protected ImageObjModel ResizeOrigImg(HttpPostedFileBase imageFile)
        {
            const int nWidth = 700;
            const int nHeight = 600;
            const int mWidth = 200;
            const int mHeight = 200;
            ushort orientation = 1;
            try
            {
                var reader = new ExifReader(imageFile.InputStream);
                reader.GetTagValue(ExifTags.Orientation, out orientation);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }
            var image = Image.FromStream(imageFile.InputStream, true, true);
            if (orientation == 6)
            {
                image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }

            int newWidth, newHeight;
            int newMwidth, newMheight;

            var coefH = (double)nHeight / (double)image.Height;
            var coefW = (double)nWidth / (double)image.Width;

            var coefMh = (double)mHeight / (double)image.Height;
            var coefMw = (double)mWidth / (double)image.Width;

            if (coefW >= coefH)
            {
                newHeight = (int)(image.Height * coefH);
                newWidth = (int)(image.Width * coefH);
            }
            else
            {
                newHeight = (int)(image.Height * coefW);
                newWidth = (int)(image.Width * coefW);
            }

            if (coefMw >= coefMh)
            {
                newMheight = (int)(image.Height * coefMh);
                newMwidth = (int)(image.Width * coefMh);
            }
            else
            {
                newMheight = (int)(image.Height * coefMw);
                newMwidth = (int)(image.Width * coefMw);
            }

            Image result = new Bitmap(newWidth, newHeight);
            Image resultMini = new Bitmap(newMwidth, newMheight);

            using (var g = Graphics.FromImage(result))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;


                g.DrawImage(image, 0, 0, newWidth, newHeight);
                g.Dispose();
            }

            using (var gm = Graphics.FromImage(resultMini))
            {
                gm.SmoothingMode = SmoothingMode.HighQuality;
                gm.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gm.CompositingQuality = CompositingQuality.HighQuality;
                gm.InterpolationMode = InterpolationMode.HighQualityBicubic;


                gm.DrawImage(image, 0, 0, newMwidth, newMheight);
                gm.Dispose();
            }

            return new ImageObjModel { Image = result, Mini = resultMini };
        }
        public int GetNetworkId()
        {
            var host = this.HttpContext.Request.Url.Host;
            var network = WebConfigurationManager.AppSettings[host];
            int networkId;
            int.TryParse(network, out networkId);
            return networkId;
        }
        public bool BonusOperation(int mount, int userId, Operation operation, PlusMinus plusMinus, RestOrder order)
        {
            var payments = db.RestPayments.Single(p => p.RestAppUser.Id == userId);
            if (payments == null) return false;
            if (plusMinus == PlusMinus.Minus)
            {
                payments.Bonus -= mount;
                db.Entry(payments).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                payments.Bonus += mount;
                db.Entry(payments).State = EntityState.Modified;
                db.SaveChanges();
            }
            var bonusHistory = new RestBonusHistory
            {
                RestOrder = order,
                PlusMinus = plusMinus,
                Operation = operation,
                Mount = mount,
                //RestAppUser = db.RestAppUsers.Find(userId),
                CurrentBonus = payments.Bonus,
                Date = DateTimeOffset.UtcNow
            };
            db.RestBonusHistorys.Add(bonusHistory);
            db.SaveChanges();
            return true;
        }
        public bool TestCloseOrder(int? orderId, int typePayment)
        {
            var order = db.RestOrders.Find(orderId);
            if (order == null) return false;
            order.TypeOfPayment = (TypeOfPayment)typePayment;
            order.DateClose = DateTimeOffset.UtcNow;
            order.OpenClose = OpenClose.Close;
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        } 
        public int SendCode(string phone)
        {
            var rand = new Random();
            var code = rand.Next(1111, 9999);
            //var gateway = GetCurrentWifiSession(mac).Point.Network.Gateway;
            var Host = "my5.t-sms.ru";
            var User = "INTmedia";
            var Password = "fogola69";
            var Address = "FitnessWiFi";
            using (var client = new System.Net.WebClient())
            {
                string s = client.DownloadString("https://" + Host + "/sendsms.php?user=" + User + "&pwd=" + Password + "&sadr=" + Address + "&dadr=" + phone + "&text=Код подтверждения: " + code);
            }
            return code;
        }
        public double GetDistance(double? latitude, double? longitude, AllClubsModel rest)
        {
            const double radius = 6372.795;
            var lat1 = latitude * Math.PI / 180;
            var long1 = longitude * Math.PI / 180;
            var lat2 = rest.latitude * Math.PI / 180;
            var long2 = rest.longitude * Math.PI / 180;
            var cl1 = Math.Cos((double)lat1);
            var cl2 = Math.Cos(lat2);
            var sl1 = Math.Sin((double)lat1);
            var sl2 = Math.Sin(lat2);
            var delta = long2 - long1;
            var cdelta = Math.Cos((double)delta);
            var sdelta = Math.Sin((double)delta);
            var y = Math.Sqrt(Math.Pow(cl2 * sdelta, 2) + Math.Pow(cl1 * sl2 - sl1 * cl2 * cdelta, 2));
            var x = sl1 * sl2 + cl1 * cl2 * cdelta;
            var ad = Math.Atan2(y, x);
            var dist = ad * radius;
            dist = Math.Round(dist, 2);
            return dist;
        }
        public RestAppSession AddUserToSession(RestAppUser user)
        {
            var sessionId = Guid.NewGuid();
            var restAppSession = db.RestAppSessions.SingleOrDefault(f => f.RestAppUser.Id == user.Id && f.EndDate == null);
            if (restAppSession != null) return restAppSession;
            restAppSession = new RestAppSession
            {
                Id = sessionId,
                RestAppUser = user,
                StartDate = DateTimeOffset.UtcNow,
                LastNewsView = DateTimeOffset.UtcNow,
                LastOccasionView = DateTimeOffset.UtcNow
            };
            db.RestAppSessions.Add(restAppSession);
            db.SaveChanges();
            return restAppSession;
        }
        public bool CheckAdmin(Guid sessionId)
        {
            var session = db.RestAppSessions.SingleOrDefault(s => s.Id == sessionId);
            if (session == null) return false;
            var user = session.RestAppUser;
            return (user.Role != RestRole.User);
        }
        public bool CheckManager(Guid sessionId)
        {
            var session = db.RestAppSessions.SingleOrDefault(s => s.Id == sessionId);
            if (session == null) return false;
            var user = session.RestAppUser;
            return (user.Role == RestRole.Admin);
        }



        //excel reading menu
        public bool ParseFile(string filePath, int pointId)
        {
            var restaurant = db.RestPoints.SingleOrDefault(r => r.Point.Id == pointId);
            if (restaurant == null) return false;

            DataTable table;
            using (var fstream = System.IO.File.OpenRead(filePath))
            {
                IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(fstream);
                DataSet result = excelReader.AsDataSet();
                table = result.Tables[0];
                excelReader.Close();
            }
            AddCategorys(table, restaurant, 0, 0, null);
            return true;
        }
        public void AddCategorys(DataTable table, RestPoint restaurant, int rowStart, int colStart, int? categoryId)
        {
            for (int i = rowStart; i < table.Rows.Count; i++)
            {
                if ((table.Rows[i].ItemArray[colStart].ToString() != "") &&
                    (table.Rows[i].ItemArray[colStart].ToString() != "продукты:"))
                {
                    int parentCatId;
                    if (categoryId.HasValue)
                    {
                        var parentCategory = db.RestCategorys.Find(categoryId);
                        var category = new RestCategory
                        {
                            RestPoint = restaurant,
                            Name = table.Rows[i].ItemArray[colStart].ToString(),
                            ParentCategory = parentCategory,
                            Activity = Activity.Active
                        };
                        db.RestCategorys.Add(category);
                        db.SaveChanges();
                        parentCatId = category.Id;
                        colStart++;
                        AddCategorys(table, restaurant, i, colStart, parentCatId);
                        colStart--;
                        continue;
                    }
                    else
                    {
                        var category = new RestCategory
                        {
                            RestPoint = restaurant,
                            Name = table.Rows[i].ItemArray[colStart].ToString(),
                            Activity = Activity.Active
                        };
                        db.RestCategorys.Add(category);
                        db.SaveChanges();
                        parentCatId = category.Id;
                        colStart++;
                        AddCategorys(table, restaurant, i, colStart, parentCatId);
                        colStart--;
                        continue;
                    }
                }
                if (table.Rows[i].ItemArray[colStart].ToString() == "продукты:")
                {
                    AddProducts(table, restaurant, i + 1, colStart, categoryId);

                    break;
                }
            }
        }
        public void AddProducts(DataTable table, RestPoint restaurant, int rowStart, int colStart, int? categoryId)
        {
            for (int i = rowStart; i < table.Rows.Count; i++)
            {
                if ((table.Rows[i].ItemArray[colStart].ToString() != "") &&
                    (table.Rows[i].ItemArray[colStart].ToString() != "продукты:"))
                {
                    var category = db.RestCategorys.Find(categoryId);
                    var product = new RestProduct { Price = Decimal.Parse(table.Rows[i].ItemArray[colStart + 2].ToString()), Category = category, Name = table.Rows[i].ItemArray[colStart].ToString(), Weight = table.Rows[i].ItemArray[colStart + 1].ToString(), Activity = Activity.Active, Description = table.Rows[i].ItemArray[colStart + 3].ToString(), Number = Int32.Parse(table.Rows[i].ItemArray[colStart + 4].ToString()) };
                    db.RestProducts.Add(product);
                    db.SaveChanges();
                }
                else
                {
                    break;
                }
            }
        }
        //excel write menu
        public WorksheetPart CreateXLSFile(string filePath)
        {
            var excel = new ExcelGeneratedClass();
            var workbook = excel.CreatePackage(filePath);
            //var workbook = excel.CreatePackage("c:\\www\\stas.intouchclub.ru\\chris.xlsx");

            WorksheetPart worksheetPart = workbook.AddNewPart<WorksheetPart>("rId1");

            return worksheetPart;
        }

        public void GenerateWorksheetPart1Content(WorksheetPart worksheetPart1, IEnumerable<ClientDataModel> clients)
        {

            Worksheet worksheet1 = new Worksheet();
            SheetData sheetData1 = new SheetData();

            Row row1 = new Row();
            Cell cell1 = new Cell() { CellReference = "A1", DataType = CellValues.InlineString };
            InlineString inlineString1 = new InlineString();
            Text text1 = new Text();
            text1.Text = "hello";
            inlineString1.Append(text1);
            cell1.Append(inlineString1);
            row1.Append(cell1);

            sheetData1.Append(row1);
            worksheet1.Append(sheetData1);
            worksheetPart1.Worksheet = worksheet1;
        }
    }
}