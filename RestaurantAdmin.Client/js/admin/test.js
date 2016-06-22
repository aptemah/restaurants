$(document).ready(function (e) {

    var sessionId = localStorage.getItem("SessionID");
    //server = "http://stas.intouchclub.ru/RestWeb";
    getOffTable(sessionId);

    function getOffTable() {
        $.ajax({
            url: server + "/Staff/OpenTable",
            async: false,
            success: function (result) {
                console.log(result);
                result.ForEach(function () {
                $("#table").append($("<tr>")
                    .append($("<td>", { html: "заказ номер " + this.OrderNumber }))
                        .append($("<td>", { class: "tab-" + this.OrderNumber })
                            .append($("<table>", { class: "table", id: "table-orderPart" + this.OrderNumber }))
                            )
                    );
                var orderNumb = this.OrderNumber;
                

                this.Orders.ForEach(function () {
                    var valid = this.ValidPurchase;
                    var orderPartId = this.Id;
                    $("#table-orderPart" + orderNumb).append($("<tr>")
                        .append($("<div>", { class: "td-" + orderPartId })));
                    this.Products.ForEach(function () {
                            $(".td-" + orderPartId).append($("<div>", { class: "div-" + this.Id })
                                .append($("<img>", { src: "http://stas.intouchclub.ru/intouchsoft/Content/Restaurant/icons/menu/" + this.Image }))
                                .append($("<div>", { html: this.Name }))
                                .append($("<div>", { html: "кол-во: " + this.Quantity }))
                                );
                            if (valid == 0) {
                                $(".div-" + this.Id).append($("<button>", { html: "удалить", class: "btn", id: "delBtn", orderPartId: orderPartId, prodId: this.Id }));
                        }
                        
                    });
                    if (this.ValidPurchase == 0) {
                        $(".td-" + orderPartId).append($("<button>", { html: "подтвердить", class: "btn acceptBtn", id: "acceptBtn", orderPartId: orderPartId }));
                    } else {
                        $(".td-" + orderPartId).append($("<div>", { html: "заказ подтвержден", class: "accept-div" }));
                    }
                        
                        
                });
                if (this.TypeOfPayment == null) {
                    $(".tab-" + this.OrderNumber).append($("<button>", { html: "оплата наличными", class: "btn acceptBtn", id: "paymentByCash", orderId: this.Id, summ: this.OrderSumm }));
                }
                $(".tab-" + this.OrderNumber).append($("<div>", { html: "общая сумма: " + this.OrderSumm + "грн", class: "accept-div", id: "orderSumm" }));
                $(".tab-" + this.OrderNumber).append($("<button>", { html: "закрыть заказ", class: "btn acceptBtn", id: "closeBtn", orderId: this.Id }));
                });
                
            }
        });
    };
    $("body").on("click", "#delBtn", function () {
        var prodId = $(this).attr("prodId");
        var orderPartId = $(this).attr("orderPartId");
        $.ajax({
            url: server + "/Staff/DelProdFromOrder",
            async: false,
            data: { prodId: prodId, orderPartId: orderPartId },
            success: function (result) {
                console.log(result);
                if (result == true) location.reload();
            }
        });
    });
    $("body").on("click", "#acceptBtn", function () {
        var orderPartId = $(this).attr("orderPartId");
        $.ajax({
            url: server + "/Staff/ConfirmOrder",
            async: false,
            data: { orderPartId: orderPartId },
            success: function (result) {
                if (result == true) location.reload();
            }
        });
    });
    $("body").on("click", "#closeBtn", function () {
        var orderId = $(this).attr("orderId");
        $.ajax({
            url: server + "/Staff/CloseOrder",
            async: false,
            data: { orderId: orderId },
            success: function (result) {
                if (result == true) location.reload();
            }
        });
    });
    $("body").on("click", "#paymentByCash", function () {
        var orderId = $(this).attr("orderId");
        var summ = $(this).attr("summ");
        console.log(summ);
        $.ajax({
            url: server + "/Order/PaymentByCash",
            async: false,
            data: { orderId: orderId, orderSum: summ },
            success: function (result) {
                if (result == true) location.reload();
            }
        });
    });

});