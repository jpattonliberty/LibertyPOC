$(function () {
    $(document).ready(function () {
        var clientCallCenterHub = $.connection.clientCallCenterHub;

        clientCallCenterHub.client.CallCenterMessage = function (accepted) {

            if (accepted) {
                SetModelText('Your session has been taken over by a call center agent. You will now be logged out while the call center completes the process on your behalf.');

                $('#btnOk').click(function () {
                    window.location.replace('/Client/Login');
                });
            }
            else {
                SetModelText('There are no available call center agents at this time. Please try again later.');
            }

            HideModelButtons();
            ToggleOnModelOkButton();

            $('#btnOk').click(function () {
                ResetModelWindowToDefault();
            });
        };

        $.connection.hub.start().done(function () {
            $('#btnBroadcastMessage').click(function () {
                SaveClientData(function () {
                    var sessionId = $('#txtSessionId').val();
                    var username = $('#txtName').val();
                    clientCallCenterHub.server.clientMessage(sessionId, username);
                    SetModelText('Request has been sent. Waiting for a resonse from the first available call center agent...');
                    DisableModelButtons();
                }, false, true);
            });
        });
    });
});

function Finish() {
    SaveClientData(function () {
        window.location.replace('/Client/Login');
    }, true, false);
}

function ResetModelWindowToDefault() {
    HideModel();
    SetModelText('You are about to hand your current session over to the call center. Are you sure you wish to continue?');
    ShowModelButtons();
    HideModelOkButton();
    EnableModelButtons();
}