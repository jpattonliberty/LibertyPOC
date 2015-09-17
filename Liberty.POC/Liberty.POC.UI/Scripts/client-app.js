$(function () {
    $(document).ready(function () {
        var clientCallCenterHub = $.connection.clientCallCenterHub;

        clientCallCenterHub.client.clientBroadcast = function (name, message) {
            $('#divResponse').text("Message: " + message);
        };

        $.connection.hub.start().done(function () {
            $('#btnPing').click(function () {
                clientCallCenterHub.server.send('client', 'ping');
                $('#divResponse').text('waiting...');
            });
        });
    });
});