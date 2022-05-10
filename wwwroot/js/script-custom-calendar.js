var routeURL = location.protocol + "//" + location.host;
$(document).ready(function () {
    $('#startDate').kendoDateTimePicker({
        value: new Date(),
        dateInput: false
    });
    initializeCalender();
});


var calendar;
function initializeCalender() {
    try {
        var calendarEl = document.getElementById('calendar');
        if (calendarEl != null) {
            calendar = new FullCalendar.Calendar(calendarEl, {

                timezone: false,

                header: {
                    left: 'rev,next,today',
                    center: 'title',
                    right: 'month,gendaWeek,agendaDay'
                },

                selectable: true,

                editable: false,

                select: function (event) {

                    onShowModel(event, null);
                },
                eventDisplay: 'block',
                eventDisplay: 'block',
                events: function (fetchInfo, successCallback, failureCallback) {
                    $.ajax({
                        url: routeURL + '/api/Appointment/GetCalendarData?docId=' + $("#doctorId").val(),
                        type: 'GET',
                        dataType: 'JSON',
                        success: function (response) {
                            var events = [];
                            if (response.status === 1) {
                                $.each(response.dataenum, function (i, data) {
                                    events.push({
                                        title: data.title,
                                        description: data.description,
                                        start: data.startDate,
                                        end: data.endDate,
                                        backgroundColor: data.isDoctorApproved ? "#28a745" : "#dc3545",
                                        borderColor: "#162466",
                                        textColor: "white",
                                        id: data.id
                                    });
                                })
                            }
                            console.log(events);
                            successCallback(events);
                        },
                        error: function (xhr) {
                            $.notify("Error", "error");
                        }
                    });
                },

                eventClick: function (info) {
                    console.log("eventClick", info.event);
                    getEventDetailsByEventId(info.event);
                }


            });
            calendar.render();
        }

    } catch (e) {
        alert(e);
    }
}

function onShowModel(obj, isEventDetail) {
    if (isEventDetail != null) {
        console.log("inside not null")
        //console.log("onShowModel obj details if not null", obj);
        $('#id').val(obj.id);
        $('#title').val(obj.title);
        $('#description').val(obj.description);
        $('#startDate').val(obj.startDate);
        $('#patientId').val(obj.patientId);
        $('#duration').val(obj.duration);
    } else {
        $('#startDate').kendoDateTimePicker({
            value: new Date(),
            dateInput: false
        });
        $('#id').val(0);
        $('#title').val("");
        $('#description').val("");
        //$('#duration').val("");
        //$('#patientId').val("");
        //$('#doctorId').val();
        $("#startDate").val(obj.startStr + " " + new moment().format("hh:mm A"));
        $('#appointmentInput').modal("show");
    }
    $('#appointmentInput').modal("show");
}

function onCloseModel() {
    $('#appointmentInput').modal("hide");
}

function onSubmitForm() {

    if (checkValidation()) {

        var requestData = {
            Id: parseInt($('#id').val()),
            Title: $('#title').val(),
            Description: $('#description').val(),
            StartDate: $('#startDate').val(),
            Duration: $('#duration').val(),
            PatientId: $('#patientId').val(),
            DoctorId: $('#doctorId').val(),
        };
        $.ajax({
            url: routeURL + '/api/Appointment/SaveCalendarData',
            type: 'POST',
            data: JSON.stringify(requestData),

            contentType: 'application/json',
            success: function (response) {
                if (response.status === 1 || response.status === 2) {
                    $.notify(response.message, "success");
                    calendar.refetchEvents();
                    onCloseModel();
                }
                else {
                    $.notify(response.message, "error");
                }
            },
            error: function (xhr) {
                $.notify("Error", "error");
            }
        });

    }
}

function checkValidation() {
    var check = true;
    if ($('#title').val() === "" || $('#title').val() === undefined) {
        check = false;
        $('#title').addClass('error');
    } else {
        $('#title').removeClass('error');
    }
    if ($('#startDate').val() === "" || $('#startDate').val() === undefined) {
        check = false;
        $('#startDate').addClass('error');
    } else {
        $('#startDate').removeClass('error');
    }
    //if ($('#duration').val() === "" || $('#duration').val() === undefined) {
    //    check = false;
    //    console.log("dur check")
    //    $('#duration').addClass('error');
    //} else {
    //    $('#duration').removeClass('error');
    //}

    return check;
}

function getEventDetailsByEventId(info) {

    $.ajax({
        url: routeURL + '/api/Appointment/GetcalendarDataById/' + info.id,
        type: 'GET',
        data: JSON,
        success: function (response) {
            console.log("response", response);
            if (response.status == 1) {
                onShowModel(response.dataenum, true);
            }
        },
        error: function (xhr) {
            $.notify("Error", "error");
        }
    });
}
function onDocChange() {
    calendar.refetchEvents();
}