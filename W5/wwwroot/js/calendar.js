var events = [];

$(document).ready(function () {
    Calendar();
});

function Calendar() {
    var eventColor;
    var today = new Date();
    today.setDate(today.getDate() + 1);
    today.setHours(0, 0, 0, 0);
    today = today.toISOString();
    today = today.substring(0, 10) + "T00:00:00";

    $.ajax(
        {
            type: "GET",
            url: "/User/Calendar/GetAll",
            success: function (data) {
                $.each(data, function (i, v) {

                    if (today > v.date) eventColor = "#ff0000";
                    else if (today < v.date) eventColor = "#00ff00";
                    else if (today == v.date) eventColor = "#1ac6ff";

                    events.push({
                        title: v.setName + v.repetition,
                        description: null,
                        start: moment(v.date),
                        end: null,
                        color: eventColor,
                        allDay: true
                    });

                })

                GenerateCalendar(events);
            },
            error: function (error) {
                alert('failed');
            }
        }

    )   
}

function GenerateCalendar(event) {
    $('#calendar').fullCalendar('destroy');
    $('#calendar').fullCalendar(
        {
            contentHeight: 700,
            defaultDate: new Date(),
            timeFormat: 'h(:mm)a',
            header:
            {
                left: 'today',
                center: 'prev title next',
                right: 'month,basicWeek'
            },
            eventLimit: true,
            eventColor: '#00ff00',
            events: events
        }
    )

}