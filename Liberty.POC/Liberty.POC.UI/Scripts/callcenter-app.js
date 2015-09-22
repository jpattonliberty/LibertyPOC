$(function () {

    var clientCallCenterHub = $.connection.clientCallCenterHub;

    clientCallCenterHub.client.ClientMessage = function (id, username) {

        SetModelText(username + " is requesting assistance. Please contact this client on 074 555 6666. If consent is granted, you can take over this clients session by clicking the 'Yes' button below");

        ShowModel();

        $.connection.hub.start().done(function () {
            $('#btnBroadcastMessage').click(function () {
                clientCallCenterHub.server.callCenterMessage(true);
                window.location.replace('/CallCenter/Process/' + id);
            });

            $('#btnDeclineSession').click(function () {
                clientCallCenterHub.server.callCenterMessage(false);
                HideModel();
            });
        });
    };

    $.connection.hub.start();
});

function Finish() {
    SaveClientData(function () {
        window.location.replace('/CallCenter/Home');
    }, true, false);
}