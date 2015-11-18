$(function () {   
    $("#OrderDate").datepicker({
        firstDay: 1,
    });
});

$(document).ready(function () {

    $("#dialog").dialog({
        buttons: [{
            text: "Save Order", click: function () {
                $(this).dialog("close");
                SaveOrder();
            }
        },
        {
            text: "Back to Orders", click: function () {
                $(this).dialog("close");
                window.location.href = '/Orders/Index';
            }
        }],
        autoOpen: false,
        show: { effect: "bounce", duration: 300 },
        hide: { effect: "explode", duration: 300 }
    });

    $("#btnSend").click(function () {
        $("#dialog").dialog("open");
        return false;
    });

    $("#lnkReturnOrder").click(function () {
        var modelOrder = GetModel();

        $.ajax({
            url: '/Order/ReturnOrders',
            type: "POST",
            data: JSON.stringify(modelOrder),
            contentType: 'application/json; charset=utf-8',
            dataType: "json",
            success: function (result) {
                if (result == "True") {
                    $("#dialog").dialog("open");
                    return false;
                } else {
                    window.location.href = '/Orders/Index';
                }
            }
        });
    });

});

function GetModel() {
   
    var modelOrder = {
        ID: $('#ID').val(),
        OrderNumber: $('#OrderNumber').val(),
        CustomerName: $('#CustomerName').val(),
        DeliveryAddress: $('#DeliveryAddress').val(),
        OrderDate: $('#OrderDate').val()
    };

    return modelOrder;
}

function SaveOrder() {
    var modelOrder = GetModel();

    $.ajax({
        url: '/Order/SaveOrder',
        type: "POST",
        data: JSON.stringify(modelOrder),
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        success: function (result) {
            alert("Success Save.");
            window.location.href = '/Orders/Index';
        }
    });
}