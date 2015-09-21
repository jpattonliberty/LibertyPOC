$(function () {

    var clientCallCenterHub = $.connection.clientCallCenterHub;

    clientCallCenterHub.client.ClientMessage = function (id) {
        //$('#divResponse').text("Message: " + message);

        //var accept = confirm("You have recieved a message from " + name + '. Would you like to respond?');

        //if (accept) {
        //    $.connection.hub.start().done(function () {
        //        clientCallCenterHub.server.send('callcenter', 'pong');
        //        $('#divResponse').text('waiting...');
        //    });
        //}

        //ShowModal(function () {
        //    //Connect was clicked, link through to Process controller
        //    //Send an accept response to the client

        //        //window.navigate...
        //    },
        //function () {
        //    //Cancel was clicked, do nothing
        //    //Send a decline response to the client
        //    $("#modal-window").modal('hide');
        //});

        var accept = confirm("You have recieved session id " + id + '. Would you like to accept?');

        $.connection.hub.start().done(function () {
            clientCallCenterHub.server.callCenterMessage(accept);
        });

        if (accept) {
            window.location.replace('/CallCenter/Process/' + id);
        }
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