﻿$(function () {
   
    var clientCallCenterHub = $.connection.clientCallCenterHub;

    clientCallCenterHub.client.callCenterBroadcast = function (name, message) {
        //$('#divResponse').text("Message: " + message);

        //var accept = confirm("You have recieved a message from " + name + '. Would you like to respond?');

        //if (accept) {
        //    $.connection.hub.start().done(function () {
        //        clientCallCenterHub.server.send('callcenter', 'pong');
        //        $('#divResponse').text('waiting...');
        //    });
        //}

        ShowModal();
    };

    $.connection.hub.start();
});