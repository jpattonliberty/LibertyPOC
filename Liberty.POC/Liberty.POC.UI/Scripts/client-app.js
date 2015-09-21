$(function () {
    $(document).ready(function () {
        var clientCallCenterHub = $.connection.clientCallCenterHub;

        clientCallCenterHub.client.CallCenterMessage = function (accepted) {
            if (accepted) {
                alert('Your session has been taken over by a call center agent. You will be logged out while the call center completes the process on your behalf');
                window.location.replace('/Client/Login');
            } else {
                alert('There are no call center agents available to take over this session');
            }
        };

        $.connection.hub.start().done(function () {
            $('#divRequestHelp').click(function () {
                HelpMe(clientCallCenterHub);
            });
        });
    });
});

function HelpMe(clientCallCenterHub) {
    SaveClientData(function (data) {
        if (data.status === "Success") {
            var sessionId = $('#txtSessionId').val();
            clientCallCenterHub.server.clientMessage(sessionId);
            alert('Your request for assistance has been sent. You will recieve a response shortly');
        } else {
            alert("An error occured");
        }
    }, false, true);
}


function Finish() {
    SaveClientData(function (data) {
        if (data.status === "Success") {
            window.location.replace('/Client/Login');
        } else {
            alert("Error occured!");
        }
    }, true, false);
}
