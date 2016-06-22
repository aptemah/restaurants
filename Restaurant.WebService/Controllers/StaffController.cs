using Intouch.Core;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Intouch.Restaurant.Controllers
{
    public class StaffController : BaseController
    {
        //ожидают
        public JsonResult GetWaitingList(int pointId, Guid sessionId)
        {
            var session = db.RestAppSessions.SingleOrDefault(s => s.Id == sessionId);
            if (session == null) return Json(false, JsonRequestBehavior.AllowGet);

            var point = db.RestPoints.SingleOrDefault(p => p.Point.Id == pointId);
            if (point == null) return Json(false, JsonRequestBehavior.AllowGet);

            var user = session.RestAppUser;
            var orders = db.RestOrders.Where(o => o.RestPoint.Id == point.Id && o.OpenClose == OpenClose.Open && (o.Officiant == null || (o.Officiant.Id == user.Id))).Select(s => new
            {
                Id = s.Id,
                DateCreateYear = s.DateCreate.Year,
                DateCreateMonth = s.DateCreate.Month,
                DateCreateDay = s.DateCreate.Day,
                DateCreateHour = s.DateCreate.Hour,
                DateCreateMin = s.DateCreate.Minute,
                DateCreate = s.DateCreate,
                OpenClose = s.OpenClose,
                OrderNumber = s.OrderNumber,
                TypeOfPayment = s.TypeOfPayment,
                BonusThisTime = s.BonusThisTime,
                WaiterComment = s.WaiterComment,
                Reordering = (s.Officiant != null),
                OrderSumm = s.RestOrderParts.SelectMany(sm => sm.RestOrderProducts).Select(sl => new
                {
                    Price = sl.Price * sl.Quantity
                }).Sum(sm => sm.Price),
                Bonus = s.RestOrderParts.SelectMany(sm => sm.RestOrderProducts).Select(sl => new
                {
                    Price = sl.Price * sl.Quantity
                }).Sum(sm => sm.Price) * point.BonusPercent / 100,
                Orders = db.RestOrderParts.Where(a => a.RestOrder.Id == s.Id).Select(r => new
                {
                    Id = r.Id,
                    Date = r.Date,
                    Comment = r.Comment,
                    CookTime = r.CookTime,
                    ValidPurchase = r.ValidPurchase,
                    TypeOfOrder = r.TypeOfOrder,
                    Products = db.RestOrderProducts.Where(p => p.RestOrderPart.Id == r.Id).Select(p => new
                    {
                        Id = p.Id,
                        Name = p.RestProduct.Name,
                        ProdId = p.RestProduct.Id,
                        Description = p.RestProduct.Description,
                        Weight = p.RestProduct.Weight,
                        Image = p.RestProduct.Image ?? p.RestProduct.Category.Image,
                        Quantity = p.Quantity,
                        Price = p.Price,
                    })
                })
            });
            return Json(new { Orders = orders }, JsonRequestBehavior.AllowGet);
        }
        //указать номер стола
        public JsonResult SetTableNumber(int orderId, string tableNumber)
        {
            var order = db.RestOrders.SingleOrDefault(o => o.Id == orderId);
            if (order == null) return Json(false, JsonRequestBehavior.AllowGet);
            order.TableNumber = tableNumber;
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //проверка заказа на оплату
        public JsonResult CheckOrderForPayment(int orderId)
        {
            var order = db.RestOrders.SingleOrDefault(o => o.Id == orderId);
            if (order == null) return Json(false, JsonRequestBehavior.AllowGet);
            if (order.TypeOfPayment == null) return Json(false, JsonRequestBehavior.AllowGet);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        //удалить продукт из заказа
        public JsonResult DelProdFromOrder(int prodId, int orderPartId)
        {
            var orderPart = db.RestOrderParts.SingleOrDefault(o => o.Id == orderPartId);
            if (orderPart == null) return Json(false, JsonRequestBehavior.AllowGet);

            var product = db.RestOrderProducts.SingleOrDefault(p => p.Id == prodId);
            if (product == null) return Json(false, JsonRequestBehavior.AllowGet);
            db.RestOrderProducts.Remove(product);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        //добавить продукт в заказ
        public JsonResult AddProdToOrder(int prodId, int orderPartId, int quantity)
        {
            var orderPart = db.RestOrderParts.SingleOrDefault(o => o.Id == orderPartId);
            if (orderPart == null) return Json(false, JsonRequestBehavior.AllowGet);

            var product = db.RestProducts.SingleOrDefault(p => p.Id == prodId);
            if (product == null) return Json(false, JsonRequestBehavior.AllowGet);

            var newProd = new RestOrderProduct { Price = product.Price, Quantity = quantity, RestOrderPart = orderPart, RestProduct = product };

            db.RestOrderProducts.Add(newProd);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        //поиск клиента по номеру телефона
        public JsonResult SearchClientByPhone(string phone)
        {
            var client = db.RestAppUsers.SingleOrDefault(c => c.Phone == phone);
            if (client == null) return Json(false, JsonRequestBehavior.AllowGet);
            var session = AddUserToSession(client);
            return Json(new { sessionId = session.Id }, JsonRequestBehavior.AllowGet);
        }
        //поиск блюда
        public JsonResult ProductsBySearch(string namePattern, int pointId)
        {
            var words = namePattern.Split(' ');
            var products = db.RestProducts.Where(p => p.Category.RestPoint.Point.Id == pointId).Select(s => new
            {
                Id = s.Id,
                Name = s.Name,
                Number = s.Number,
                Description = s.Description,
                Weight = s.Weight,
                Price = s.Price,
                Category = s.Category,
                Activity = s.Activity
            });
            for (var i = 0; i < words.Length; i++)
            {
                var word = words[i];
                products = products.Where(p => p.Name.Contains(word) || (p.Description).Contains(word));
            }
            return Json(new { Product = products }, JsonRequestBehavior.AllowGet);
        }
        //пользователи за период
        public JsonResult ClientsByDate(int dayStart, int monthStart, int yearStart, int dayFinish, int monthFinish, int yearFinish)
        {
            var month = DateTimeOffset.Now.AddMonths(-1);
            var dateStart = new DateTimeOffset(yearStart, monthStart, dayStart, 0, 0, 0, new TimeSpan(0));
            var dateFinish = new DateTimeOffset(yearFinish, monthFinish, dayFinish, 0, 0, 0, new TimeSpan(0));
            if (dateStart >= dateFinish) return Json(false, JsonRequestBehavior.AllowGet);
            var users = db.RestAppUsers.Where
                (u => u.RestOrders.Any(o => o.DateCreate >= dateStart
                                            && o.DateCreate <= dateFinish))
                .Select(s => new
                {
                    Id = s.Id,
                    Name = s.Name,
                    Phone = s.Phone,
                    Email = s.Email,
                    TotalMoney = s.RestOrders
                    .SelectMany(sm => sm.RestOrderParts)
                    .SelectMany(sm => sm.RestOrderProducts)
                    .Select(sl => new
                                  {
                                      Price = sl.Price * sl.Quantity
                                  }).Sum(sm => sm.Price),
                    TotalOrders = s.RestOrders.Count(),
                    ActivityLastMonth = db.RestOrders.Count(o => o.RestAppUser.Id == s.Id && o.DateCreate >= month),
                    Bonus = s.RestPayment.Bonus,
                    LastActivity = db.RestOrders.Where(o => o.RestAppUser.Id == s.Id).Select(sl => new
                    {
                        Day = sl.DateCreate.Day,
                        Month = sl.DateCreate.Month,
                        Year = sl.DateCreate.Year,
                        DateCreate = sl.DateCreate
                    }).OrderByDescending(o => o.DateCreate).FirstOrDefault(),
                    Status = s.Role
                });
            return Json(new { Users = users }, JsonRequestBehavior.AllowGet);
        }
        //пользователи по поиску
        public JsonResult ClientsBySearch(string searchPattern, ReadWriteData status)
        {
            var month = DateTimeOffset.Now.AddMonths(-1);

            var users = db.RestAppUsers.Where(p => p.Phone.Contains(searchPattern) || (p.Email).Contains(searchPattern))
                .ToArray()
                .Select(s => new ClientDataModel()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Phone = s.Phone,
                    Email = s.Email,
                    ActivityLastMonth = db.RestOrders.Count(o => o.RestAppUser.Id == s.Id && o.DateCreate >= month),
                    Bonus = s.RestPayment.Bonus,
                    Status = s.Role.ToString(),
                    LastActivityDate = (db.RestOrders.Where(o => o.RestAppUser.Id == s.Id).OrderByDescending(o => o.DateCreate).FirstOrDefault() == null)
                                        ? "" 
                                        : db.RestOrders.Where(o => o.RestAppUser.Id == s.Id).OrderByDescending(o => o.DateCreate).FirstOrDefault().DateCreate.ToString("dd/MM/yyyy")
                });
            if (status == ReadWriteData.Read) return Json(new { Users = users }, JsonRequestBehavior.AllowGet);
            var worksheetPart = CreateXLSFile("c:\\www\\stas.intouchclub.ru\\chris.xlsx");
            GenerateWorksheetPart1Content(worksheetPart, users);

            return null;
        }
        //экран пользователя
        public JsonResult UserEdit(string name, string phone, string email, int role, int userId)
        {
            var user = db.RestAppUsers.SingleOrDefault(u => u.Id == userId);
            if (user == null) return Json(false, JsonRequestBehavior.AllowGet);

            user.Name = name;
            user.Phone = phone;
            user.Email = email;
            user.Role = (RestRole)role;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        //начислить бонусы
        public JsonResult BonusOvercharging(int userId, int bonus)
        {
            var payment = db.RestPayments.SingleOrDefault(u => u.RestAppUser.Id == userId);
            if (payment == null) return Json(false, JsonRequestBehavior.AllowGet);

            payment.Bonus = payment.Bonus + bonus;
            db.Entry(payment).State = EntityState.Modified;
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        //общая статистика
        public JsonResult GeneralStatistics(int pointId, int dayStart, int monthStart, int yearStart, int dayFinish, int monthFinish, int yearFinish)
        {
            var dateStart = new DateTimeOffset(yearStart, monthStart, dayStart, 0, 0, 0, new TimeSpan(0));
            var dateFinish = new DateTimeOffset(yearFinish, monthFinish, dayFinish, 0, 0, 0, new TimeSpan(0));
            if (dateStart >= dateFinish) return Json(false, JsonRequestBehavior.AllowGet);
            var activity = db.RestOrders.Count(u => u.RestPoint.Point.Id == pointId
                                                 && u.DateCreate >= dateStart
                                                 && u.DateCreate <= dateFinish);
            var registration = db.RestAppUsers.Count(u => u.RestOrders.Any(o => o.RestPoint.Point.Id == pointId)
                                                 && u.RegistrationDate >= dateStart
                                                 && u.RegistrationDate <= dateFinish);
            return Json(new { Activity = activity, Registration = registration });
        }
        //статистика по заказам верхняя секция
        public JsonResult OrderStatisticsFirst(int pointId, int dayStart, int monthStart, int yearStart, int dayFinish,
            int monthFinish, int yearFinish)
        {
            var point = db.RestPoints.SingleOrDefault(p => p.Id == pointId);
            if (point == null) return Json(false, JsonRequestBehavior.AllowGet);

            var dateStart = new DateTimeOffset(yearStart, monthStart, dayStart, 0, 0, 0, new TimeSpan(0));
            var dateFinish = new DateTimeOffset(yearFinish, monthFinish, dayFinish, 0, 0, 0, new TimeSpan(0));
            if (dateStart >= dateFinish) return Json(false, JsonRequestBehavior.AllowGet);

            var orderSumm = db.RestOrders.Where(o => o.RestPoint.Point.Id == pointId
                                                 && o.DateCreate >= dateStart
                                                 && o.DateCreate <= dateFinish
                                                 && o.OpenClose == OpenClose.Close)
                                                 .SelectMany(sm => sm.RestOrderParts)
                                                 .SelectMany(sm => sm.RestOrderProducts)
                                                 .Select(s => new
                                                 {
                                                     Price = s.Price * s.Quantity
                                                 }).Sum(sm => sm.Price);

            var orderCount = db.RestOrders.Count(o => o.RestPoint.Point.Id == pointId
                                                      && o.DateCreate >= dateStart
                                                      && o.DateCreate <= dateFinish);
            var avarageCheck = orderSumm / orderCount;

            var bonusSumm = orderSumm * point.BonusPercent / 100;

            return Json(new { orderCount = orderCount, orderSumm = orderSumm, avarageCheck = avarageCheck, bonusSumm = bonusSumm }, JsonRequestBehavior.AllowGet);
        }
        //статистика по заказам средняя секция
        public JsonResult OrderStatisticsSecond(int pointId, int dayStart, int monthStart, int yearStart, int dayFinish,
            int monthFinish, int yearFinish)
        {
            var point = db.RestPoints.SingleOrDefault(p => p.Id == pointId);
            if (point == null) return Json(false, JsonRequestBehavior.AllowGet);

            var dateStart = new DateTimeOffset(yearStart, monthStart, dayStart, 0, 0, 0, new TimeSpan(0));
            var dateFinish = new DateTimeOffset(yearFinish, monthFinish, dayFinish, 0, 0, 0, new TimeSpan(0));
            if (dateStart >= dateFinish) return Json(false, JsonRequestBehavior.AllowGet);

            //способы оплаты
            var orders = db.RestOrders.Where(o => o.RestPoint.Point.Id == pointId
                                                     && o.DateCreate >= dateStart
                                                     && o.DateCreate <= dateFinish
                                                     && o.OpenClose == OpenClose.Close);
            var typePayment = new
                              {
                                  bonus = orders.Count(b => b.TypeOfPayment == TypeOfPayment.Bonus),
                                  card = orders.Count(c => c.TypeOfPayment == TypeOfPayment.CreditCard),
                                  cash = orders.Count(c => c.TypeOfPayment == TypeOfPayment.Cash),
                              };

            var typeOfDelivery = new
                                 {
                                     rest = orders.Count(o => o.RestOrderParts.Any(p => p.TypeOfOrder == TypeOfOrder.Restaurant)),
                                     self = orders.Count(o => o.RestOrderParts.Any(p => p.TypeOfOrder == TypeOfOrder.Self)),
                                     address = orders.Count(o => o.RestOrderParts.Any(p => p.TypeOfOrder == TypeOfOrder.Address))
                                 };
            var repeatOrder = new
                              {
                                  repeatOrder = db.RestOrders.Count(o => o.RestAppUser.RestOrders
                                      .Count(u => u.RestPoint.Point.Id == pointId
                                                     && u.DateCreate >= dateStart
                                                     && u.DateCreate <= dateFinish) > 1),
                                  singleOrder = db.RestOrders.Count(o => o.RestAppUser.RestOrders
                                      .Count(u => u.RestPoint.Point.Id == pointId
                                                     && u.DateCreate >= dateStart
                                                     && u.DateCreate <= dateFinish) == 1)
                              };
            return Json(new { typePayment = typePayment, typeOfDelivery = typeOfDelivery, repeatOrder = repeatOrder }, JsonRequestBehavior.AllowGet);
        }
        //статистика по заказам нижняя секция
        public JsonResult OrderStatisticsThird(int pointId, int dayStart, int monthStart, int yearStart, int dayFinish,
            int monthFinish, int yearFinish)
        {
            var point = db.RestPoints.SingleOrDefault(p => p.Id == pointId);
            if (point == null) return Json(false, JsonRequestBehavior.AllowGet);

            var dateStart = new DateTimeOffset(yearStart, monthStart, dayStart, 0, 0, 0, new TimeSpan(0));
            var dateFinish = new DateTimeOffset(yearFinish, monthFinish, dayFinish, 0, 0, 0, new TimeSpan(0));
            if (dateStart >= dateFinish) return Json(false, JsonRequestBehavior.AllowGet);

            var orders = db.RestOrders
                .Where(o => o.RestPoint.Point.Id == pointId
                    && o.DateCreate >= dateStart
                    && o.DateCreate <= dateFinish
                    && o.OpenClose == OpenClose.Close)
                    .Select(s => new
                    {
                        Id = s.Id,
                        DateCreate = s.DateCreate,
                        OrderNumber = s.OrderNumber,
                        TypeOfPayment = s.TypeOfPayment,
                        Summ = s.RestOrderParts
                            .SelectMany(sm => sm.RestOrderProducts)
                            .Select(sl => new
                                      {
                                          Price = sl.Price * sl.Quantity
                                      }).Sum(sm => sm.Price),
                        Bonus = s.RestOrderParts
                            .SelectMany(sm => sm.RestOrderProducts)
                            .Select(sl => new
                            {
                                Price = sl.Price * sl.Quantity
                            }).Sum(sm => sm.Price) * point.BonusPercent / 100,
                        Orders = db.RestOrderParts
                                .Where(a => a.RestOrder.Id == s.Id)
                                .Select(r => new
                                {
                                    Id = r.Id,
                                    TypeOfOrder = r.TypeOfOrder,
                                    Products = db.RestOrderProducts
                                                .Where(p => p.RestOrderPart.Id == r.Id)
                                                .Select(p => new
                                                {
                                                    Id = p.Id,
                                                    Name = p.RestProduct.Name,
                                                    ProdId = p.RestProduct.Id,
                                                    Image = p.RestProduct.Image ?? p.RestProduct.Category.Image,
                                                    Quantity = p.Quantity,
                                                    Price = p.Price,
                                                })
                                })
                    });

            return Json(new { orders = orders }, JsonRequestBehavior.AllowGet);
        }
        //статистика по пользователям
        public JsonResult ClientStatistics(int pointId, int dayStart, int monthStart, int yearStart, int dayFinish,
            int monthFinish, int yearFinish)
        {
            var point = db.RestPoints.SingleOrDefault(p => p.Id == pointId);
            if (point == null) return Json(false, JsonRequestBehavior.AllowGet);

            var dateStart = new DateTimeOffset(yearStart, monthStart, dayStart, 0, 0, 0, new TimeSpan(0));
            var dateFinish = new DateTimeOffset(yearFinish, monthFinish, dayFinish, 0, 0, 0, new TimeSpan(0));
            if (dateStart >= dateFinish) return Json(false, JsonRequestBehavior.AllowGet);

            var users = db.RestAppUsers
                .Where(u => u.RestOrders
                    .Any(o => o.RestPoint.Point.Id == pointId
                    && o.DateCreate >= dateStart
                    && o.DateCreate <= dateFinish
                    && o.OpenClose == OpenClose.Close));
            var totalUsers = users.Count();
            var newUsers = users.Count(u => u.RegistrationDate >= dateStart && u.RegistrationDate <= dateFinish);
            var regularUsers = users.Count(u => u.RegistrationDate <= dateStart);
            return Json(new { totalUsers = totalUsers, newUsers = newUsers, regularUsers = regularUsers }, JsonRequestBehavior.AllowGet);
        }
        //настройка бонусов
        public JsonResult SetBonus(int pointId, int bonus)
        {
            var restaurant = db.RestPoints.SingleOrDefault(r => r.Point.Id == pointId);
            if (restaurant == null) return Json(false, JsonRequestBehavior.AllowGet);

            restaurant.BonusPercent = bonus;
            db.Entry(restaurant).State = EntityState.Modified;
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }





        //фидбек на отзыв
        public JsonResult FeedbackToReview(int reviewId, string comment)
        {
            var review = db.RestReviews.SingleOrDefault(s => s.Id == reviewId);
            if (review == null) return Json(false, JsonRequestBehavior.AllowGet);
            review.AdminFeedBack = comment;
            review.AdminFeedBackDate = DateTimeOffset.UtcNow;
            db.Entry(review).State = EntityState.Modified;
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        //подтвердить заказ
        public JsonResult ConfirmOrder(int orderPartId)
        {
            var orderPart = db.RestOrderParts.SingleOrDefault(o => o.Id == orderPartId);
            if (orderPart == null) return Json(false, JsonRequestBehavior.AllowGet);
            orderPart.ValidPurchase = ValidPurchase.Confirmed;
            db.Entry(orderPart).State = EntityState.Modified;
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //добавить офика к заказу при считывании кьюар кода
        public JsonResult AddOfficiantToOrder(int orderPartId, Guid sessionId)
        {
            var session = db.RestAppSessions.SingleOrDefault(s => s.Id == sessionId);
            if (session == null) return Json(false, JsonRequestBehavior.AllowGet);
            var user = session.RestAppUser;

            var order = db.RestOrders.SingleOrDefault(o => o.RestOrderParts.Any(p => p.Id == orderPartId));
            if (order == null) return Json(false, JsonRequestBehavior.AllowGet);
            if (order.Officiant == null)
            {
                order.Officiant = user;
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return null;
        }
        //проверка на юзер/персонал
        public JsonResult CheckAdminBySessionId(Guid sessionId)
        {
            var session = db.RestAppSessions.SingleOrDefault(s => s.Id == sessionId);
            if (session == null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            var user = session.RestAppUser;
            if (user.Role == RestRole.User)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        //закрыть заказ при оплате наличкой
        public JsonResult CloseOrderByCash(int orderId, decimal mount)
        {
            var order = db.RestOrders.SingleOrDefault(o => o.Id == orderId);
            if (order == null) return Json(false, JsonRequestBehavior.AllowGet);
            var user = order.RestAppUser;
            var result = BonusOperation(Convert.ToInt32(mount), user.Id, Operation.Order, PlusMinus.Plus, order);
            if (result)
            {
                var close = TestCloseOrder(orderId, 0);
                if (close)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        //таблица открытых заказов офика
        public JsonResult GetOffOpenTable(Guid sessionId)
        {
            var session = db.RestAppSessions.SingleOrDefault(s => s.Id == sessionId);
            if (session == null) return Json(false, JsonRequestBehavior.AllowGet);
            var officiant = session.RestAppUser;

            var orders = db.RestOrders.Where(o => o.Officiant.Id == officiant.Id && o.OpenClose == OpenClose.Open).Select(s => new
            {
                Id = s.Id,
                DateCreateYear = s.DateCreate.Year,
                DateCreateMonth = s.DateCreate.Month,
                DateCreateDay = s.DateCreate.Day,
                DateCreateHour = s.DateCreate.Hour,
                DateCreateMin = s.DateCreate.Minute,
                DateCreate = s.DateCreate,
                OpenClose = s.OpenClose,
                OrderNumber = s.OrderNumber,
                TypeOfPayment = s.TypeOfPayment,
                BonusThisTime = s.BonusThisTime,
                WaiterComment = s.WaiterComment,
                Orders = db.RestOrderParts.Where(a => a.RestOrder.Id == s.Id).Select(r => new
                {
                    Id = r.Id,
                    DateYear = r.Date.Year,
                    DateMonth = r.Date.Month,
                    DateDay = r.Date.Day,
                    DateHour = r.Date.Hour,
                    DateMin = r.Date.Minute,
                    Date = r.Date,
                    Comment = r.Comment,
                    CookTime = r.CookTime,
                    ValidPurchase = (ValidPurchase)r.ValidPurchase,
                    TypeOfOrder = (TypeOfOrder)r.TypeOfOrder,
                    Products = db.RestOrderProducts.Where(p => p.RestOrderPart.Id == r.Id).Select(p => new
                    {
                        Id = p.Id,
                        Name = p.RestProduct.Name,
                        ProdId = p.RestProduct.Id,
                        Description = p.RestProduct.Description,
                        Weight = p.RestProduct.Weight,
                        Image = (p.RestProduct.Image != null) ? p.RestProduct.Image : p.RestProduct.Category.Image,
                        Quantity = p.Quantity,
                        Price = p.Price,
                    })
                })
            });
            return Json(orders, JsonRequestBehavior.AllowGet);
        }
        //часть заказа
        public JsonResult OneOrderPart(int orderPartId)
        {
            var orderPart = db.RestOrderParts.Where(o => o.Id == orderPartId).Select(s => new
            {
                Id = s.Id,
                DateYear = s.Date.Year,
                DateMonth = s.Date.Month,
                DateDay = s.Date.Day,
                DateHour = s.Date.Hour,
                DateMin = s.Date.Minute,
                Date = s.Date,
                Comment = s.Comment,
                CookTime = s.CookTime,
                OrderNumber = s.RestOrder.OrderNumber,
                ValidPurchase = (ValidPurchase)s.ValidPurchase,
                TypeOfOrder = (TypeOfOrder)s.TypeOfOrder,
                Products = db.RestOrderProducts.Where(p => p.RestOrderPart.Id == s.Id).Select(p => new
                {
                    Id = p.Id,
                    Name = p.RestProduct.Name,
                    ProdId = p.RestProduct.Id,
                    Description = p.RestProduct.Description,
                    Weight = p.RestProduct.Weight,
                    Image = (p.RestProduct.Image != null) ? p.RestProduct.Image : p.RestProduct.Category.Image,
                    Quantity = p.Quantity,
                    Price = p.Price,
                })
            }).FirstOrDefault();
            return Json(orderPart, JsonRequestBehavior.AllowGet);
        }
        //весь заказ по одной части
        public JsonResult GetOrderByPart(int orderPartId)
        {
            var order = db.RestOrders.Where(o => o.RestOrderParts.Any(rop => rop.Id == orderPartId)).Select(s => new
            {
                Id = s.Id,
                DateCreateYear = s.DateCreate.Year,
                DateCreateMonth = s.DateCreate.Month,
                DateCreateDay = s.DateCreate.Day,
                DateCreateHour = s.DateCreate.Hour,
                DateCreateMin = s.DateCreate.Minute,
                DateCreate = s.DateCreate,
                OpenClose = s.OpenClose,
                OrderNumber = s.OrderNumber,
                TypeOfPayment = s.TypeOfPayment,
                BonusThisTime = s.BonusThisTime,
                WaiterComment = s.WaiterComment,
                Orders = db.RestOrderParts.Where(a => a.RestOrder.Id == s.Id).Select(r => new
                {
                    Id = r.Id,
                    DateYear = r.Date.Year,
                    DateMonth = r.Date.Month,
                    DateDay = r.Date.Day,
                    DateHour = r.Date.Hour,
                    DateMin = r.Date.Minute,
                    Date = r.Date,
                    Comment = r.Comment,
                    CookTime = r.CookTime,
                    ValidPurchase = r.ValidPurchase,
                    TypeOfOrder = r.TypeOfOrder,
                    Products = db.RestOrderProducts.Where(p => p.RestOrderPart.Id == r.Id).Select(p => new
                    {
                        Id = p.Id,
                        Name = p.RestProduct.Name,
                        ProdId = p.RestProduct.Id,
                        Description = p.RestProduct.Description,
                        Weight = p.RestProduct.Weight,
                        Image = p.RestProduct.Image,
                        Quantity = p.Quantity,
                        Price = p.Price,
                    })
                })
            }).FirstOrDefault();
            return Json(order, JsonRequestBehavior.AllowGet);
        }
        
        //список пользователей, которые делали оплату в этом поинте
        public JsonResult GetUserList(int pointId)
        {
            var month = DateTimeOffset.Now.AddMonths(-1);
            var users = db.RestAppUsers.Where(u => u.RestOrders.Any(o => o.RestPoint.Point.Id == pointId)).Select(s => new
            {
                Id = s.Id,
                Name = s.Name,
                Phone = s.Phone,
                Email = s.Email,
                Role = s.Role,
                CheckUser = s.CheckUser,
                RegistrationYear = s.RegistrationDate.Year,
                RegistrationMonth = s.RegistrationDate.Month,
                RegistrationDay = s.RegistrationDate.Day,
                ActivityLastMonth = db.RestOrders.Count(o => o.RestAppUser.Id == s.Id && o.DateCreate >= month),
                LastMonth = db.RestOrders.Where(o => o.RestAppUser.Id == s.Id).OrderByDescending(o => o.DateCreate)
                .Select(c => new
                {
                    Year = c.DateCreate.Year,
                    Month = c.DateCreate.Month,
                    Day = c.DateCreate.Day
                }).FirstOrDefault(),
                Photo = s.Photo,
                Bonus = s.RestPayment.Bonus
            });
            return Json(users, JsonRequestBehavior.AllowGet);
        }
        //поиск заказа по номеру заказа
        public JsonResult OrderBySearch(string orderNumber)
        {
            var order = db.RestOrders.Where(o => o.OrderNumber == orderNumber).Select(s => new
            {
                Id = s.Id,
                DateYear = s.DateCreate.Year,
                DateMonth = s.DateCreate.Month,
                DateDay = s.DateCreate.Day,
                OpenClose = s.OpenClose,
                TypeOfPayment = s.TypeOfPayment,
                OrderNumber = s.OrderNumber,
            });
            return null;
        }
        //статистика регистраций пользователей за период
        public JsonResult RegistrationByDate(int dayStart, int monthStart, int yearStart, int dayFinish, int monthFinish, int yearFinish, int pointId)
        {
            var dateStart = new DateTimeOffset(yearStart, monthStart, dayStart, 0, 0, 0, new TimeSpan(0));
            var dateFinish = new DateTimeOffset(yearFinish, monthFinish, dayFinish, 0, 0, 0, new TimeSpan(0));
            if (dateStart >= dateFinish) return Json(false, JsonRequestBehavior.AllowGet);
            var users = db.RestAppUsers.Count(u => u.RestOrders.Any(o => o.RestPoint.Point.Id == pointId)
                                                 && u.RegistrationDate >= dateStart
                                                 && u.RegistrationDate <= dateFinish);
            return Json(users, JsonRequestBehavior.AllowGet);
        }


        //редактировать сообщение в чате
        public JsonResult ChatMessageEdit(int messageId, string text)
        {
            var message = db.ChatMessages.SingleOrDefault(r => r.Id == messageId);
            if (message == null) return Json(false, JsonRequestBehavior.AllowGet);

            message.Messages = text;
            db.Entry(message).State = EntityState.Modified;
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        //статистика всего заказов
        public int TotalOrder(int dayStart, int monthStart, int yearStart, int dayFinish, int monthFinish, int yearFinish, int pointId)
        {
            var dateStart = new DateTimeOffset(yearStart, monthStart, dayStart, 0, 0, 0, new TimeSpan(0));
            var dateFinish = new DateTimeOffset(yearFinish, monthFinish, dayFinish, 0, 0, 0, new TimeSpan(0));
            var orders = db.RestOrders.Count(o => o.RestPoint.Point.Id == pointId
                                                 && o.DateCreate >= dateStart
                                                 && o.DateCreate <= dateFinish);
            return orders;
        }
        //статистика сумма заказов
        public decimal TotalOrderSumm(int dayStart, int monthStart, int yearStart, int dayFinish, int monthFinish, int yearFinish, int pointId)
        {
            var dateStart = new DateTimeOffset(yearStart, monthStart, dayStart, 0, 0, 0, new TimeSpan(0));
            var dateFinish = new DateTimeOffset(yearFinish, monthFinish, dayFinish, 0, 0, 0, new TimeSpan(0));

            var orderSumm = db.RestOrders.Where(o => o.RestPoint.Point.Id == pointId
                                                 && o.DateCreate >= dateStart
                                                 && o.DateCreate <= dateFinish)
                                                 .SelectMany(sm => sm.RestOrderParts)
                                                 .SelectMany(sm => sm.RestOrderProducts)
                                                 .Select(s => new
                                                 {
                                                     Price = s.Price * s.Quantity
                                                 }).Sum(sm => sm.Price);
            return orderSumm;
        }
        //статистика средний чек
        public JsonResult AvarageCheck(int dayStart, int monthStart, int yearStart, int dayFinish, int monthFinish, int yearFinish, int pointId)
        {
            var totalSumm = TotalOrderSumm(dayStart, monthStart, yearStart, dayFinish, monthFinish, yearFinish, pointId);
            var totalOrder = TotalOrder(dayStart, monthStart, yearStart, dayFinish, monthFinish, yearFinish, pointId);
            var avarageCheck = totalSumm / totalOrder;
            return Json(avarageCheck, JsonRequestBehavior.AllowGet);
        }
        //закрыть заказ
        public JsonResult CloseOrder(int orderId)
        {
            var order = db.RestOrders.Find(orderId);
            if (order == null) return Json(false, JsonRequestBehavior.AllowGet);
            if (order.RestOrderParts.Any(o => o.ValidPurchase == ValidPurchase.NotConfirmed)) return Json(false, JsonRequestBehavior.AllowGet);
            if (order.TypeOfPayment == null) return Json(false, JsonRequestBehavior.AllowGet);
            order.DateClose = DateTimeOffset.UtcNow;
            order.OpenClose = OpenClose.Close;
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //потом убрать. тестовая страничка для офика
        public JsonResult OpenTable()
        {
            var orders = db.RestOrders.Where(o => o.OpenClose == OpenClose.Open).Select(s => new
            {
                Id = s.Id,
                DateCreateYear = s.DateCreate.Year,
                DateCreateMonth = s.DateCreate.Month,
                DateCreateDay = s.DateCreate.Day,
                DateCreateHour = s.DateCreate.Hour,
                DateCreateMin = s.DateCreate.Minute,
                DateCreate = s.DateCreate,
                OpenClose = s.OpenClose,
                OrderNumber = s.OrderNumber,
                TypeOfPayment = s.TypeOfPayment,
                BonusThisTime = s.BonusThisTime,
                WaiterComment = s.WaiterComment,
                OrderSumm = s.RestOrderParts.SelectMany(sm => sm.RestOrderProducts).Select(sl => new
                {
                    Price = sl.Price * sl.Quantity
                }).Sum(sm => sm.Price),
                Orders = db.RestOrderParts.Where(a => a.RestOrder.Id == s.Id).Select(r => new
                {
                    Id = r.Id,
                    DateYear = r.Date.Year,
                    DateMonth = r.Date.Month,
                    DateDay = r.Date.Day,
                    DateHour = r.Date.Hour,
                    DateMin = r.Date.Minute,
                    Date = r.Date,
                    Comment = r.Comment,
                    CookTime = r.CookTime,
                    ValidPurchase = (ValidPurchase)r.ValidPurchase,
                    TypeOfOrder = (TypeOfOrder)r.TypeOfOrder,
                    Products = db.RestOrderProducts.Where(p => p.RestOrderPart.Id == r.Id).Select(p => new
                    {
                        Id = p.Id,
                        Name = p.RestProduct.Name,
                        ProdId = p.RestProduct.Id,
                        Description = p.RestProduct.Description,
                        Weight = p.RestProduct.Weight,
                        Image = p.RestProduct.Image ?? p.RestProduct.Category.Image,
                        Quantity = p.Quantity,
                        Price = p.Price,
                    })
                })
            });
            return Json(orders, JsonRequestBehavior.AllowGet);
        }

    }
}