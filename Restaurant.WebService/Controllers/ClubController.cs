using System.Linq;
using System.Web.Mvc;

namespace Intouch.Restaurant.Controllers
{
    public class ClubController : BaseController
    {
        public JsonResult RestNetworkInfo(int? pointId)
        {
            var rest = db.RestPoints.SingleOrDefault(c => c.Point.Id == pointId);
            if (rest != null)
            {
                var model = new
                {
                    ClubId = rest.Id,
                    PointId = pointId,
                    ClubName = rest.Name,
                    ClubDescription = rest.Description,
                    ClubAddress = rest.Point.Address,
                    WorkTime = rest.WorkTime,
                    Skype = rest.Skype,
                    Logo = rest.Logo,
                    Kitchen = rest.Kitchen,
                    EmailToFeedback = rest.EmailToFeedback,
                    EmailToReservation = rest.EmailToReservation,
                    AveragePrice = rest.AveragePrice,
                    Background = rest.Background,
                    ClubTelephone = rest.Phone,
                    BonusPercent = rest.BonusPercent,
                    Email = rest.Email,
                    WelcomePage = rest.WelcomePage,
                    Photos = db.RestGallerys.Where(p => p.RestPoint.Id == rest.Id).Select(s => new
                    {
                        PhotoId = s.Id,
                        PhotoName = s.Image,
                        Description = s.Description
                    })
                };
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var networkId = GetNetworkId();
                var restNetwork = db.RestNetworks.Single(c => c.Network.Id == networkId);
                var model = new
                {
                    ClubId = 0,
                    NetworkId = networkId,
                    Name = restNetwork.Name,
                    Description = restNetwork.Description,
                    AveragePrice = restNetwork.AveragePrice,
                    OpeningTime = restNetwork.OpeningTimes,
                    Services = restNetwork.Services,
                    Background = restNetwork.Background,
                    OtherServices = restNetwork.OthersServices,
                    Tariff = restNetwork.Tariff,
                    Telephone = restNetwork.Phone,
                    Video = restNetwork.VideoUrl,
                    Photos = db.RestGallerys.Where(p => p.RestPoint.Point.Network.Id == networkId).ToList().Select(s => new
                    {
                        PhotoId = s.Id,
                        Photo = s.Image,
                        Description = s.Description
                    })
                };
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult AllClubs(double? latitude, double? longitude) // координаты положения человека
        {
            var networkId = GetNetworkId();
            var restaurants = db.RestPoints.Where(r => r.Point.Network.Id == networkId).Select(res => new AllClubsModel
            {
                ClubId = res.Id,
                PointId = res.Point.Id,
                Name = res.Name,
                Logo = res.Logo,
                Network = res.Point.Network.Name,
                Address = res.Point.Address,
                latitude = res.Point.Latitude,
                longitude = res.Point.Longitude
            }).ToList();
            if (latitude.HasValue && longitude.HasValue)
            {
                foreach (var rest in restaurants)
                {
                    rest.distance = GetDistance(latitude, longitude, rest);
                }
                restaurants = restaurants.OrderBy(res => res.distance).ToList();
                return Json(new { Ordered = "distance", Rest = restaurants }, JsonRequestBehavior.AllowGet);
            }
            restaurants = restaurants.OrderByDescending(res => res.ClubId).ToList();
            return Json(new { Ordered = "point", Rest = restaurants }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult OneClub(int pointId, double? latitude, double? longitude) // координаты положения человека
        {
            var restaurant = db.RestPoints.Where(r => r.Point.Id == pointId).Select(res => new AllClubsModel
            {
                ClubId = res.Id,
                Phone = res.Phone,
                PointId = res.Point.Id,
                Name = res.Name,
                Logo = res.Logo,
                Network = res.Point.Network.Name,
                Address = res.Point.Address,
                latitude = res.Point.Latitude,
                longitude = res.Point.Longitude
            }).FirstOrDefault();
            if (restaurant == null) return Json(false, JsonRequestBehavior.AllowGet);
            if (latitude.HasValue && longitude.HasValue)
            {
                restaurant.distance = GetDistance(latitude, longitude, restaurant);
                return Json(new { Ordered = "distance", Rest = restaurant }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Ordered = "point", Rest = restaurant }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMainPhotos(int pointId)
        {
            var photo = db.RestMainPhotos.Where(p => p.RestPoint.Point.Id == pointId).Select(s => new
            {
                Id = s.Id,
                Photo = s.Photo,
                Date = s.DateCreate
            }).OrderBy(s => s.Date);
            return Json(photo, JsonRequestBehavior.AllowGet);
        }
    }
}