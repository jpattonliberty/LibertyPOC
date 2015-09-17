$(function () {
    $(document).ready(function () {
        var clientCallCenterHub = $.connection.clientCallCenterHub;

        $.connection.hub.start().done(function () {
            $('#btnPing').click(function () {
                clientCallCenterHub.server.send('client', 'ping');
                $('#divResponse').text('waiting...');
            });
        });
    });
});