function SaveAndContinue(id) {
    SaveClientData(function (data) {
        if (data.status === "Success") {
            $("#" + id).click();
            fade();

        } else {
            alert("Error occurs on the Database level!");
        }
    }, false, false);
}

function SaveClientData(successCallback, isComplete, isHelperCall) {
    var payload = GetClientDetails(isComplete, isHelperCall);

    $.ajax({
        url: "/Client/Save",
        type: "POST",
        data: JSON.stringify(payload),
        dataType: "json",
        traditional: true,
        contentType: "application/json; charset=utf-8",
        success: successCallback,
        error: function () {
            alert("An error has occured!!!");
        }
    });
}

function fade() {
    $('#support-tag').fadeOut(350);
    $('#support-tag').fadeIn(350);
    window.setTimeout("$('#support-tag').fadeOut(350); $('#support-tag').fadeIn(350);", 2500);
    window.setTimeout("$('#support-tag').fadeOut(350); $('#support-tag').fadeIn(350);", 5000);
}

function GetClientDetails(isComplete, isHelperCall) {
    var sessionId = $('#txtSessionId').val();
    var name = $('#txtName').val();
    var personal = GetPersonal();
    var address = GetAddress();
    var contact = GetContact();

    var session = {
        "Id": sessionId,
        "Name": name,
        "CurrentStep": (isHelperCall ? getCurrentStep() : getCurrentSaveStep()),
        "AddressDetails": address,
        "ContactDetail": contact,
        "PersonalDetails": personal,
        "IsCompleted": isComplete
    };

    return session;
}

function getCurrentSaveStep() {
    if ($('#personal').hasClass('active'))
        return 'Address';
    else if ($('#address').hasClass('active'))
        return 'Contact';
    return 'Personal';
}

function getCurrentStep() {
    if ($('#personal').hasClass('active'))
        return 'Personal';
    else if ($('#address').hasClass('active'))
        return 'Address';
    return 'Contact';
}

function GetPersonal() {
    var firstname = $('#txtFirstName').val();
    var surname = $('#txtSurname').val();
    var idnumber = $('#txtIdNumber').val();
    var nodependants = $("#ddlNoDependants option:selected").text();

    var personal = {
        "FirstName": firstname,
        "Surname": surname,
        "IdNo": idnumber,
        "NoOfDependants": nodependants
    }
    return personal;
}

function GetAddress() {

    var street = $('#txtStreet').val();
    var suburb = $('#txtSuburb').val();
    var city = $('#txtCity').val();
    var province = $("#ddlProvince option:selected").text();
    var code = $('#txtCode').val();

    var address = {
        "PhysicalAddressStreet": street,
        "PhysicalAddressSuburb": suburb,
        "PhysicalAddressCity": city,
        "PhysicalAddressProvince": province,
        "PhysicalAddressCode": code
    }
    return address;
}

function GetContact() {
    var email = $('#txtEmail').val();
    var cellNo = $('#txtCellNo').val();
    var homeNo = $('#txtHomeNo').val();
    var workNo = $('#txtWorkNo').val();
    var altNo = $('#txtAltNo').val();

    var contact = {
        "EmailAddress": email,
        "Cellphone": cellNo,
        "Home": homeNo,
        "Work": workNo,
        "Alternative": altNo
    }
    return contact;
}

function ShowModal(yesCallback, noCallback) {
    $("#modal-window").modal();
    $("#modal-window").css({
        top: ($(window).height() - $(this).height()) / 2,
        left: ($(window).width() - $(this).width()) / 2
    });
    $('#btnConnect').click(function ()
    {
        $("#modal-window").modal('toggle');
        if (yesCallback) yesCallback();
    });
    $('#btnCancel').click(function ()
    {
        $("#modal-window").modal('toggle');
        if (noCallback) noCallback();
    });
}