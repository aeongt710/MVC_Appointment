$(document).ready(function () { initializeCalender(); });
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