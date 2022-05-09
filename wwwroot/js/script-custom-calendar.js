var routeURL = location.protocol + "//" + location.host;
$(document).ready(function () {
    $('#startDate').kendoDateTimePicker({
        value: new Date(),
        dateInput: false
    });
    initializeCalender();
});



function initializeCalender() {
    try {
        //  $('#calender').fullCalendar({
        //      timezone:false,
        //    header:{
        //          left:'rev,next,today',
        //        center: 'title',
        //          right: 'month,gendaWeek,agendaDay'
        //      },
        //    selectable: true,
        //    editable: false
        //});
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
                }

            });
            calendar.render();
        }

    } catch (e) {
        alert(e);
    }
}

function onShowModel(obj, isEventDetail) {
    console.log("click");
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
    
    return check;
}