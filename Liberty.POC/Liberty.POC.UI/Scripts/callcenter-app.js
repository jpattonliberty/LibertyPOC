$(function () {

    var clientCallCenterHub = $.connection.clientCallCenterHub;

    clientCallCenterHub.client.ClientMessage = function (id) {
        $('#basicModal').modal('show');

        $.connection.hub.start().done(function () {
            $('#btnBroadcastMessage').click(function () {
                clientCallCenterHub.server.callCenterMessage(true);
                window.location.replace('/CallCenter/Process/' + id);
            });

            $('#btnDeclineSession').click(function () {
                clientCallCenterHub.server.callCenterMessage(false);
                $('#basicModal').modal('hide');
            });
        });
    };

    $.connection.hub.start();
});

function Finish() {
    SaveClientData(function (data) {
        if (data.status === "Success") {
            window.location.replace('/CallCenter/Home');
        } else {
            alert("Error occured!");
        }
    }, true, false);
}

function ShowModal(yesCallback, noCallback) {
    $("#modal-window").modal();
    $("#modal-window").css({
        top: ($(window).height() - $(this).height()) / 2,
        left: ($(window).width() - $(this).width()) / 2
    });

    $('#btnConnect').click(function () {
        $("#modal-window").modal('toggle');
        if (yesCallback) yesCallback();
    });
    $('#btnCancel').click(function () {
        $("#modal-window").modal('toggle');
        if (noCallback) noCallback();
    });
}