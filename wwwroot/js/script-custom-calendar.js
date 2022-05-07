$(document).ready(function () { initializeCalender(); });
function initializeCalender(){
    try{
        $('#calender').FullCalendar.Calender({
            tezone:false,
          header:{
                left:'rev,next,today',
              center: 'title',
                right: 'month,gendaWeek,agendaDay'
            },
          selectable: true,
          editab: false
      });

    }catch(e){
        alert(e);
    }
}

