$(function () {
    $.datepicker.setDefaults($.datepicker.regional["es"]);
    $("#dtDate").datepicker({
        firstDay: 1,
    });
});

$(document).ready(function () {
    ShowAllOrder();
});

function ShowAllOrder() {
    $.ajax({
        type: 'GET',
        dataType: "json",
        contentType: "application/json",
        url: '/Orders/GetOrders',
        success: function (result) {
            var strHtml = "<table border='1' >";
            strHtml += "<tr><th>Order Date</th><th>Order Number</th><th>Customer Name</th><th>Delivery Address</th><th>Quantity Ordered</th><th>Action</th></tr>";
            for (var i in result) {
                strHtml += "<tr>";
                strHtml += "<td>" + FormatDate(result[i].OrderDate) + "</td>";
                strHtml += "<td> <a href='#' onclick='RedirectOrder(" + result[i].ID + ");'  > " + result[i].OrderNumber + " </a></td>";
                strHtml += "<td>" + result[i].CustomerName + "</td>";
                strHtml += "<td>" + result[i].DeliveryAddress + "</td>";
                strHtml += "<td>" + result[i].QuantityOrdered + "</td>";
                strHtml += "<td> <input type='button' onclick='DeleteOrder(" + result[i].ID + ");'  value='Delete Selected Order' /></td>";
                strHtml += "</tr>";
            }
            strHtml += "</table>";

            $("#gdOrders").html(strHtml);
        }
    });
}

function RedirectOrder(id) {
    window.location.href = '/Order/Index/' + id;
}

function DeleteOrder(id) {
    $.ajax({
        type: 'GET',
        data: { pId: id },
        url: '/Orders/DeleteOrder',
        success: function (result) {
            if (result == "OK") {
                ShowAllOrder();
            }
        }
    });
}

function FindOrder() {
    var date = $.trim($("#dtDate").val());

    $.ajax({
        type: 'GET',
        data: { pDate: date },
        url: '/Orders/GetOrdersByDate',
        success: function (result) {
            var strHtml = "<table border='1'  >";
            strHtml += "<tr><th>Order Date</th><th>Order Number</th><th>Customer Name</th><th>Delivery Address</th><th>Quantity Ordered</th><th>Action</th></tr>";
            for (var i in result) {
                strHtml += "<tr>";
                strHtml += "<td>" + FormatDate(result[i].OrderDate) + "</td>";
                strHtml += "<td> <a href='#' onclick='RedirectOrder(" + result[i].ID + ");'  > " + result[i].OrderNumber + " </a></td>";
                strHtml += "<td>" + result[i].CustomerName + "</td>";
                strHtml += "<td>" + result[i].DeliveryAddress + "</td>";
                strHtml += "<td>" + result[i].QuantityOrdered + "</td>";
                strHtml += "<td> <input type='button' onclick='DeleteOrder(" + result[i].ID + ");'  value='Delete Selected Order' /></td>";
                strHtml += "</tr>";
            }
            strHtml += "</table>";

            $("#gdOrders").html(strHtml);
        }
    });
}

function FormatDate(value) {
    var dt = new Date(parseInt(value.substring(6, value.length - 2)));
    var dtString = (dt.getMonth() + 1) + "/" + dt.getDate() + "/" + dt.getFullYear();
    return dtString;
}