using Intouch.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Web.Mvc;

namespace Intouch.Restaurant.Controllers
{
    public class OrderController : BaseController
    {
        //Открытие заказа
        public JsonResult OpenOrder(int? orderId, Guid sessionId, string orders, int typeOrder, int pointId, string cookTime, string orderAddress)
        {
            var session = db.RestAppSessions.SingleOrDefault(s => s.Id == sessionId);
            if (session == null) return Json(false, JsonRequestBehavior.AllowGet);
            var user = session.RestAppUser;

            var openOrderCheck = db.RestOrders.Where(o => o.RestAppUser.Id == user.Id && o.OpenClose == OpenClose.Open && o.TypeOfPayment != null).OrderByDescending(d => d.DateCreate).FirstOrDefault();
            if (openOrderCheck != null) return Json(true, JsonRequestBehavior.AllowGet);

            var orderCheck = db.RestOrders.Where(o => o.RestAppUser.Id == user.Id && o.OpenClose == OpenClose.Open).OrderByDescending(d => d.DateCreate).FirstOrDefault();
            
            if (orderCheck == null)
            {
                var point = db.RestPoints.SingleOrDefault(p => p.Point.Id == pointId);
                if (point == null) return Json(false, JsonRequestBehavior.AllowGet);

                var rand = new Random();
                var randNumb = rand.Next(1111, 9999);
                var bonus = db.RestPayments.SingleOrDefault(b => b.RestAppUser.Id == user.Id);
                var order = new RestOrder
                {
                    OpenClose = OpenClose.Open,
                    DateCreate = DateTimeOffset.UtcNow,
                    RestAppUser = user,
                    RestPoint = point,
                    OrderAddress = orderAddress,
                    BonusThisTime = (bonus != null) ? (bonus.Bonus) : 0
                };
                
                db.RestOrders.Add(order);
                db.SaveChanges();

                string orderNumber = randNumb.ToString() + order.Id;
                

                order.OrderNumber = orderNumber;
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();

                var orderPartId = AddOrder(order.Id, orders, typeOrder, cookTime);
               
                return Json(new { OrderId = order.Id, OrderPartId = orderPartId, code = orderNumber }, JsonRequestBehavior.AllowGet);
                
            }
            if (orderId.HasValue)
            {
                var order = db.RestOrders.SingleOrDefault(o => o.Id == orderId);
                if (order == null) return Json(false, JsonRequestBehavior.AllowGet);
                var orderPartId = AddOrder(order.Id, orders, typeOrder, cookTime);
                return Json(new { OrderId = order.Id, OrderPartId = orderPartId, code = order.OrderNumber }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var order = db.RestOrders.Where(o => o.RestAppUser.Id == user.Id && o.OpenClose == OpenClose.Open).OrderByDescending(d => d.DateCreate).FirstOrDefault();
                if (order == null) return Json(false, JsonRequestBehavior.AllowGet);
                var orderPartId = AddOrder(order.Id, orders, typeOrder, cookTime);
                return Json(new { OrderId = order.Id, OrderPartId = orderPartId, code = order.OrderNumber }, JsonRequestBehavior.AllowGet);
            }
        }
        //проверка на открытый заказ
        public JsonResult CheckReorder(Guid sessionId)
        {
            var session = db.RestAppSessions.SingleOrDefault(s => s.Id == sessionId);
            if (session == null) return Json(false, JsonRequestBehavior.AllowGet);
            var user = session.RestAppUser;

            var orderCheck = db.RestOrders.FirstOrDefault(o => o.RestAppUser.Id == user.Id && (o.OpenClose == OpenClose.Open));
            if (orderCheck == null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        //добавить продукты в заказ
        public int AddOrder(int orderId, string orders, int typeOrder, string cookTime)
        {
            var products = JsonConvert.DeserializeObject<List<OrderModel>>(orders);
            var orderPart = new RestOrderPart { Date = DateTimeOffset.UtcNow, RestOrder = db.RestOrders.Find(orderId), TypeOfOrder = (TypeOfOrder)typeOrder, CookTime = cookTime, ValidPurchase = ValidPurchase.NotConfirmed};
          
            db.RestOrderParts.Add(orderPart);
            db.SaveChanges();

            foreach (var i in products)
            {
                var product = new RestOrderProduct { RestOrderPart = orderPart, RestProduct = db.RestProducts.Find(i.ProductId), Quantity = i.Quantity, Price = db.RestProducts.Find(i.ProductId).Price };
                db.RestOrderProducts.Add(product);
                db.SaveChanges();
            }
            return orderPart.Id;
        }

        //проверка пароля для оплаты картой
        public JsonResult CheckPasswordForPayment(Guid sessionId, string password)
        {
            var session = db.RestAppSessions.SingleOrDefault(s => s.Id == sessionId);
            if (session == null) return Json(false, JsonRequestBehavior.AllowGet);

            var user = session.RestAppUser;

            using (var md5Hash = MD5.Create())
            {
                if (user.Password != GetMd5Hash(md5Hash, password)) return Json(false, JsonRequestBehavior.AllowGet);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        //сумма заказа
        public JsonResult GetOrderSum(int? orderId, Guid sessionId)
        {
            if (orderId.HasValue)
            {
                var orderSumm = db.RestOrders.Where(o => o.Id == orderId).SelectMany(sm => sm.RestOrderParts).SelectMany(sm => sm.RestOrderProducts).Select(s => new
                {
                    Price = s.Price * s.Quantity
                }).Sum(sm => sm.Price);
                return Json(orderSumm, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var session = db.RestAppSessions.SingleOrDefault(s => s.Id == sessionId);
                if (session == null) return Json(false, JsonRequestBehavior.AllowGet);
                var user = session.RestAppUser;
                var order = db.RestOrders.Where(o => o.RestAppUser.Id == user.Id && o.OpenClose == OpenClose.Open).OrderByDescending(d => d.DateCreate).FirstOrDefault();
                var orderSumm = db.RestOrders.Where(o => o.Id == order.Id).SelectMany(sm => sm.RestOrderParts).SelectMany(sm => sm.RestOrderProducts).Select(s => new
                {
                    Price = s.Price * s.Quantity
                }).Sum(sm => sm.Price);
                return Json(orderSumm, JsonRequestBehavior.AllowGet);
            }
        }

        //оплата
        public JsonResult PaymentByCard(int? orderId, Guid sessionId, decimal orderSum)
        {
            var session = db.RestAppSessions.SingleOrDefault(s => s.Id == sessionId);
            if (session == null) return Json(false, JsonRequestBehavior.AllowGet);
            var user = session.RestAppUser;
            var order = (orderId.HasValue) ?
                (db.RestOrders.SingleOrDefault(o => o.Id == orderId)) :
                (db.RestOrders.Where(o => o.RestAppUser.Id == user.Id && o.OpenClose == OpenClose.Open)
                    .OrderByDescending(d => d.DateCreate).FirstOrDefault());
            if (order == null) return Json(false, JsonRequestBehavior.AllowGet);
            order.TypeOfPayment = TypeOfPayment.CreditCard;
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
            var point = order.RestPoint;
            if (user.CheckUser == CheckUser.Сonfirmed)
            {
                var orderResult = BonusOperation(Convert.ToInt32(orderSum*point.BonusPercent/100), user.Id, Operation.Additional,
                        PlusMinus.Plus, order);
                if (orderResult) return Json(Convert.ToInt32(orderSum * point.BonusPercent / 100), JsonRequestBehavior.AllowGet);
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //оплата наличкой - убрать, перенести все офику
        public JsonResult PaymentByCash(int? orderId, decimal orderSum)
        {
            var order = db.RestOrders.SingleOrDefault(o => o.Id == orderId);
            if (order == null) return Json(false, JsonRequestBehavior.AllowGet);
            if (order.TypeOfPayment != null) return Json(false, JsonRequestBehavior.AllowGet);
            var user = order.RestAppUser;
            var point = order.RestPoint;
            order.TypeOfPayment = TypeOfPayment.Cash;
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
            if (user.CheckUser == CheckUser.Сonfirmed)
            {
                var orderResult = BonusOperation(Convert.ToInt32(orderSum * point.BonusPercent / 100), user.Id, Operation.Additional, PlusMinus.Plus, order);
                if (orderResult) return Json(Convert.ToInt32(orderSum * point.BonusPercent / 100), JsonRequestBehavior.AllowGet);
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //оплата бонусами
        public JsonResult PaymentByBonus(int? orderId, Guid sessionId, decimal orderSum)
        {
            var session = db.RestAppSessions.SingleOrDefault(s => s.Id == sessionId);
            if (session == null) return Json(false, JsonRequestBehavior.AllowGet);
            var user = session.RestAppUser;
            var order = (orderId.HasValue) ?
                (db.RestOrders.SingleOrDefault(o => o.Id == orderId)) :
                (db.RestOrders.Where(o => o.RestAppUser.Id == user.Id && o.OpenClose == OpenClose.Open)
                    .OrderByDescending(d => d.DateCreate).FirstOrDefault());

            if (order == null) return Json(false, JsonRequestBehavior.AllowGet);
            order.TypeOfPayment = TypeOfPayment.Bonus;
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();

            var point = order.RestPoint;
            var payments = db.RestPayments.Single(b => b.RestAppUser.Id == user.Id);
            if (payments == null) return Json(false, JsonRequestBehavior.AllowGet);
            if (payments.Bonus < Convert.ToInt32(orderSum)) return Json("недостаточно бонусов на счету", JsonRequestBehavior.AllowGet);
            

            var orderResult = BonusOperation(Convert.ToInt32(orderSum), order.RestAppUser.Id, Operation.Order, PlusMinus.Minus, order);
            return Json(orderResult, JsonRequestBehavior.AllowGet);

        }      
         
        //продукты в корзине
        public JsonResult ProdToBag(string products)
        {
            var list = new List<object>();
            var prod = JsonConvert.DeserializeObject<List<OrderModel>>(products);
            foreach(var i in prod)
            {
                var product = db.RestProducts.Where(p => p.Id == i.ProductId).Select(c => new
                {
                    Name = c.Name,
                    Id = c.Id,
                    status = "product",
                    Price = c.Price,
                    Description = c.Description,
                    Weight = c.Weight,
                    Image = c.Image ?? c.Category.Image,
                    Quantity = i.Quantity,
                    Parent = c.Category.Name,
                    Rating = c.RestProdReviews.Where(pr => pr.RestProduct.Id == c.Id).Select(s => new
                    {
                        Id = s.Id,
                        Comment = s.Comment,
                        Date = s.Date,
                        Rating = s.Rating,
                        User = s.RestAppUser.Name
                    })
                }).Single();
                list.Add(product);
            }
            return Json(list, JsonRequestBehavior.AllowGet);

        }

        //сохранить токен
        public JsonResult SaveToken(Guid sessionId, string token)
        {
            var session = db.RestAppSessions.SingleOrDefault(s => s.Id == sessionId);
            if (session == null) return Json(false, JsonRequestBehavior.AllowGet);
            var user = session.RestAppUser;

            var payments = db.RestPayments.SingleOrDefault(p => p.RestAppUser.Id == user.Id);
            if (payments == null) return Json(false, JsonRequestBehavior.AllowGet);
            payments.Token = token;
            db.Entry(payments).State = EntityState.Modified;
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //получить токен
        public JsonResult GetToken(Guid sessionId)
        {
            var session = db.RestAppSessions.SingleOrDefault(s => s.Id == sessionId);
            if (session == null) return Json(false, JsonRequestBehavior.AllowGet);
            var user = session.RestAppUser;

            var payments = db.RestPayments.SingleOrDefault(p => p.RestAppUser.Id == user.Id);
            if (payments == null) return Json(false, JsonRequestBehavior.AllowGet);
            if (payments.Token == null) return Json(false, JsonRequestBehavior.AllowGet);
            return Json(new { token = payments.Token}, JsonRequestBehavior.AllowGet);
        }

        //айди офика, привязанного к заказу
        public JsonResult GetOfficiantId(int? orderId, Guid sessionId)
        {
            if (orderId.HasValue)
            {
                var order = db.RestOrders.SingleOrDefault(o => o.Id == orderId);
                if (order == null) return Json(false, JsonRequestBehavior.AllowGet);
                return Json(order.Officiant.Id, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var session = db.RestAppSessions.SingleOrDefault(s => s.Id == sessionId);
                if (session == null) return Json(false, JsonRequestBehavior.AllowGet);
                var user = session.RestAppUser;
                var order = db.RestOrders.Where(o => o.RestAppUser.Id == user.Id && o.OpenClose == OpenClose.Open).OrderByDescending(d => d.DateCreate).FirstOrDefault();
                if (order == null) return Json(false, JsonRequestBehavior.AllowGet);
                return Json(order.Officiant.Id, JsonRequestBehavior.AllowGet);
            }
        }

        //проверка на подтвержденный заказ
        public JsonResult ActiveInactive(Guid sessionId)
        {
            var session = db.RestAppSessions.SingleOrDefault(s => s.Id == sessionId);
            if (session == null) return Json(false, JsonRequestBehavior.AllowGet);
            var user = session.RestAppUser;

            var order = db.RestOrders.Where(o => o.RestAppUser.Id == user.Id && o.OpenClose == OpenClose.Open)
                .OrderByDescending(d => d.DateCreate).FirstOrDefault();
            if (order == null) return Json(true, JsonRequestBehavior.AllowGet);
            if (order.RestOrderParts.Any(o => o.ValidPurchase == ValidPurchase.NotConfirmed)) return Json(false, JsonRequestBehavior.AllowGet);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}