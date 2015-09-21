$(function () {
    $(document).ready(function () {
        var clientCallCenterHub = $.connection.clientCallCenterHub;

        clientCallCenterHub.client.CallCenterMessage = function (accepted) {

            if (accepted) {
                $("#basicModal .modal-body").text('Your session has been taken over by a call center agent. You will now be logged out while the call center completes the process on your behalf.');
                $("#basicModal .modal-footer :input").addClass("hide");

                $('#btnOk').click(function () {
                    window.location.replace('/Client/Login');
                });

            } else {
                $("#basicModal .modal-body").text('There are no available call center agents at this time.');
                $("#basicModal .modal-footer :input").addClass("hide");
            }
            $("#basicModal .modal-footer #btnOk").attr("disabled", false);
            $("#basicModal .modal-footer #btnOk").removeClass("hide");

            $('#btnOk').click(function () {
                $("#basicModal").modal('hide');
                $("#basicModal .modal-body").text('You are about to hand your current session over to the call center. Are you sure you wish to continue?');
                $("#basicModal .modal-footer :input").removeClass("hide");
                $("#basicModal .modal-footer #btnOk").addClass("hide");
                $("#basicModal .modal-footer :input").attr("disabled", false);
            });
        };

        $.connection.hub.start().done(function () {
            $('#btnBroadcastMessage').click(function () {
                SaveClientData(function (data) {
                    if (data.status === "Success") {
                        var sessionId = $('#txtSessionId').val();
                        clientCallCenterHub.server.clientMessage(sessionId);

                        $("#basicModal .modal-body").text('Request has been sent. Waiting for a resonse from the first available call center agent...');
                        $("#basicModal :input").attr("disabled", true);
                    } else {
                        alert("An error occured");
                    }
                }, false, true);
            });
        });
    });
});

function Finish() {
    SaveClientData(function (data) {
        if (data.status === "Success") {
            window.location.replace('/Client/Login');
        } else {
            alert("Error occured!");
        }
    }, true, false);
}