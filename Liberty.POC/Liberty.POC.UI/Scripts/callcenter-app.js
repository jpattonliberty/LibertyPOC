$(function () {
    $(document).ready(function () {
        $('#btnPong').hide();
    });

    var clientCallCenterHub = $.connection.clientCallCenterHub;

    clientCallCenterHub.client.callCenterBroadcast = function (name, message) {
        $('#divResponse').text(message + ' from ' + name);
    };

    $.connection.hub.start();
});